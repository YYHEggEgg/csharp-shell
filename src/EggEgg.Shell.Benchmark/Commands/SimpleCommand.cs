using CommandLine;

namespace YYHEggEgg.Shell.Benchmark.Commands;

public class SimpleOption
{
    [Option("checksum-integrity", Required = false, Default = false, HelpText = "Check on integrity of checksum values.")]
    public bool CheckChecksumIntegrity { get; set; }
    [Option("dead-ref", Required = false, Default = false, HelpText = "Check on dead ActualMetadata refs.")]
    public bool CheckDeadRef { get; set; }
    [Option("multi-def", Required = false, Default = false, HelpText = "Check on multi definition for the same file.")]
    public bool CheckMultiDef { get; set; }
    [Option("duplicate-object", Required = false, Default = false, HelpText = "Check on duplicate object key.")]
    public bool CheckDuplicateObject { get; set; }
}

public class SimpleCommand : StandardCommandHandler<SimpleOption>
{
    public override string CommandName => "simple";
    public override string Description => "SImple command.";

    public override Task<bool> HandleAsync(SimpleOption o, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}