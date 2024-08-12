using YYHEggEgg.Shell.Model;

namespace YYHEggEgg.Shell.Attributes;

/// <summary>
/// Used for an option class, specifying whether its corresponding
/// command (or subcommand) accepts a forwarded cmd argument in
/// <see cref="StandardCommandForwarder{TCmdOption}.HandleAsync(TCmdOption, string?, CancellationToken)"/>.
/// </summary>
/// <param name="allowForwardCmd"></param>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class RequiresForwardCmdAttribute(ArgumentStatus allowForwardCmd) : Attribute
{
    /// <summary>
    /// Define the available command caller.
    /// </summary>
    public ArgumentStatus AllowForwardCmd { get; } = allowForwardCmd;
}