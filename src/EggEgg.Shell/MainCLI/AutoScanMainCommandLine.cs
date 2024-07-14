using System.Diagnostics.CodeAnalysis;

namespace YYHEggEgg.Shell.MainCLI;

/// <summary>
/// A implementation of <see cref="MainCommandLineBase"/> that
/// registers all <see cref="CommandHandlerBase"/> in the 
/// calling assembly and entry assembly.
/// </summary>
[RequiresUnreferencedCode("Require reflection on Assembly.GetCallingAssembly() and Assembly.GetEntryAssembly().")]
public class AutoScanMainCommandLine : MainCommandLine
{
    /// <inheritdoc cref="Tools.ScanCommandHandlers()"/>
    protected override IEnumerable<CommandHandlerBase> GetCommandHandlers()
    {
        return Tools.ScanCommandHandlers();
    }
}
