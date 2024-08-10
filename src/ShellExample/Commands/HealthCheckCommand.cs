using CommandLine;
using Microsoft.Extensions.Logging;
using YYHEggEgg.Logger;

namespace YYHEggEgg.Shell.Example.Commands;

[Verb("run", true, HelpText = "Run the health check background.")]
public class HealthCheckRunOptions
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

[Verb("cancel", false, ["abandon", "stop"], HelpText = "Cancel the current health check.")]
public class HealthCheckCancelOptions
{

}

public class HealthCheckCommand : HasSubCommandsHandlerBase<HealthCheckRunOptions, HealthCheckCancelOptions>
{
    public override string CommandName => "healthcheck";

    public override string Description => "Run service Health Check.";

    public override Task<bool> HandleAsync(HealthCheckRunOptions o, CancellationToken cancellationToken)
    {
        _logger.LogInformation("You requested health check, CheckChecksumIntegrity: {bool}, CheckDeadRef: {bool}, CheckMultiDef: {bool}, CheckDuplicateObject: {bool}", o.CheckChecksumIntegrity, o.CheckDeadRef, o.CheckMultiDef, o.CheckDuplicateObject);
        return Task.FromResult(true);
    }

    public override async Task<bool> HandleAsync(HealthCheckCancelOptions o, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Are you sure to continue?");
        ConsoleWrapper.InputPrefix = "Type 'y' or 'n': ";
        var result = await ConsoleWrapper.ReadLineAsync();
        if (result.ToLower() == "y")
            _logger.LogInformation("Cancellation requested.");
        else
        {
            _logger.LogInformation("User refused to continue.");
            return false;
        }
        return true;
    }
}
