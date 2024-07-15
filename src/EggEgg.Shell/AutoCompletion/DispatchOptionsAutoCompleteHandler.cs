using YYHEggEgg.Logger;

namespace YYHEggEgg.Shell.AutoCompletion;

/// <summary>
/// A special type used to dispatch auto completion operation.
/// </summary>
/// <param name="handlers"></param>
public class DispatchOptionsAutoCompleteHandler(IEnumerable<CommandHandlerBase> handlers) : IAutoCompleteHandler
{
    /// <inheritdoc/>
    public SuggestionResult GetSuggestions(string text, int index)
    {
        var separatorIdx = text.IndexOf(' ');
        if (separatorIdx < 0) return new();
        var commandName = text[..separatorIdx];
        var handler = handlers.Where(x => x.CommandName == commandName).FirstOrDefault();
        if (handler == null) return new();
        else return handler.GetSuggestions(text, index);
    }
}
