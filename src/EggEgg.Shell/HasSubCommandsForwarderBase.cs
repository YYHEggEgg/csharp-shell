using CommandLine;
using Microsoft.Extensions.Logging;
using System.Reflection;
using YYHEggEgg.Logger;
using YYHEggEgg.Shell.Attributes;
using YYHEggEgg.Shell.AutoCompletion;
using YYHEggEgg.Shell.Model;

namespace YYHEggEgg.Shell;

/// <summary>
/// Define the base of command forwarder with multiple subcommands.
/// <seealso cref="CommandForwarderBase">What's a Command Forwarder</seealso>
/// </summary>
public abstract class HasSubCommandsForwarderBase : CommandForwarderBase
{
    private readonly Dictionary<string, OptionsAutoCompleteHandler> _autoCmplHandlersMap;
    private readonly Dictionary<string, string> _subCommandAliasesMap;
    private readonly SuggestionSlave _privateSlave;
    /// <summary>
    /// The provided type parameters of option types.
    /// </summary>
    protected readonly IEnumerable<Type> _subCommandOptionTypes;
    internal HasSubCommandsForwarderBase(ILogger logger, IEnumerable<Type> optionTypes)
        : base(logger)
    {
        _privateSlave = new(CommandName, logger, optionTypes);
        _autoCmplHandlersMap = [];
        _subCommandAliasesMap = [];
        _subCommandOptionTypes = optionTypes;
        InitializeSubCommands(optionTypes);
    }

    private void InitializeSubCommands(IEnumerable<Type> optionTypes)
    {
        foreach (var optionType in optionTypes)
        {
            var verbAttr = optionType.GetCustomAttribute<VerbAttribute>() ?? throw new ArgumentException("Provided an option type that doesn't define VerbAttribute.", nameof(optionTypes));
            var subCommandName = verbAttr.Name;
            var aliases = verbAttr.Aliases.ToList();
            aliases.Add(subCommandName);
            if (verbAttr.IsDefault)
                aliases.Add(string.Empty);
            foreach (var alias in aliases)
                _subCommandAliasesMap.Add(alias, subCommandName);

            _autoCmplHandlersMap.Add(subCommandName, new OptionsAutoCompleteHandler(optionType));
        }
    }

    /// <inheritdoc/>
    public override SuggestionResult GetSuggestionsForOptions(string text, int index)
    {
        return _privateSlave.GetSuggestions(text, index);
    }

    /// <summary>
    /// The additional descriptions of subcommands, key is <see cref="VerbAttribute.Name"/>. Added after the usage of one subcommand.
    /// </summary>
    protected virtual Dictionary<string, IEnumerable<string>>? SubcommandAdditionalDescLinesMap => null;
    /// <summary>
    /// The additional description of this (whole) command. Added to the end of <see cref="UsageLines"/>.
    /// </summary>
    protected virtual IEnumerable<string>? AdditionalDescLines => null;

    /// <inheritdoc/>
    public override IEnumerable<string> UsageLines
    {
        get
        {
            yield return $"{CommandName} <command> [options]";
            bool first = true;
            foreach (var optionType in _subCommandOptionTypes)
            {
                if (first) first = false;
                else
                {
                    yield return "";
                    first = false;
                }

                var verbAttribute = optionType.GetCustomAttribute<VerbAttribute>();
                if (verbAttribute == null) continue;
                yield return HasSubCommandsHandlerBase.GetVerbIntroLine(verbAttribute);
                yield return $"    {GetCommandUsageIntroLine(optionType, $"{CommandName} {verbAttribute.Name}", Separator)}";

                foreach (var helpline in GetCmdOptionsHelpStrings(optionType))
                {
                    yield return $"      {helpline}";
                }
                // yield return string.Empty;
                if (SubcommandAdditionalDescLinesMap?.TryGetValue(verbAttribute.Name, out var descLines) == true)
                {
                    foreach (var line in descLines ?? [])
                        yield return line;
                }
            }
            yield return string.Empty;
            foreach (var line in AdditionalDescLines ?? [])
                yield return line;
        }
    }

    /// <inheritdoc cref="StandardCommandForwarder{TCmdOption}.ForwardCmdArgumentShield(string?)"/>
    protected virtual bool ForwardCmdArgumentShield<TCmdOption>(string? forwardedCmd)
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

    [DoNotRegisterCommand]
    private class SuggestionSlave(string commandName, ILogger logger, IEnumerable<Type> optionTypes) : HasSubCommandsHandlerBase(logger, optionTypes)
    {
        public override string CommandName => commandName;

        public override string Description => throw new NotImplementedException();

        public override Task<bool> HandleAsync(string argList, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
