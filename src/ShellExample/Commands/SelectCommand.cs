using CommandLine;
using Microsoft.Extensions.Logging;

namespace YYHEggEgg.Shell.Example.Commands;

public class SelectOption
{
    [Option('p', "prefix", Required = false, Default = "", MetaValue = "base-path", HelpText = "The initial prefix.")]
    public required string IncludePrefix { get; set; }
}

public class SelectCommand : StandardCommandHandler<SelectOption>
{
    public override string CommandName => "select";

    public override string Description => "Query the files status.";

    public override Task<bool> HandleAsync(SelectOption o, CancellationToken cancellationToken)
    {
        _logger.LogInformation("You typed prefix: {prefix}", o.IncludePrefix);
        return Task.FromResult(true);
    }
}