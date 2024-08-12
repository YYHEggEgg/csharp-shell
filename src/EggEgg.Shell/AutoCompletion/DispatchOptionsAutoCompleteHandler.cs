using YYHEggEgg.Logger;

namespace YYHEggEgg.Shell.AutoCompletion;

/// <summary>
/// A special type used to dispatch auto completion operation.
/// </summary>
/// <param name="handlers"></param>
/// <param name="getGeneralOpHandlerCallback"></param>
public class DispatchOptionsAutoCompleteHandler(IEnumerable<CommandHandlerBase> handlers, Func<CommandHandlerBase?> getGeneralOpHandlerCallback) : IAutoCompleteHandler
{
    private readonly List<CommandHandlerBase> _handlers = handlers.ToList();

    /// <inheritdoc/>
    public SuggestionResult GetSuggestions(string text, int index)
    {
        var separatorIdx = text.IndexOf(' ');
        if (separatorIdx < 0) return new();
        var commandName = text[..separatorIdx];
        var handler = _handlers.Where(x => x.CommandName == commandName).FirstOrDefault();
        if (handler != null) return handler.GetSuggestions(text, index);
        
        var generalOpHandler = getGeneralOpHandlerCallback();
        if (generalOpHandler == null) return new();
        var generalOpAlias = generalOpHandler.CommandName;
        var res = generalOpHandler.GetSuggestions($"{generalOpAlias} {text}", index);
        res.StartIndex -= generalOpAlias.Length;
        res.EndIndex -= generalOpAlias.Length;
        if (res.StartIndex < 0) return new();
        else return res;
    }
}
