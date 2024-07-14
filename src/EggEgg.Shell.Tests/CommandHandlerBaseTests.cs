
namespace EggEgg.Shell.Tests;

public class CommandHandlerBaseTests
{
    [Theory]
    [InlineData(@"""abc"" d e", new string[] { @"abc", @"d", @"e" })]
    [InlineData(@"a\\b d""e f""g h", new string[] { @"a\\b", @"de fg", @"h" })]
    [InlineData(@"a\\\""b c d", new string[] { @"a\""b", @"c", @"d" })]
    [InlineData(@"a\\\\""b c"" d e", new string[] { @"a\\b c", @"d", @"e" })]
    [InlineData(@"a""b"""" c d", new string[] { @"ab"" c d" })]
    [InlineData(@"a""b""\"" c d", new string[] { @"ab""", @"c", @"d" })]
    [InlineData(@"ab\n cd", new string[] { @"ab\n", @"cd" })]
    public void ParseAsArgsShould(string argList, IEnumerable<string> expectedArgv)
    {
        Assert.Equal(expectedArgv, CommandHandlerBase.ParseAsArgs(argList));
    }
}