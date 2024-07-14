using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using YYHEggEgg.Shell.MainCLI;

namespace YYHEggEgg.Shell.Example.Hosting;

internal class CustomCommandLine : HostedMainCommandLine
{
    public CustomCommandLine(ILogger<CustomCommandLine> logger, IServiceProvider serviceProvider, IHostApplicationLifetime lifetime) : base(logger, serviceProvider, lifetime)
    {
    }

    protected override IEnumerable<string>? AppStartNotices => 
        [
            "This is a Hosting version (suitable for ASP.NET servers or Microsoft.Extensions.Hosting users).",
        ];

    public override void Shutdown()
    {
        ShutdownRequested = true;
        _logger.LogInformation("Thanks for using YYHEggEgg.Shell!");
        base.Shutdown();
    }
}
