using CommandLine;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using YYHEggEgg.Logger;
using YYHEggEgg.Shell.Attributes;
using YYHEggEgg.Shell.Exceptions;
using YYHEggEgg.Shell.MainCLI;
using YYHEggEgg.Shell.Model;

namespace YYHEggEgg.Shell;

/// <summary>
/// The basic definition of all command handlers.
/// </summary>
/// <param name="logger"></param>
public abstract class CommandHandlerBase(ILogger logger) : IAutoCompleteHandler
{
    /// <inheritdoc cref="CommandHandlerBase(ILogger)"/>
    public CommandHandlerBase() : this(null!)
    {
        _logger = new EggEggLogger(CommandName);
    }

    /// <summary>
    /// Parse <paramref name="cmd"/> to list of args in the
    /// command line parsing rules used by Microsoft C/C++ code.
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns></returns>
    /// <remarks><seealso href="https://learn.microsoft.com/en-us/cpp/cpp/main-function-command-line-args?view=msvc-170#parsing-c-command-line-arguments">Parsing C++ command-line arguments</seealso></remarks>
    public static CmdLineParseResult ParseCmd(string cmd)
    {
        var argv = new List<string>();
        var rawStrings = new List<CmdLineRawString>();
        var currentArg = new StringBuilder();
        var currentRawString = new StringBuilder();
        var curStartIndex = 0;
        bool inQuotes = false;
        int backslashCount = 0;

        for (int i = 0; i < cmd.Length; i++)
        {
            char c = cmd[i];

            if (c == '\\')
            {
                backslashCount++;
            }
            else if (c == '"')
            {
                if (backslashCount % 2 == 0)
                {
                    inQuotes = !inQuotes;
                    // Add escaped quotes if inQuotes was true
                    if (inQuotes && i > 0 && cmd[i - 1] == '"')
                    {
                        currentArg.Append('"');
                    }
                    // Add half of the backslashes that are not escaping the quote
                    currentArg.Append('\\', backslashCount / 2);
                    backslashCount = 0;
                }
                else
                {
                    // Odd number of backslashes means the quote is escaped
                    currentArg.Append('\\', backslashCount / 2);
                    currentArg.Append('"');
                    backslashCount = 0;
                }
            }
            else
            {
                if (backslashCount > 0)
                {
                    currentArg.Append('\\', backslashCount);
                    backslashCount = 0;
                }

                if (char.IsWhiteSpace(c) && !inQuotes)
                {
                    if (currentArg.Length > 0)
                    {
                        argv.Add(currentArg.ToString());
                        rawStrings.Add(new(currentRawString.ToString(), curStartIndex));
                        currentArg.Clear();
                        currentRawString.Clear();
                        curStartIndex = i + 1;
                    }
                    else
                    {
                        curStartIndex++;
                    }
                    continue;
                }
                else
                {
                    currentArg.Append(c);
                }
            }

            currentRawString.Append(c);
        }

        if (backslashCount > 0)
        {
            currentArg.Append('\\', backslashCount);
        }

        if (currentArg.Length > 0)
        {
            argv.Add(currentArg.ToString());
        }
        if (currentRawString.Length > 0)
        {
            rawStrings.Add(new(currentRawString.ToString(), curStartIndex));
        }

        return new()
        {
            ParsedArgv = argv,
            RawStringInfos = rawStrings,
        };
    }

    /// <inheritdoc cref="ParseCmd(string)"/>
    public static List<string> ParseAsArgs(string cmd) => ParseCmd(cmd).ParsedArgv;

    /// <summary>
    /// The Command Name for user to invoke your command (the first word in their command).
    /// </summary>
    public abstract string CommandName { get; }
    /// <summary>
    /// Logger.
    /// </summary>
    protected ILogger _logger = logger;
    /// <summary>
    /// A brief and one-line description of this command.
    /// </summary>
    public abstract string Description { get; }
    /// <summary>
    /// Basic usage of this command. You can put multiple lines here.
    /// </summary>
    public abstract IEnumerable<string> UsageLines { get; }
    /// <summary>
    /// Command Handler.
    /// </summary>
    /// <param name="argList">The received args, without the precending <see cref="CommandName"/>.</param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// Command execution will be canceled if:<para/>
    /// - The program is closing (though it's not guaranteed that a graceful cancellation can always be performed);<para/>
    /// - The non-user executor requested this to be canceled.
    /// </param>
    /// <returns>Whether this command succeeded to recognize input and finish its task. This return type doesn't mean inheritor shouldn't throw an exception when meeting failure.</returns>
    public abstract Task<bool> HandleAsync(string argList, CancellationToken cancellationToken = default);
    /// <summary>
    /// Perform essential CleanUp while program is exiting gracefully.
    /// </summary>
    /// <remarks>
    /// We see Ctrl+C, <see cref="Environment.Exit(int)"/> as graceful
    /// exit. If you're using a Hosting version, then the point is whether
    /// the operation can trigger <c>IHostApplicationLifetime</c>'s
    /// stopping application.
    /// </remarks>
    public virtual void CleanUp() { }

    internal MainCommandLineBase? OwnerCli = null;

    /// <summary>
    /// Show command name and description in one line.
    /// </summary>
    public virtual void ShowDescription()
    {
        _logger.LogInformation("Command '{name}': {desc}", CommandName, Description);
    }

    /// <summary>
    /// Show description and usage.
    /// </summary>
    public virtual void ShowUsage()
    {
        ShowDescription();
        foreach (var line in UsageLines)
            _logger.LogInformation("{text}", line);
    }

    /// <summary>
    /// The CommandLineParser used by all command handlers.
    /// Use this when you're outputing parse error by yourself.
    /// </summary>
    public readonly static Parser DefaultCommandsParser = new(config =>
    {
        // 在构建时设置自定义 ConsoleWriter
        config.HelpWriter = Tools.NothingWriter;
    });

    /// <summary>
    /// The hardcoded string that are considered as seeking 'help'.
    /// </summary>
    public static readonly ReadOnlyCollection<string> HelpStrings = new(new string[]
    {
        "help", "?", "--help", "-h", "-?", "/h", "/?"
    });

    /// <summary>
    /// The method to output <see cref="Error"/> in log
    /// rather than original Parser. Should be used along
    /// with <see cref="DefaultCommandsParser"/>.
    /// </summary>
    /// <param name="errors"></param>
    protected void OutputInvalidUsage(IEnumerable<Error> errors)
    {
        foreach (var line in Tools.ReportCommandLineErrors(errors))
            _logger.LogWarning("{warn}", line ?? string.Empty);
        _logger.LogError("Invalid input for param. Please view the errors and check your input.");
    }

    /// <summary>
    /// Get suggestions for this command. It is guaranteed
    /// that when it's invoked, the user is surely typing
    /// parameters for THIS command.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public virtual SuggestionResult GetSuggestions(string text, int index)
    {
        return new();
    }

    /// <summary>
    /// Generate help result from one property with either
    /// <see cref="OptionAttribute"/> or <see cref="ValueAttribute"/>.
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    protected OptionHelpResult? GenerateFromCmdOption(PropertyInfo property)
    {
        var optionAttr = property.GetCustomAttribute<OptionAttribute>();
        var valueAttr = property.GetCustomAttribute<ValueAttribute>();
        OptionHelpResult helpResult;
        if (optionAttr != null)
        {
            if (optionAttr.Hidden) return null;
            helpResult = new()
            {
                MetaName = optionAttr.MetaValue,
                HelpText = optionAttr.HelpText,
                IsRequired = optionAttr.Required,
            };

            if (string.IsNullOrEmpty(optionAttr.ShortName))
                helpResult.OptionString = $"--{optionAttr.LongName}";
            else
            {
                helpResult.OptionString = $"-{optionAttr.ShortName}";
                if (!string.IsNullOrEmpty(optionAttr.LongName))
                    helpResult.OptionString += $", --{optionAttr.LongName}";
            }
        }
        else if (valueAttr != null)
        {
            if (valueAttr.Hidden) return null;
            helpResult = new()
            {
                MetaName = valueAttr.MetaName,
                HelpText = valueAttr.HelpText,
                IsRequired = valueAttr.Required,
            };
            if (string.IsNullOrEmpty(helpResult.MetaName))
            {
                if (!string.IsNullOrEmpty(valueAttr.MetaValue))
                    helpResult.MetaName = valueAttr.MetaValue;
            }
        }
        else return null;
        if (string.IsNullOrEmpty(helpResult.MetaName))
            helpResult.MetaName = GetMetaTypeFromType(property.PropertyType, helpResult.OptionString == null);
        return helpResult;
    }

    /// <summary>
    /// Calculate the precending length (without <see cref="OptionHelpResult.HelpText"/>).
    /// </summary>
    /// <param name="helpResult"></param>
    /// <returns></returns>
    protected int CalculatePrecendingLength(OptionHelpResult helpResult)
    {
        var precendingLength = helpResult.MetaName?.Length ?? -1;
        if (!string.IsNullOrEmpty(helpResult.OptionString))
        {
            // -o, --output <path>
            // [--example <value>]
            precendingLength += helpResult.OptionString.Length + 1 + 2;
            if (!helpResult.IsRequired) precendingLength += 2;
        }
        else
        {
            // <value>
            // [path]
            precendingLength += 2;
        }
        return precendingLength;
    }

    /// <summary>
    /// Construct a line of usage about this option.
    /// It doesn't case <see cref="OptionHelpResult.AdditionalHelps"/>.
    /// </summary>
    /// <param name="helpResult"></param>
    /// <param name="precendingLength"></param>
    /// <returns></returns>
    protected string ConstructHelpLine(OptionHelpResult helpResult, int precendingLength)
    {
        StringBuilder sb = new();
        if (!helpResult.IsRequired) sb.Append('[');
        if (!string.IsNullOrEmpty(helpResult.OptionString))
        {
            sb.Append(helpResult.OptionString);
            if (helpResult.MetaName != null)
            {
                sb.Append(" <");
                sb.Append(helpResult.MetaName);
                sb.Append('>');
            }
        }
        else
        {
            if (helpResult.IsRequired) sb.Append('<');
            sb.Append(helpResult.MetaName);
            if (helpResult.IsRequired) sb.Append('>');
        }
        if (!helpResult.IsRequired) sb.Append(']');

        sb.Append(' ', precendingLength - sb.Length);
        sb.Append("  ");
        sb.Append(helpResult.HelpText);
        return sb.ToString();
    }

    /// <summary>
    /// Get the usage of options defined in one type.
    /// </summary>
    /// <param name="cmdOptionType"></param>
    /// <returns></returns>
    protected IEnumerable<string> GetCmdOptionsHelpStrings([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] Type cmdOptionType)
    {
        var properties = cmdOptionType.GetProperties();
        int maxPrecendingLength = 0;
        List<OptionHelpResult> options = [];
        foreach (var property in properties)
        {
            var helpResult = GenerateFromCmdOption(property);
            if (helpResult == null) continue;
            helpResult = CustomizeOptionHelpResult(helpResult) ?? helpResult;
            foreach (var replacedHelpResult in helpResult.ReplaceWith ?? [helpResult])
            {
                maxPrecendingLength = Math.Max(maxPrecendingLength, CalculatePrecendingLength(helpResult));
                options.Add(helpResult);
            }
        }
        foreach (var optionResult in options)
        {
            yield return ConstructHelpLine(optionResult, maxPrecendingLength);
            if (optionResult.AdditionalHelps != null)
            {
                foreach (var addLine in optionResult.AdditionalHelps)
                {
                    yield return addLine.PadLeft(maxPrecendingLength + 2 + addLine.Length);
                }
            }
        }
    }

    /// <summary>
    /// Override this method to customize help text of some options.
    /// Return null to use the default one;
    /// set <see cref="OptionHelpResult.ReplaceWith"/> to empty enumerable
    /// to hide this option (you can reach this by returning <c>[]</c> in
    /// the latest C# lang version).
    /// </summary>
    /// <param name="help"></param>
    /// <returns></returns>
    protected virtual OptionHelpResult? CustomizeOptionHelpResult(OptionHelpResult help) => null;

    /// <summary>
    /// Get the default meta type from a option type (used if not specified).
    /// </summary>
    /// <param name="type"></param>
    /// <param name="isValue"></param>
    /// <returns></returns>
    protected virtual string? GetMetaTypeFromType(Type type, bool isValue)
    {
        return type.Name switch
        {
            "String" => "string",
            "Byte" or "SByte" or "Int16" or "UInt16" or "Int32" or "UInt32" or "Int64" or "UInt64" or "Int128" or "UInt128" => "integer",
            "Single" or "Double" or "Decimal" => "number",
            "IEnumerable`1" => $"{GetMetaTypeFromType(type.GenericTypeArguments[0], true)}s",
            "Boolean" => isValue ? "true|false" : null,
            _ => "value",
        };
    }

    /// <summary>
    /// Invoke another command with the raw command input.
    /// </summary>
    /// <param name="cmd">The raw command input, with full command name and options.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    /// Whether the command succeeded. Notice that when the command don't want to
    /// be invoked from non-user caller, a <see cref="RejectedCommandCallException"/>
    /// is thrown instead of returning false.
    /// </returns>
    /// <remarks>
    /// This method is thread-safe. However, if you mark your command handler with
    /// <see cref="CommandNonUserCallAttribute"/>, theoritically you should
    /// guarantee your <see cref="HandleAsync(string, CancellationToken)"/>
    /// implementation is thread-safe as well.
    /// </remarks>
    /// <exception cref="RejectedCommandCallException">
    /// The invoked command rejected invocation because it is configured to do so.
    /// </exception>
    protected async Task<bool> InvokeCommandAsync(string cmd, CancellationToken cancellationToken = default)
    {
        if (OwnerCli == null)
        {
            throw new InvalidOperationException("You must register Command Handler to a MainCommandLineBase and run it before invoking other commands.");
        }
        return await OwnerCli.NonUserInvokeCommandAsync(this, cmd, cancellationToken);
    }
}
