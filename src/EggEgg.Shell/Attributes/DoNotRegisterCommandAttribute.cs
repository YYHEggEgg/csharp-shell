using YYHEggEgg.Shell.MainCLI;

namespace YYHEggEgg.Shell.Attributes;

/// <summary>
/// Specifies that this class doesn't want to be included
/// in Auto-registering command procedures (like provided
/// by <see cref="AutoScanMainCommandLine"/>).
/// </summary>
/// <remarks>
/// Types that inherit from <see cref="CommandHandlerBase"/>
/// and is not an abstract class will be included in
/// command auto-registration.
/// </remarks>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class DoNotRegisterCommandAttribute : Attribute
{

}