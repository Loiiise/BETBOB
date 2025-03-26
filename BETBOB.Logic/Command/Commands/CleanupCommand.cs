using BETBOB.Logic.Standards;
using Microsoft.Extensions.Logging;

namespace BETBOB.Logic.Command;

public class CleanupCommand : Command
{
    public CleanupCommand(ILogger logger) : base(logger) { }

    public override void Execute() => Directory.Delete(ProgramStandards.TemporaryFolderLocation(), true);
}
