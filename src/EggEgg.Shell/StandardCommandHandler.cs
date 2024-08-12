using CommandLine;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using YYHEggEgg.Logger;
using YYHEggEgg.Shell.AutoCompletion;

namespace YYHEggEgg.Shell;

/// <summary>
/// A more standard command handler using <see cref="Parser"/>.
/// </summary>
/// <typeparam name="TCmdOption"></typeparam>
public abstract class StandardCommandHandler<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TCmdOption>(ILogger logger) : CommandHandlerBase(logger)
{
    /// <inheritdoc/>
    public StandardCommandHandler() : this(null!)
    {
        _logger = new EggEggLogger(CommandName ?? "<unknown command>");
    }

    /// <summary>
    /// Command Handler, using <typeparamref name="TCmdOption"/>.
    /// </summary>
    /// <param name="o"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task<bool> HandleAsync(TCmdOption o, CancellationToken cancellationToken = default);

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

    /// <inheritdoc/>
    public override async Task<bool> HandleAsync(string argList, CancellationToken cancellationToken = default)
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
                    result = await HandleAsync(opt, cancellationToken);
                });
        }
        catch (AccessViolationException) { }
        return result;
    }

    private readonly OptionsAutoCompleteHandler<TCmdOption> _standardAutoCompleteHandler = new();

    /// <inheritdoc/>
    public override SuggestionResult GetSuggestions(string text, int index)
    {
        return _standardAutoCompleteHandler.GetSuggestions(text, index);
    }

    /// <inheritdoc/>
    public override IEnumerable<string> UsageLines
    {
        get
        {
            yield return $"{CommandName} [options]";
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
