using YYHEggEgg.Logger;
using YYHEggEgg.Shell.MainCLI;

namespace YYHEggEgg.Shell.AutoCompletion;

/// <summary>
/// Complete commands (the first word).
/// </summary>
/// <param name="commands"></param>
/// <param name="getAdditionalAllowedNamesCallback">
/// The additionally allowed names (though not commands).
/// Used for <see cref="MainCommandLineBase.RefreshGeneralOperationHandler()"/>.
/// </param>
public class CommandAutoCompleteHandler(IEnumerable<CommandHandlerBase> commands, Func<IEnumerable<string>?> getAdditionalAllowedNamesCallback) : IAutoCompleteHandler
{
    private readonly IEnumerable<string> _commandNames = commands.Select(x => x.CommandName);

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
        var additionalAllowedNames = getAdditionalAllowedNamesCallback();
        if (additionalAllowedNames != null)
        {
            matches = matches.Concat(MatchByName(additionalAllowedNames, text, index));
        }
        return new()
        {
            Suggestions = matches.ToList(),
        };
    }
}
