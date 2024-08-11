namespace YYHEggEgg.Shell.Model;

/// <summary>
/// Specify the parse result of <see cref="CommandHandlerBase.ParseCmd(string)"/>.
/// </summary>
public class CmdLineParseResult
{
    /// <summary>
    /// The usable command-line argument strings.
    /// </summary>
    public List<string> ParsedArgv { get; set; } = [];
    /// <summary>
    /// The raw string corresponding to <see cref="ParsedArgv"/>, with all escape characters or quotes.
    /// </summary>
    /// <remarks>
    /// It's guarantee that joining this sequence with space gets the original cmd passed to the parse method (if ignoring extra spaces).
    /// </remarks>
    public List<CmdLineRawString> RawStringInfos { get; set; } = [];
}
