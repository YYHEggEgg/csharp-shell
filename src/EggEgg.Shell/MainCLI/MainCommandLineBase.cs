using Microsoft.Extensions.Logging;
using YYHEggEgg.Logger;
using YYHEggEgg.Shell.AutoCompletion;

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
    /// Get a fallback handler for 'command name' that doesn't bind to one in <see cref="GetCommandHandlers()"/>.
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// IMPORTANT: It will be invoked EVERY TIME when a new command starts executing, so consider reuse the same instance when implementing. <para/>
    /// Another IMPORTANT: when a general handler's <see cref="CommandHandlerBase.GetSuggestions(string, int)"/>
    /// is invoked, the given <c>text</c> will start with <see cref="CommandHandlerBase.CommandName"/>
    /// (even though user never type them). This is due to compatibility demands
    /// of the method <seealso cref="CommandHandlerBase.GetSuggestions(string, int)"/>.
    /// </remarks>
    protected virtual CommandHandlerBase? RefreshGeneralOperationHandler() => null;
    /// <summary>
    /// Get the list of allowed 'command name' that doesn't bind to one in <see cref="GetCommandHandlers()"/> and can be handled by <see cref="RefreshGeneralOperationHandler()"/>.
    /// </summary>
    /// <remarks>
    /// This list will be used ONLY for auto completion and won't affect what are thrown
    /// to <see cref="RefreshGeneralOperationHandler()"/> (whenever defined, it will handle
    /// all requests failed passed to a registered command handler).<para/>
    /// IMPORTANT: It will be invoked EVERY TIME when a new command starts executing, so consider reuse the same instance when implementing.
    /// </remarks>
    protected virtual IEnumerable<string>? RefreshGeneralOperations() => null;
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

    private CommandAutoCompleteHandler? _cmdAutoCmplHandler;
    private DispatchOptionsAutoCompleteHandler? _optionsAutoCmplHandler;
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
        foreach (var handler in _commandHandlers.Values)
        {
            handler.ShowDescription();
        }
        _logger.LogInformation("Type [command] help to get more detailed usage.");
    }

    private void RefuseCommand(string commandName)
    {
        _logger.LogInformation("Invalid command: {commandName}.", commandName);
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
        _commandHandlers = new(GetCommandHandlers().Select(x => new KeyValuePair<string, CommandHandlerBase>(x.CommandName, x)));
        if (outerCancellationToken != default)
        {
            outerCancellationToken.Register(_commandLineCancellationTokenSource.Cancel);
        }

        #endregion

        #region Auto Complete
        _cmdAutoCmplHandler = new(_commandHandlers.Values, () =>
        {
            try
            {
                return RefreshGeneralOperations();
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex, "{methodName} failed. General operations will not be included in Command Auto Completion.", nameof(RefreshGeneralOperations));
                return null;
            }
        });
        _optionsAutoCmplHandler = new(_commandHandlers.Values, () => _unknownHandler);
        var autoCmplHandler = new MultipleAutoCompleteChainHandler();
        autoCmplHandler.PushComponent(_cmdAutoCmplHandler);
        autoCmplHandler.PushComponent(new FilePathAutoCompleteHandler());
        autoCmplHandler.PushComponent(_optionsAutoCmplHandler);
        ConsoleWrapper.AutoCompleteHandler = autoCmplHandler;
        #endregion

        var helpstrings = CommandHandlerBase.HelpStrings;
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
                _logger.LogError(ex, "{methodName} failed. General operations will not be available.", nameof(RefreshGeneralOperationHandler));
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

            int sepindex = cmd.IndexOf(' ');
            if (sepindex == -1) sepindex = cmd.Length;
            string commandName = cmd[..sepindex];
            if (helpstrings.Contains(commandName))
            {
                ShowHelps();
                continue;
            }
            else
            {
                string argList = cmd[Math.Min(cmd.Length, sepindex + 1)..];
                bool matchedHandler = _commandHandlers.TryGetValue(commandName, out var cmdhandle);
                try
                {
                    if (cmdhandle == null)
                    {
                        if (_unknownHandler == null) RefuseCommand(commandName);
                        else await _unknownHandler.HandleAsync(cmd);
                        continue;
                    }

                    if (helpstrings.Contains(argList.Trim().ToLower()))
                    {
                        cmdhandle.ShowUsage();
                    }
                    else
                    {
                        ConsoleWrapper.InputPrefix = string.Empty;
                        await cmdhandle.HandleAsync(argList);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Encountered error when handling command '{commandName}'.",
                        matchedHandler ? commandName : $"<{_unknownHandler?.CommandName} general operation>");
                }
            }
        }
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
    /// Invoke each command handler's <see cref="CommandHandlerBase.CleanUp()"/>.
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
}
