using CommandLine;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using YYHEggEgg.Logger;

namespace YYHEggEgg.Shell.AutoCompletion;

/// <summary>
/// Complete command option names for <see cref="OptionAttribute"/> or <see cref="ValueAttribute"/>.
/// </summary>
public class OptionsAutoCompleteHandler : IAutoCompleteHandler
{
    private readonly List<string> _autoCmplOptions;
    private readonly Dictionary<string, string> _availableOptions;
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
    private readonly Type _optType;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="optType">The type with public properties that have either <see cref="OptionAttribute"/> or <see cref="ValueAttribute"/>.</param>
    public OptionsAutoCompleteHandler([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] Type optType)
    {
        _autoCmplOptions = [];
        _availableOptions = [];
        _optType = optType;
        InitializeOptions();
    }

    /// <inheritdoc/>
    public SuggestionResult GetSuggestions(string text, int index)
    {
        var args = CommandHandlerBase.ParseAsArgs(text);
        if (index <= args[0].Length)
            throw new NotImplementedException($"Unexpected Internal Error: Should be handled by {nameof(CommandAutoCompleteHandler)}."); // Should not be dispatched here.
        if (text[index - 1] != ' ') return new();

        var startIndex = index;
        var endIndex = text.IndexOf(' ', index);
        if (endIndex == -1) endIndex = text.Length;
        var replaced = text[startIndex..endIndex];
        if (replaced.Contains('"')) return new();

        HashSet<string> excludeOptions = [];
        foreach (var arg in args)
        {
            var defValIndex = arg.LastIndexOf('=');
            if (defValIndex == -1) defValIndex = arg.Length;
            var curArg = arg[..defValIndex];
            foreach (var pair in _availableOptions)
            {
                if (curArg == pair.Key)
                {
                    excludeOptions.Add(pair.Value);
                }
            }
        }

        List<string> suggestions = new(_autoCmplOptions
            .Except(excludeOptions).Where(x => x.StartsWith(replaced)));
        suggestions.Sort();
        return new()
        {
            Suggestions = suggestions,
            StartIndex = startIndex,
            EndIndex = endIndex,
        };
    }

    private void InitializeOptions()
    {
        var properties = _optType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.SetProperty);
        foreach (var property in properties)
        {
            var optAttr = property.GetCustomAttribute<OptionAttribute>();
            if (optAttr == null) continue;
            var optName = $"--{optAttr.LongName}";
            if (optName == "--")
                optName = $"-{optAttr.ShortName}";
            _autoCmplOptions.Add(optName);
            if (!string.IsNullOrEmpty(optAttr.ShortName))
                _availableOptions.Add($"-{optAttr.ShortName}", optName);
            if (!string.IsNullOrEmpty(optAttr.LongName))
                _availableOptions.Add($"--{optAttr.LongName}", optName);
        }
    }
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="TOptions"></typeparam>
public class OptionsAutoCompleteHandler<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TOptions> : OptionsAutoCompleteHandler
{
    /// <summary>
    /// 
    /// </summary>
    public OptionsAutoCompleteHandler() : base(typeof(TOptions))
    {
    }
}
