using YYHEggEgg.Shell.Attributes;
using YYHEggEgg.Shell.MainCLI;

namespace YYHEggEgg.Shell.Model;

/// <summary>
/// Define the available command caller.
/// </summary>
public enum CallerAccess
{
    /// <summary>
    /// Just the same as Undefined.
    /// </summary>
    Undefined = 0,
    /// <summary>
    /// Directly from user input read by <see cref="MainCommandLineBase"/>. This is the default behaviour.
    /// </summary>
    OnlyUserInput = 1,
    /// <summary>
    /// From other commands' invoke (defined in <see cref="CommandNonUserCallAttribute.IncludedAccessors"/>) by <see cref="CommandHandlerBase.InvokeCommandAsync(string, CancellationToken)"/>.
    /// </summary>
    AllowOtherCommands = 9,
    /// <summary>
    /// From any other commands.
    /// </summary>
    AllowEveryone = 20,
}