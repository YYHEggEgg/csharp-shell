using YYHEggEgg.Logger;

namespace YYHEggEgg.Shell.AutoCompletion;

/// <summary>
/// Complete commands (the first word).
/// </summary>
/// <param name="commands"></param>
/// <param name="getGeneralOpHandlerCallback"></param>
public class CommandAutoCompleteHandler(IEnumerable<CommandHandlerBase> commands, Func<CommandHandlerBase?> getGeneralOpHandlerCallback) : IAutoCompleteHandler
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

        var generalOpHandler = getGeneralOpHandlerCallback();
        if (generalOpHandler != null)
        {
            var generalOpAlias = generalOpHandler.CommandName;
            var res = generalOpHandler.GetSuggestions($"{generalOpAlias} {text}", index);
            res.StartIndex -= generalOpAlias.Length;
            res.EndIndex -= generalOpAlias.Length;
            if (res.StartIndex >= 0 && res.Suggestions != null)
            {
                matches = matches.Concat(res.Suggestions);
            }
        }
        return new()
        {
            Suggestions = matches.ToList(),
        };
    }
}
