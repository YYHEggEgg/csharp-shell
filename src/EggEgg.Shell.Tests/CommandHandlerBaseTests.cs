
using YYHEggEgg.Shell.Model;

namespace EggEgg.Shell.Tests;

public class CommandHandlerBaseTests
{
    private Dictionary<string, CmdLineRawString[]> ExpectedRawStringsTestDataMap = new()
    {
        [@"""abc"" d e"] = [new(@"""abc""", 0), new(@"d", 6), new(@"e", 8),],
        [@"""abc""  d  e"] = [new(@"""abc""", 0), new(@"d", 7), new(@"e", 10),],
        [@"a\\b d""e f""g h"] = [new(@"a\\b", 0), new(@"d""e f""g", 5), new(@"h", 13),],
        [@"a\\b   d""e f""g  h   "] = [new(@"a\\b", 0), new(@"d""e f""g", 7), new(@"h", 16),],
        [@"a\\\""b c d"] = [new(@"a\\\""b", 0), new(@"c", 7), new(@"d", 9),],
        [@"a\\\""b c   d "] = [new(@"a\\\""b", 0), new(@"c", 7), new(@"d", 11),],
        [@"a\\\\""b c"" d e"] = [new(@"a\\\\""b c""", 0), new(@"d", 11), new(@"e", 13),],
        [@"a\\\\""b c""  d e"] = [new(@"a\\\\""b c""", 0), new(@"d", 12), new(@"e", 14),],
        [@"a""b"""" c d"] = [new(@"a""b"""" c d", 0),],
        [@"a""b""\"" c d"] = [new(@"a""b""\""", 0), new(@"c", 7), new(@"d", 9),],
        [@"  a""b""\""   c d"] = [new(@"a""b""\""", 2), new(@"c", 11), new(@"d", 13),],
        [@"ab\n  cd"] = [new(@"ab\n", 0), new(@"cd", 6),],
        [@"  ab\n   cd  "] = [new(@"ab\n", 2), new(@"cd", 9),],
    };

    [Theory]
    [InlineData(@"""abc"" d e", new string[] { @"abc", @"d", @"e", })]
    [InlineData(@"a\\b d""e f""g h", new string[] { @"a\\b", @"de fg", @"h", })]
    [InlineData(@"a\\\""b c d", new string[] { @"a\""b", @"c", @"d", })]
    [InlineData(@"a\\\\""b c"" d e", new string[] { @"a\\b c", @"d", @"e", })]
    [InlineData(@"a""b"""" c d", new string[] { @"ab"" c d", })]
    [InlineData(@"a""b""\"" c d", new string[] { @"ab""", @"c", @"d", })]
    [InlineData(@"ab\n  cd", new string[] { @"ab\n", @"cd", })]
    [InlineData(@"""abc""  d  e", new string[] { @"abc", @"d", @"e", })]
    [InlineData(@"a\\b   d""e f""g  h   ", new string[] { @"a\\b", @"de fg", @"h", })]
    [InlineData(@"a\\\""b c   d ", new string[] { @"a\""b", @"c", @"d", })]
    [InlineData(@"a\\\\""b c""  d e", new string[] { @"a\\b c", @"d", @"e", })]
    [InlineData(@"  a""b""\""   c d", new string[] { @"ab""", @"c", @"d", })]
    [InlineData(@"  ab\n   cd  ", new string[] { @"ab\n", @"cd", })]
    public void ParseCmdShould(string argList, IEnumerable<string> expectedArgv)
    {
        var parseResult = CommandHandlerBase.ParseCmd(argList);
        var expectedRawStrings = ExpectedRawStringsTestDataMap[argList];
        Assert.Equal(expectedRawStrings, parseResult.RawStringInfos);
        Assert.Equal(expectedArgv, parseResult.ParsedArgv);
    }

    [Theory]
    [InlineData(@"""abc"" d e", new string[] { @"abc", @"d", @"e", })]
    [InlineData(@"a\\b d""e f""g h", new string[] { @"a\\b", @"de fg", @"h", })]
    [InlineData(@"a\\\""b c d", new string[] { @"a\""b", @"c", @"d", })]
    [InlineData(@"a\\\\""b c"" d e", new string[] { @"a\\b c", @"d", @"e", })]
    [InlineData(@"a""b"""" c d", new string[] { @"ab"" c d", })]
    [InlineData(@"a""b""\"" c d", new string[] { @"ab""", @"c", @"d", })]
    [InlineData(@"ab\n cd", new string[] { @"ab\n", @"cd", })]
    public void ParseAsArgsShould(string argList, IEnumerable<string> expectedArgv)
    {
        Assert.Equal(expectedArgv, CommandHandlerBase.ParseAsArgs(argList));
    }
}