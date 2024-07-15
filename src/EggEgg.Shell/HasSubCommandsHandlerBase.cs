using CommandLine;
using Microsoft.Extensions.Logging;
using System.Reflection;
using YYHEggEgg.Logger;
using YYHEggEgg.Shell.AutoCompletion;

namespace YYHEggEgg.Shell;

/// <summary>
/// Define the base of handlers with multiple subcommands.
/// </summary>
public abstract class HasSubCommandsHandlerBase : CommandHandlerBase
{
    private readonly Dictionary<string, OptionsAutoCompleteHandler> _autoCmplHandlersMap;
    private readonly Dictionary<string, string> _subCommandAliasesMap;
    /// <summary>
    /// The provided type parameters of option types.
    /// </summary>
    protected readonly IEnumerable<Type> _subCommandOptionTypes;
    internal HasSubCommandsHandlerBase(ILogger logger, IEnumerable<Type> optionTypes)
        : base(logger)
    {
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
    public override SuggestionResult GetSuggestions(string text, int index)
    {
        var args = ParseAsArgs(text);
        if (index <= args[0].Length) return new();
        var subCommand = args.Count > 1 ? args[1] : string.Empty;

        if (args.Count > 2 || (args.Count == 2 && text[index - 1] == ' '))
        {
            if (!_subCommandAliasesMap.TryGetValue(subCommand, out var subCommandDef) && !_subCommandAliasesMap.TryGetValue(string.Empty, out subCommandDef))
                return new();
            var subCommandHandler = _autoCmplHandlersMap[subCommandDef];
            return subCommandHandler.GetSuggestions(text, index);
        }

        // Fill out subcommand
        var subStartLimit = text[(args[0].Length + 1)..index];
        var subEndLimit = text[index..];
        return new SuggestionResult
        {
            Suggestions = (from subCommandName in _autoCmplHandlersMap.Keys
                           where subCommandName.StartsWith(subStartLimit) && subCommandName.EndsWith(subEndLimit)
                           select subCommandName).ToList(),
            StartIndex = args[0].Length + 1,
            EndIndex = -1,
        };
    }

    /// <summary>
    /// The additional descriptions of subcommands, key is <see cref="VerbAttribute.Name"/>. Added after the usage of one subcommand.
    /// </summary>
    protected virtual Dictionary<string, IEnumerable<string>>? SubcommandAdditionalDescLinesMap => null;
    /// <summary>
    /// The additional description of this (whole) command. Added to the end of <see cref="UsageLines"/>.
    /// </summary>
    protected virtual IEnumerable<string>? AdditionalDescLines => null;

    /// <summary>
    /// Get a description of one subcommand.
    /// </summary>
    /// <param name="verbAttribute"></param>
    /// <returns></returns>
    protected string GetVerbIntroLine(VerbAttribute verbAttribute)
    {
        var introLine = $"  command {verbAttribute.Name}";
        if (verbAttribute.Aliases != null && verbAttribute.Aliases.Length > 0)
        {
            introLine += $", {string.Join(", ", verbAttribute.Aliases)}";
        }
        if (verbAttribute.IsDefault) introLine += " (default):";
        else introLine += ":";
        return introLine;
    }

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
                yield return GetVerbIntroLine(verbAttribute);

                foreach (var helpline in GetCmdOptionsHelpStrings(optionType))
                {
                    yield return $"    {helpline}";
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
}
