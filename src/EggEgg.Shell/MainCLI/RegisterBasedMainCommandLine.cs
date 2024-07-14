
namespace YYHEggEgg.Shell.MainCLI;

/// <summary>
/// A implementation of <see cref="MainCommandLineBase"/> that
/// requires all <see cref="CommandHandlerBase"/> to be manually
/// registered by <see cref="Register(CommandHandlerBase)"/>.
/// </summary>
public class RegisterBasedMainCommandLine : MainCommandLine
{
    private readonly List<CommandHandlerBase> _commands = [];

    /// <summary>
    /// Register a handler.
    /// </summary>
    /// <param name="commandHandler"></param>
    public void Register(CommandHandlerBase commandHandler)
    {
        ThrowIfStarted();
        _commands.Add(commandHandler);
    }

    /// <summary>
    /// Get all handlers registered in the input order.
    /// </summary>
    /// <returns></returns>
    protected override IEnumerable<CommandHandlerBase> GetCommandHandlers() => _commands;
}
