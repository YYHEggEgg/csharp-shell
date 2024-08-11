using YYHEggEgg.Shell.Model;

namespace YYHEggEgg.Shell.Attributes;

/// <summary>
/// Specifies how this class want to be treated when there's
/// a caller from not user input.
/// </summary>
/// <param name="callerPolicy"></param>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class CommandNonUserCallAttribute(CallerAccess callerPolicy) : Attribute
{
    /// <summary>
    /// Define the available command caller.
    /// </summary>
    public CallerAccess CallerPolicy { get; } = callerPolicy;
    /// <summary>
    /// The included accessor types. If <see cref="CallerPolicy"/>
    /// is <see cref="CallerAccess.AllowOtherCommands"/>
    /// </summary>
    public Type[]? IncludedAccessors { get; set; }
}