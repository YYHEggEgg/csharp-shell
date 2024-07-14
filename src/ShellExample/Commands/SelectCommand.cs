using CommandLine;
using Microsoft.Extensions.Logging;

namespace YYHEggEgg.Shell.Example.Commands;

public class SelectOption
{
    [Option('p', "prefix", Required = false, Default = "", MetaValue = "base-path", HelpText = "初始筛选对象使用的目录前缀。")]
    public required string IncludePrefix { get; set; }
}

public class SelectCommand : StandardCommandHandler<SelectOption>
{
    public override string CommandName => "select";

    public override string Description => "Query the files status.";

    public override Task HandleAsync(SelectOption o)
    {
        _logger.LogInformation("You typed prefix: {prefix}", o.IncludePrefix);
        return Task.CompletedTask;
    }
}