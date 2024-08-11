namespace YYHEggEgg.Shell.Model;

/// <summary>
/// Define the option base for a forwarding command.
/// </summary>
public abstract class ForwardCommandOptionBase
{
    /// <summary>
    /// Whether this command (or subcommand) allows a forwarded cmd in <see cref="CommandForwarderBase.HandleAsync(string, string?, CancellationToken)"/>.
    /// </summary>
    public abstract ArgumentStatus AllowForwardCmd { get; }
}
