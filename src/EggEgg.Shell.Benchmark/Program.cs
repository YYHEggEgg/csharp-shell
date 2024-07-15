using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using YYHEggEgg.Shell.Benchmark;

namespace EggEgg.Shell.Benchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = DefaultConfig.Instance;
            var summary = BenchmarkRunner.Run<CommandHandler_UsageGeneration>(config, args);

            // Use this to select benchmarks from the console:
            // var summaries = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);
        }
    }
}