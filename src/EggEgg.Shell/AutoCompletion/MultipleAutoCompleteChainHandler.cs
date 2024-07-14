using YYHEggEgg.Logger;

namespace YYHEggEgg.Shell.AutoCompletion;

/// <summary>
/// Chain multiple <see cref="IAutoCompleteHandler"/> until one of them give suggestions.
/// </summary>
public class MultipleAutoCompleteChainHandler : IAutoCompleteHandler
{
    private readonly List<IAutoCompleteHandler> _handlers = [];
    /// <summary>
    /// Push a component to the chain handler. It's ordered.
    /// </summary>
    /// <param name="autoCompleteHandler"></param>
    public void PushComponent(IAutoCompleteHandler autoCompleteHandler) => _handlers.Add(autoCompleteHandler);
    
    /// <inheritdoc/>
    public SuggestionResult GetSuggestions(string text, int index)
    {
        foreach (var handler in _handlers)
        {
            var result = handler.GetSuggestions(text, index);
            if (result.Suggestions?.Any() == true) return result;
        }
        return new();
    }
}