using CommandLine;

namespace YYHEggEgg.Shell.Model;

/// <summary>
/// The parsed result for generating usage from one option property.
/// </summary>
public class OptionHelpResult
{
    /// <summary>
    /// The usage in actual command, like <c>-o, --output</c>. Null specify that it's a <see cref="ValueAttribute"/> definition.
    /// </summary>
    public string? OptionString { get; set; }
    /// <summary>
    /// The meta name of required value, like <c>path</c> instead of default <c>string</c> or <c>integer</c>.
    /// </summary>
#if NET8_0_OR_GREATER
    public required string? MetaName { get; set; }
#else
    public string? MetaName { get; set; } = null!;
#endif
    /// <summary>
    /// The help text of this option. Must be a single line
    /// (additional lines should go to <see cref="AdditionalHelps"/>).
    /// </summary>
#if NET8_0_OR_GREATER
    public required string HelpText { get; set; }
#else
    public string HelpText { get; set; } = null!;
#endif
    /// <summary>
    /// Additional details about this options. This will follow <see cref="HelpText"/>.
    /// </summary>
    public IEnumerable<string>? AdditionalHelps { get; set; }
    /// <summary>
    /// Whether this option or value is required.
    /// </summary>
    public bool IsRequired { get; set; }
    /// <summary>
    /// Replace this option's help information with another.
    /// </summary>
    /// <remarks>Notice that if you want to hide this option (without replacing), you should give an empty enumerable (instead of null).</remarks>
    public IEnumerable<OptionHelpResult>? ReplaceWith { get; set; }
}
