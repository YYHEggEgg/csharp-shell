using BenchmarkDotNet.Attributes;
using YYHEggEgg.Logger;
using YYHEggEgg.Shell.Benchmark.Commands;

namespace YYHEggEgg.Shell.Benchmark;

public class CommandHandler_UsageGeneration
{
    static CommandHandler_UsageGeneration()
    {
        Log.Initialize(new()
        {
            Use_Console_Wrapper = true,
        });
    }

    private SimpleCommand _simpleCommand = new();
    private ComplexCommand _complexCommand = new();

    [Benchmark]
    public string[] SimpleCommand()
    {
        return _simpleCommand.UsageLines.ToArray();
    }

    [Benchmark]
    public string[] ComplexCommand()
    {
        return _complexCommand.UsageLines.ToArray();
    }
}
