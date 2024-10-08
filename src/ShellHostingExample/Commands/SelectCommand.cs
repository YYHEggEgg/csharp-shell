﻿using CommandLine;
using Microsoft.Extensions.Logging;

namespace YYHEggEgg.Shell.Example.Hosting.Commands;

public class SelectOption
{
    [Option('p', "prefix", Required = false, Default = "", MetaValue = "base-path", HelpText = "初始筛选对象使用的目录前缀。")]
    public required string IncludePrefix { get; set; }
}

public class SelectCommand(ILogger<SelectCommand> logger) : StandardCommandHandler<SelectOption>(logger)
{
    public override string CommandName => "select";

    public override string Description => "Query the files status.";

    public override Task<bool> HandleAsync(SelectOption o, CancellationToken cancellationToken)
    {
        _logger.LogInformation("You typed prefix: {prefix}", o.IncludePrefix);
        return Task.FromResult(true);
    }
}