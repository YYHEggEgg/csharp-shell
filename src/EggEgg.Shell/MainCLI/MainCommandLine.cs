namespace YYHEggEgg.Shell.MainCLI;

/// <summary>
/// An <see cref="MainCommandLineBase"/> implementation for common projects
/// (without <c>Microsoft.Extensions.Hosting</c>).
/// </summary>
public abstract class MainCommandLine : MainCommandLineBase
{
    /// <inheritdoc/>
    public MainCommandLine() : base(new EggEggLogger(nameof(MainCommandLine)))
    {
    }

    /// <inheritdoc/>
    public override void Shutdown()
    {
        _commandLineCancellationTokenSource.Cancel();
        PerformCommandsCleanUp();
        Environment.Exit(0);
    }
}
