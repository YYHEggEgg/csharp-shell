using Microsoft.Extensions.Logging;

namespace YYHEggEgg.Shell.Example.Commands;

public class RepeatCommand : CommandHandlerBase
{
    public override string CommandName => "repeat";

    public override string Description => "Repeat your parameters.";

    public override IEnumerable<string> UsageLines =>
        [
            "repeat <anything>"
        ];

    public override Task HandleAsync(string argList)
    {
        _logger.LogInformation("{}", argList);
        _logger.LogInformation("args list: [ {} ]", ParseAsArgs(argList));
        return Task.CompletedTask;
    }
}