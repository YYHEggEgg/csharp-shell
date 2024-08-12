using CommandLine;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using YYHEggEgg.Logger;
using YYHEggEgg.Shell.Attributes;
using YYHEggEgg.Shell.AutoCompletion;
using YYHEggEgg.Shell.Model;

namespace YYHEggEgg.Shell;

/// <summary>
/// A more standard command forwarder using <see cref="Parser"/>.
/// <seealso cref="CommandForwarderBase">What's a Command Forwarder</seealso>
/// </summary>
/// <typeparam name="TCmdOption"></typeparam>
public abstract class StandardCommandForwarder<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TCmdOption>(ILogger logger) : CommandForwarderBase(logger)
{
    /// <inheritdoc/>
    public StandardCommandForwarder() : this(null!)
    {
        _logger = new EggEggLogger(CommandName ?? "<unknown command>");
    }

    /// <summary>
    /// Command forwarding Handler, using <typeparamref name="TCmdOption"/>.
    /// </summary>
    /// <returns></returns>
    /// <inheritdoc cref="HandleAsync(string, string?, CancellationToken)"/>
    public abstract Task<bool> HandleAsync(TCmdOption o, string? forwardedCmd, CancellationToken cancellationToken = default);

    /// <summary>
    /// Handle the errors happened in the command args parsing. In most cases, it don't need to be overriden.
    /// </summary>
    /// <param name="errors"></param>
    public virtual void HandleInvalidUsage(IEnumerable<Error> errors)
    {
        OutputInvalidUsage(errors);
        ShowUsage();
        throw new AccessViolationException();
    }

    /// <summary>
    /// Check if <paramref name="forwardedCmd"/>'s status is acceptable under
    /// <typeparamref name="TCmdOption"/>'s <see cref="RequiresForwardCmdAttribute"/>,
    /// and output warnings or interrupt the process.
    /// </summary>
    /// <param name="forwardedCmd"></param>
    /// <returns>Whether the process should continue.</returns>
    protected virtual bool ForwardCmdArgumentShield(string? forwardedCmd)
    {
        var attr = typeof(TCmdOption).GetCustomAttribute<RequiresForwardCmdAttribute>();
        if (attr == null) return true;
        switch (attr.AllowForwardCmd)
        {
            case ArgumentStatus.Disallowed:
                if (!string.IsNullOrWhiteSpace(forwardedCmd))
                    _logger.LogWarning("This command expects no value for forwarded command, but it is provided. Content after separator '{separator}' will be ignored.", Separator);
                break;
            case ArgumentStatus.Required:
                if (string.IsNullOrWhiteSpace(forwardedCmd))
                {
                    _logger.LogError("This command requires value for forwarded command, but it does not exist. Please join separator '{separator}' and add a command after it.", Separator);
                    return false;
                }
                break;
        }
        return true;
    }

    private async Task<bool> HandleForwardShieldAsync(TCmdOption o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        if (!ForwardCmdArgumentShield(forwardedCmd)) return false;
        return await HandleAsync(o, forwardedCmd, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        var args = ParseAsArgs(argList);
        var cmd_parser = DefaultCommandsParser;
        bool result = false;
        try
        {
            await cmd_parser.ParseArguments<TCmdOption>(args)
                .WithNotParsed(errs => HandleInvalidUsage(errs))
                .WithParsedAsync(async (opt) =>
                {
                    result = await HandleForwardShieldAsync(opt, forwardedCmd, cancellationToken);
                });
        }
        catch (AccessViolationException) { }
        return result;
    }

    private readonly OptionsAutoCompleteHandler<TCmdOption> _standardAutoCompleteHandler = new();

    /// <inheritdoc/>
    public override SuggestionResult GetSuggestionsForOptions(string text, int index)
    {
        return _standardAutoCompleteHandler.GetSuggestions(text, index);
    }

    /// <inheritdoc/>
    public override IEnumerable<string> UsageLines
    {
        get
        {
            yield return GetCommandUsageIntroLine(typeof(TCmdOption), CommandName, Separator);
            foreach (var helpline in GetCmdOptionsHelpStrings(typeof(TCmdOption)))
            {
                yield return $"  {helpline}";
            }
            yield return string.Empty;
            foreach (var line in AdditionalDescLines ?? [])
                yield return line;
        }
    }

    /// <summary>
    /// The additional description of this command. Added to the end of <see cref="UsageLines"/>.
    /// </summary>
    protected virtual IEnumerable<string>? AdditionalDescLines => null;
}
