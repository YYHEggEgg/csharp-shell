using CommandLine;
using Microsoft.Extensions.Logging;
using YYHEggEgg.Shell.Attributes;
using YYHEggEgg.Shell.Model;

namespace YYHEggEgg.Shell.Example.Commands;

[RequiresForwardCmd(ArgumentStatus.Required)]
[Verb("run", true, HelpText = "Forward a command.")]
internal class ForwardOption
{
    [Option("option1", HelpText = "example option.")]
    public string? Option1 { get; set; }
}

[RequiresForwardCmd(ArgumentStatus.Disallowed)]
[Verb("status", false, HelpText = "Get the forward status.")]
internal class ForwardStatusOption
{

}

internal class ForwardCommand : HasSubCommandsForwarderBase<ForwardOption, ForwardStatusOption>
{
    public override string CommandName => "forward";

    public override string Description => "Forward a command.";

    public override async Task<bool> HandleAsync(ForwardOption o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Forwarding option 1: {opt}, command: '{cmd}'", o.Option1, forwardedCmd);
        // Required by 'AllowForwardCmd => ArgumentStatus.Required;'
        var result = await InvokeCommandAsync(forwardedCmd!, cancellationToken);
        _logger.LogInformation("Command execution success: {bool}.", result);
        return result;
    }

    public override Task<bool> HandleAsync(ForwardStatusOption o, string? forwardedCmd, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("I'm fine!");
        return Task.FromResult(true);
    }
}
