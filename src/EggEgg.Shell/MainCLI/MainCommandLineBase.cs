using Microsoft.Extensions.Logging;
using System.Reflection;
using YYHEggEgg.Logger;
using YYHEggEgg.Shell.Attributes;
using YYHEggEgg.Shell.AutoCompletion;
using YYHEggEgg.Shell.Exceptions;
using YYHEggEgg.Shell.Model;

namespace YYHEggEgg.Shell.MainCLI;

/// <summary>
/// Define all basic features of a Command Line.
/// </summary>
public abstract class MainCommandLineBase
{
    /// <summary>
    /// Logger.
    /// </summary>
    protected readonly ILogger _logger;
    /// <summary>
    /// Command handlers keyed by their <see cref="CommandHandlerBase.CommandName"/>.
    /// </summary>
    protected Dictionary<string, CommandHandlerBase> _commandHandlers;

    /// <summary>
    /// Get all command handlers for this application to register.
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerable<CommandHandlerBase> GetCommandHandlers();
    /// <summary>
    /// Get a fallback targetHandler for 'command name' that doesn't bind to one in <see cref="GetCommandHandlers()"/>.
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// IMPORTANT: It will be invoked EVERY TIME when a new command starts executing, so consider reuse the same instance when implementing. <para/>
    /// Another IMPORTANT: when a general targetHandler's <see cref="CommandHandlerBase.GetSuggestions(string, int)"/>
    /// is invoked, the given <c>text</c> will start with <see cref="CommandHandlerBase.CommandName"/>
    /// (even though user never type them). This is due to compatibility demands
    /// of the method <seealso cref="CommandHandlerBase.GetSuggestions(string, int)"/>.
    /// </remarks>
    protected virtual CommandHandlerBase? RefreshGeneralOperationHandler() => null;
    /// <summary>
    /// The notice for welcome. When command line starts, the specified
    /// notice will be written to console as lines.
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerable<string>? AppStartNotices => null;
    /// <summary>
    /// The help notice content, shown after <see cref="AppStartNotices"/>.
    /// Default is <c>Now running! Type 'help' to get commands help.</c>.
    /// You're recommended to add content before this rather than replacing.
    /// </summary>
    /// <returns></returns>
    protected virtual string HelpNotice => "Now running! Type 'help' to get commands help.";
    /// <summary>
    /// Whenever the program starts waiting for a new command, it outputs this.
    /// </summary>
    protected virtual IEnumerable<string>? StartNewCommandNotices => null;
    /// <summary>
    /// The prompt for waiting input, like <c>ubuntu@localhost:~$ </c>, or as simple as default <c>> </c>.
    /// </summary>
    /// <remarks>It should have a trailing space.</remarks>
    protected virtual string WaitingInputPrompt => "> ";
    /// <summary>
    /// The name of command history file name. If providing this, a file will be generated to support recovering shell history at startup.
    /// </summary>
    protected virtual string? CommandHistoryFilePath => null;

    private CommandHistoryManager? _historyManager;
    private CommandHandlerBase? _unknownHandler;

    /// <summary>
    /// The cancellation token source for <see cref="RunAsync(CancellationToken)"/>.
    /// </summary>
    protected readonly CancellationTokenSource _commandLineCancellationTokenSource;

    /// <summary>
    /// Throw <see cref="InvalidOperationException"/> if Command Line has
    /// already started.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    protected void ThrowIfStarted()
    {
        if (Started)
            throw new InvalidOperationException("Trying to operate after Command Line started.");
    }

    /// <summary>
    /// Whether the Command Line has already started.
    /// </summary>
    public bool Started { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <exception cref="InvalidOperationException">
    /// Before using YYHEggEgg.Shell features, you must use
    /// <see cref="Log.Initialize(LoggerConfig)"/>
    /// with a <see cref="LoggerConfig"/> that has
    /// '<see cref="LoggerConfig.Use_Console_Wrapper"/> = true'
    /// specified.
    /// </exception>
    public MainCommandLineBase(ILogger logger)
    {
        _logger = logger;
        if (!string.IsNullOrEmpty(CommandHistoryFilePath))
        {
            _historyManager = new(CommandHistoryFilePath);
            ConsoleWrapper.ChangeHistory(_historyManager.ReadSavedHistory());
        }
        _commandLineCancellationTokenSource = new();
        _commandHandlers = [];

        if (!Log.GlobalConfig.Use_Console_Wrapper)
        {
            throw new InvalidOperationException("Before using YYHEggEgg.Shell features, you must use YYHEggEgg.Logger.Log.Initialize with a LoggerConfig that has 'Use_Console_Wrapper = true' specified.");
        }
    }

    /// <summary>
    /// Show all commands' description.
    /// </summary>
    public void ShowHelps()
    {
        lock (_commandHandlers)
        {
            foreach (var handler in _commandHandlers.Values)
            {
                try
                {
                    handler.ShowDescription();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Command '{outCommandName}' failed to load description.", handler.CommandName);
                }
            }
            _logger.LogInformation("Type [command] help to get more detailed usage.");
        }
    }

    private void RefuseCommand(string commandName)
    {
        _logger.LogInformation("Invalid command: {outCommandName}.", commandName);
    }

    /// <summary>
    /// Shutdown the application (for Ctrl+C and <see cref="CommandHandlerBase.CleanUp()"/> handling).
    /// </summary>
    public abstract void Shutdown();

    private void WriteLines(IEnumerable<string>? lines)
    {
        foreach (var line in lines ?? [])
            _logger.LogInformation("{line}", line);
    }

    /// <summary>
    /// The callback when user types 'Enter' and requests
    /// one command to execute.
    /// </summary>
    /// <remarks>
    /// If throwing an exception, the command's execution
    /// will be interrupted; however it won't stop user's
    /// input from entering the history.
    /// </remarks>
    protected virtual void RequestedExecuteCallback(string commandString) { }

    /// <summary>
    /// Run the Command Line.
    /// </summary>
    /// <param name="outerCancellationToken"></param>
    /// <returns></returns>
    public async Task RunAsync(CancellationToken outerCancellationToken = default)
    {
        ThrowIfStarted();
        ConsoleWrapper.ShutDownRequest += (_, _) => Shutdown();
        WriteLines(AppStartNotices);
        if (HelpNotice != null)
            _logger.LogInformation("{notice}", HelpNotice);

        #region Prepare Commands
        Started = true;
        _commandHandlers = new(GetCommandHandlers().Select(x =>
        {
            x.OwnerCli = this;
            return new KeyValuePair<string, CommandHandlerBase>(x.CommandName, x);
        }));
        if (outerCancellationToken != default)
        {
            outerCancellationToken.Register(_commandLineCancellationTokenSource.Cancel);
        }

        #endregion

        #region Auto Complete
        ConsoleWrapper.AutoCompleteHandler = GetDefaultAutoCompleteHandlerCore(_ => true);
        #endregion

        var cancellationToken = _commandLineCancellationTokenSource.Token;
        while (!cancellationToken.IsCancellationRequested)
        {
            WriteLines(StartNewCommandNotices);

            #region Prepare for general operation
            try
            {
                _unknownHandler = RefreshGeneralOperationHandler();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{methodName} failed. General operation and its auto-complete will not be available.", nameof(RefreshGeneralOperationHandler));
            }
            #endregion

            ConsoleWrapper.InputPrefix = WaitingInputPrompt;
            string cmd = (await ConsoleWrapper.ReadLineAsync(true, outerCancellationToken)).Trim();
            if (string.IsNullOrEmpty(cmd)) continue;
            _historyManager?.PushExecuted(cmd);
            try
            {
                RequestedExecuteCallback(cmd);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Rejected command because pre-execution callback threw an exception.");
                continue;
            }

            var tryResult = TryGetCommandHandler(cmd, out var cmdhandle, out var commandName, out var argList);
            if (tryResult != GetCommandHandlerResult.Success)
            {
                continue;
            }
            ConsoleWrapper.InputPrefix = string.Empty;
            await InvokeCommandCoreAsync(cmdhandle, commandName, argList, cancellationToken);
        }
    }

    private GetCommandHandlerResult TryGetCommandHandler(string cmd, out CommandHandlerBase handler,
        out string outCommandName, out string argList)
    {
        handler = null!;
        argList = null!;
        outCommandName = null!;

        var helpstrings = CommandHandlerBase.HelpStrings;
        int sepindex = cmd.IndexOf(' ');
        if (sepindex == -1) sepindex = cmd.Length;
        var commandName = cmd[..sepindex].TrimStart();
        if (helpstrings.Contains(commandName))
        {
            ShowHelps();
            return GetCommandHandlerResult.ShownHelp;
        }
        else
        {
            bool matchedHandler;
            CommandHandlerBase? cmdhandle;
            lock (_commandHandlers)
            {
                matchedHandler = _commandHandlers.TryGetValue(commandName, out cmdhandle);
            }
            try
            {
                if (cmdhandle == null)
                {
                    outCommandName = $"<{_unknownHandler?.CommandName} general operation>";
                    if (_unknownHandler == null)
                    {
                        RefuseCommand(commandName);
                        return GetCommandHandlerResult.Failed;
                    }
                    else
                    {
                        handler = _unknownHandler;
                        argList = cmd;
                        return GetCommandHandlerResult.Success;
                    }
                }

                argList = cmd[Math.Min(cmd.Length, sepindex + 1)..];
                if (helpstrings.Contains(argList.Trim().ToLower()))
                {
                    cmdhandle.ShowUsage();
                    return GetCommandHandlerResult.ShownHelp;
                }
                else
                {
                    handler = cmdhandle;
                    outCommandName = commandName;
                    return GetCommandHandlerResult.Success;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Encountered error when handling command '{outCommandName}' (Get Usage string).", outCommandName);
                return GetCommandHandlerResult.Failed;
            }
        }
    }

    private async Task<bool> InvokeCommandCoreAsync(CommandHandlerBase cmdhandle, string commandName, string argList, CancellationToken cancellationToken)
    {
        try
        {
            return await cmdhandle.HandleAsync(argList, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Encountered error when handling command '{outCommandName}'.", commandName);
            return false;
        }
    }

    private static bool CheckNonUserInvokeCommandPermission(CommandHandlerBase cmdIdentity, string throwExCommandName, Type targetHandlerType, bool throwOnNotPermitted)
    {
        var callAttribute = targetHandlerType.GetCustomAttribute<CommandNonUserCallAttribute>();
        if (callAttribute == null)
        {
            // The default behaviour is not allowing non-user call.
            if (throwOnNotPermitted) throw new RejectedCommandCallException(cmdIdentity.CommandName, throwExCommandName);
            return false;
        }
        var cmdType = cmdIdentity.GetType();
        switch (callAttribute.CallerPolicy)
        {
            case CallerAccess.AllowOtherCommands:
                if (callAttribute.IncludedAccessors?.Contains(cmdType) == true) break;
                else goto case CallerAccess.OnlyUserInput;
            case CallerAccess.AllowEveryone:
                break;
            case CallerAccess.OnlyUserInput:
            default:
                if (throwOnNotPermitted) throw new RejectedCommandCallException(cmdIdentity.CommandName, throwExCommandName, callAttribute.CallerPolicy);
                return false;
        }
        return true;
    }

    internal async Task<bool> NonUserInvokeCommandAsync(CommandHandlerBase cmdIdentity, string cmd, CancellationToken cancellationToken)
    {
        var tryResult = TryGetCommandHandler(cmd, out var handler, out var commandName, out var argList);
        if (tryResult != GetCommandHandlerResult.Success)
        {
            return tryResult != GetCommandHandlerResult.Failed;
        }

        CheckNonUserInvokeCommandPermission(cmdIdentity, commandName, handler.GetType(), true);

        return await InvokeCommandCoreAsync(handler, commandName, argList, cancellationToken);
    }

    /// <summary>
    /// Start <see cref="RunAsync"/> at background.
    /// </summary>
    public void StartBackground()
    {
        ThrowIfStarted();
        Tools.RunBackground(async () =>
        {
            await RunAsync();
        }, "The command line ended unexpectedly.");
    }

    /// <summary>
    /// Invoke each command targetHandler's <see cref="CommandHandlerBase.CleanUp()"/>.
    /// </summary>
    /// <remarks>You can manage the CleanUp order by overriding this method.</remarks>
    protected virtual void PerformCommandsCleanUp()
    {
        foreach (var commandHandler in GetCommandHandlers())
        {
            try
            {
                commandHandler.CleanUp();
            }
            catch (NotImplementedException) { }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cleanup of command {type} failed:.", commandHandler.GetType());
            }
        }
    }

    private IAutoCompleteHandler GetDefaultAutoCompleteHandlerCore(Func<Type, bool> predicate)
    {
        CommandAutoCompleteHandler cmdAutoCmplHandler;
        DispatchOptionsAutoCompleteHandler optionsAutoCmplHandler;
        lock (_commandHandlers)
        {
            var selectedHandlers = from handler in _commandHandlers.Values
                                   let type = handler.GetType()
                                   where predicate(type)
                                   select handler;
            cmdAutoCmplHandler = new(selectedHandlers, () => _unknownHandler);
            optionsAutoCmplHandler = new(selectedHandlers, () => _unknownHandler);
        }
        var autoCmplHandler = new MultipleAutoCompleteChainHandler();
        autoCmplHandler.PushComponent(cmdAutoCmplHandler);
        autoCmplHandler.PushComponent(new FilePathAutoCompleteHandler());
        autoCmplHandler.PushComponent(optionsAutoCmplHandler);
        return autoCmplHandler;
    }

    /// <summary>
    /// Get the auto complete targetHandler at the view of the specified targetHandler.
    /// </summary>
    /// <param name="cmdIdentity">The instance to identify the targetHandler's type.</param>
    /// <returns>A auto complete targetHandler compatiable with all features in the main CLI.</returns>
    /// <remarks>Forwarding handlers use this to get a second-level auto completion. This method is thread-safe.</remarks>
    public IAutoCompleteHandler GetAutoCompleteHandler(CommandHandlerBase cmdIdentity)
    {
        return GetDefaultAutoCompleteHandlerCore(handlerType => CheckNonUserInvokeCommandPermission(cmdIdentity, "", handlerType, false));
    }
}
