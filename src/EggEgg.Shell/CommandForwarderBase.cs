using Microsoft.Extensions.Logging;
using System.Reflection;
using YYHEggEgg.Logger;
using YYHEggEgg.Shell.Attributes;
using YYHEggEgg.Shell.Model;

namespace YYHEggEgg.Shell;

/// <summary>
/// Defines a handler for forwarding command, using a separator
/// (like <c>--</c>) to separate options and a raw command for
/// forwarding commands via
/// <see cref="CommandHandlerBase.InvokeCommandAsync(string, CancellationToken)"/>.
/// </summary>
public abstract class CommandForwarderBase(ILogger logger) : CommandHandlerBase(logger)
{
    /// <summary>
    /// Get the index of two halfs in a separator model. CmdHalfStartIndex will be -1 if separator does not exist here.
    /// </summary>
    public static (int OptionHalfEndIndex, int CmdHalfStartIndex) GetSeparatorIndexes(string argList, string separator)
    {
        var args = ParseCmd(argList);
        for (int i = 0; i < args.RawStringInfos.Count; i++)
        {
            var rawStringInfo = args.RawStringInfos[i];
            if (rawStringInfo.RawString == separator)
            {
                var optionHalfEndIndex = rawStringInfo.StartIndex;
                while (optionHalfEndIndex >= 0 && argList[optionHalfEndIndex] == ' ')
                {
                    optionHalfEndIndex--;
                }
                var cmdHalfStartIndex = rawStringInfo.EndIndex;
                if (cmdHalfStartIndex == argList.Length)
                    return (optionHalfEndIndex, -1);
                while (cmdHalfStartIndex < argList.Length && argList[cmdHalfStartIndex] == ' ')
                {
                    cmdHalfStartIndex++;
                }
                return (optionHalfEndIndex, cmdHalfStartIndex);
            }
        }
        return (argList.Length, -1);
    }

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, CancellationToken cancellationToken = default)
    {
        (var optionHalfEndIndex, var cmdHalfStartIndex) = GetSeparatorIndexes(argList, Separator);
        if (cmdHalfStartIndex > 0)
            return await HandleAsync(argList[..optionHalfEndIndex].Trim(), argList[cmdHalfStartIndex..].Trim(), cancellationToken);
        else return await HandleAsync(argList.Trim(), null, cancellationToken);
    }

    /// <summary>
    /// The separator between forward options and the cmd being forwarded.
    /// </summary>
    public virtual string Separator => "--";
    /// <summary>
    /// Handle the forward request.
    /// </summary>
    /// <param name="optionArgList">The forward options, defined before separator.</param>
    /// <param name="forwardedCmd">The cmd requested to be forwarded, defined after separator.</param>
    /// <param name="cancellationToken"></param>
    /// <inheritdoc cref="CommandHandlerBase.HandleAsync(string, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(string optionArgList, string? forwardedCmd, CancellationToken cancellationToken = default);

    private IAutoCompleteHandler? _glbForwardAutoCompleteHandler;

    /// <inheritdoc/>
    public override SuggestionResult GetSuggestions(string text, int index)
    {
        (var optionHalfEndIndex, var cmdHalfStartIndex) = GetSeparatorIndexes(text, Separator);
        var forwardToGlobalMode = cmdHalfStartIndex >= 0 && index >= cmdHalfStartIndex;
        if (forwardToGlobalMode)
        {
            _glbForwardAutoCompleteHandler ??= OwnerCli?.GetAutoCompleteHandler(this);
            var result = _glbForwardAutoCompleteHandler?.GetSuggestions(text[cmdHalfStartIndex..], index - cmdHalfStartIndex);
            if (result?.Suggestions == null || result.Suggestions.Count == 0) return new();
            return new()
            {
                StartIndex = result.StartIndex + cmdHalfStartIndex,
                EndIndex = result.EndIndex < 0 ? -1 : result.EndIndex + cmdHalfStartIndex,
                Suggestions = result.Suggestions,
            };
        }
        else
        {
            if (index > optionHalfEndIndex) return new();
            var result = GetSuggestionsForOptions(text[..optionHalfEndIndex], index);
            if (result?.Suggestions == null || result.Suggestions.Count == 0) return new();
            return new()
            {
                StartIndex = result.StartIndex,
                EndIndex = (result.EndIndex < 0 || result.EndIndex > optionHalfEndIndex) ? optionHalfEndIndex : result.EndIndex,
                Suggestions = result.Suggestions,
            };
        }
    }

    internal static string GetCommandUsageIntroLine(Type optionType, string commandName, string separator)
    {
        var result = $"{commandName} [options]";
        var attr = optionType.GetCustomAttribute<RequiresForwardCmdAttribute>();
        if (attr != null)
        {
            switch (attr.AllowForwardCmd)
            {
                case ArgumentStatus.Optional:
                    result += $" [{separator} <command>]";
                    break;
                case ArgumentStatus.Required:
                    result += $" {separator} <command>";
                    break;
            }
        }
        else
        {
            // Treat as Optional
            result += $" [{separator} <command>]";
        }
        return result;
    }

    /// <summary>
    /// Get suggestions specially for this command's options. Raw cmd after separator will not be included.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public virtual SuggestionResult GetSuggestionsForOptions(string text, int index)
    {
        return new();
    }
}
