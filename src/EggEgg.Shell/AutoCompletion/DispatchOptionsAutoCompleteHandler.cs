using YYHEggEgg.Logger;
using YYHEggEgg.Shell.MainCLI;

namespace YYHEggEgg.Shell.AutoCompletion;

/// <summary>
/// A special type used to dispatch auto completion operation.
/// </summary>
/// <param name="handlers"></param>
/// <param name="getGeneralOpHandlerCallback"></param>
public class DispatchOptionsAutoCompleteHandler(IEnumerable<CommandHandlerBase> handlers, Func<CommandHandlerBase?> getGeneralOpHandlerCallback) : IAutoCompleteHandler
{
    /// <inheritdoc/>
    public SuggestionResult GetSuggestions(string text, int index)
    {
        var separatorIdx = text.IndexOf(' ');
        if (separatorIdx < 0) return new();
        var commandName = text[..separatorIdx];
        var handler = handlers.Where(x => x.CommandName == commandName).FirstOrDefault();
        if (handler != null) handler.GetSuggestions(text, index);
        
        var generalOpHandler = getGeneralOpHandlerCallback();
        if (generalOpHandler == null) return new();
        return generalOpHandler.GetSuggestions($"{commandName} {text}", index);
    }
}
