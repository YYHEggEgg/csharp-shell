using YYHEggEgg.Shell.Model;

namespace YYHEggEgg.Shell.Exceptions;

/// <summary>
/// 
/// </summary>
public class RejectedCommandCallException : Exception
{
    /// <summary>
    /// Reject a non-user call for the specified reason.
    /// </summary>
    /// <param name="callerName"></param>
    /// <param name="commandName"></param>
    /// <param name="refuserPolicy"></param>
    public RejectedCommandCallException(string callerName, string commandName, CallerAccess refuserPolicy)
        : base(GenerateExMessage(callerName, commandName, refuserPolicy))
    {

    }

    /// <inheritdoc cref="RejectedCommandCallException(string, string, CallerAccess)"/>
    public RejectedCommandCallException(string callerName, string commandName)
        : base(GenerateExMessage(callerName, commandName, CallerAccess.Undefined))
    {

    }

    private static string GenerateExMessage(string callerName, string commandName, CallerAccess refuserPolicy)
    {
        var result = $"Command '{commandName}' rejected non-user call from '{callerName}'. ";
        result += refuserPolicy switch
        {
            CallerAccess.OnlyUserInput => "This command only allow access from direct user input.",
            CallerAccess.AllowOtherCommands => "This command does not include caller as a accepted accessor.",
            CallerAccess.Undefined => "This command performs as is the default behaviour (refusing all non-user calls).",
            CallerAccess.AllowEveryone => "This may be a bug of EggEgg.Shell because this command allows invocation from everyone.",
            _ => "This CallerAccess is not recognized by Exception message generator."
        };
        return result;
    }
}
