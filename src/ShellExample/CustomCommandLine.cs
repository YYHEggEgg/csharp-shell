using Microsoft.Extensions.Logging;
using YYHEggEgg.Shell.MainCLI;

namespace YYHEggEgg.Shell.Example;

internal class CustomCommandLine : AutoScanMainCommandLine
{
    protected override IEnumerable<string>? AppStartNotices => 
        [
            "This is a common version (suitable for common Program.Main programs).",
        ];
    protected override IEnumerable<string>? StartNewCommandNotices => 
        [
            "---Running---",
        ];
    protected override string? CommandHistoryFilePath => ".not.bash_history";

    public override void Shutdown()
    {
        _logger.LogInformation("Thanks for using YYHEggEgg.Shell!");
        base.Shutdown();
    }
}
