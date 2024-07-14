using YYHEggEgg.Logger;
using YYHEggEgg.Shell.MainCLI;

namespace YYHEggEgg.Shell.AutoCompletion;

/// <summary>
/// Complete commands (the first word).
/// </summary>
/// <param name="commands"></param>
public class CommandAutoCompleteHandler(IEnumerable<CommandHandlerBase> commands) : IAutoCompleteHandler
{
    private readonly IEnumerable<string> _commandNames = commands.Select(x => x.CommandName);
    /// <summary>
    /// The additionally allowed names (though not commands).
    /// Used for <see cref="MainCommandLineBase.GetGeneralOperationHandler()"/>.
    /// </summary>
    public IEnumerable<string>? AdditionalAllowedNames { get; set; }

    private static IEnumerable<string> MatchByName(IEnumerable<string> strings, string text, int index)
    {
        var start = text[..index];
        var end = text[index..];
        return from str in strings
               where str.StartsWith(start) && str.EndsWith(end)
               select str;
    }

    /// <inheritdoc/>
    public SuggestionResult GetSuggestions(string text, int index)
    {
        if (text.Contains(' ')) return new();
        var matches = MatchByName(_commandNames, text, index);
        if (AdditionalAllowedNames != null)
        {
            matches = matches.Concat(MatchByName(AdditionalAllowedNames, text, index));
        }
        return new()
        {
            Suggestions = matches.ToList(),
        };
    }
}
