using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace YYHEggEgg.Shell.MainCLI;

/// <summary>
/// A <see cref="MainCommandLine"/> implementation that is
/// suitable for <c>Microsoft.Extensions.Hosting</c> users
/// with Dependency Injection support.
/// </summary>
public class HostedMainCommandLine(ILogger<HostedMainCommandLine> logger, IServiceProvider serviceProvider, IHostApplicationLifetime lifetime) : MainCommandLineBase(logger)
{
    /// <inheritdoc/>
    protected override IEnumerable<CommandHandlerBase> GetCommandHandlers()
    {
        return serviceProvider.GetServices<CommandHandlerBase>();
    }

    /// <inheritdoc/>
    public override void Shutdown()
    {
        ShutdownRequested = true;
        _commandLineCancellationTokenSource.Cancel();
        PerformCommandsCleanUp();
        lifetime.StopApplication();
    }

    /// <summary>
    /// Whether shutdown is requested (either from 
    /// <see cref="IHostApplicationLifetime.StopApplication()"/>
    /// or <see cref="Shutdown()"/>).
    /// </summary>
    public bool ShutdownRequested { get; protected set; } = false;
}
