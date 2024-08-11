using CommandLine;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YYHEggEgg.Shell.Model;

namespace YYHEggEgg.Shell.Example.Commands;

internal class ForwardOption : ForwardCommandOptionBase
{
    public override ArgumentStatus AllowForwardCmd => ArgumentStatus.Required;

    [Option("option1", HelpText = "example option.")]
    public string? Option1 { get; set; }
}

internal class ForwardCommand : StandardCommandForwarder<ForwardOption>
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
}
