namespace YYHEggEgg.Shell.Model;

/// <summary>
/// The status for an argument.
/// </summary>
public enum ArgumentStatus
{
    /// <summary>
    /// It's not allowed to pass value for this argument. If so, an exception should be thrown.
    /// </summary>
    Disallowed,
    /// <summary>
    /// The invoker accepts a null value for this argument, or it's never defined.
    /// </summary>
    Optional,
    /// <summary>
    /// The invoker requires a valid value for this argument.
    /// </summary>
    Required,
}
