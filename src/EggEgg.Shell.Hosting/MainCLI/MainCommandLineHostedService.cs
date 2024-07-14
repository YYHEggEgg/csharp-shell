using Microsoft.Extensions.Hosting;

namespace YYHEggEgg.Shell.MainCLI;

/// <summary>
/// An <see cref="IHostedService"/> implementation that can be
/// registered in a DI container. Acts as a wrapper of
/// <see cref="HostedMainCommandLine"/>.
/// </summary>
/// <param name="mainCommandLine">The Command Line to be wrapped.</param>
public class MainCommandLineHostedService(HostedMainCommandLine mainCommandLine) : IHostedService
{
    /// <inheritdoc/>
    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        mainCommandLine.StartBackground();
        await Task.CompletedTask;
    }

    /// <inheritdoc/>
    public async Task StopAsync(CancellationToken cancellationToken = default)
    {
        if (!mainCommandLine.ShutdownRequested) mainCommandLine.Shutdown();
        await Task.CompletedTask;
    }
}
