using BETBOB.Logic.Standards;
using Microsoft.Extensions.Logging;

namespace BETBOB.Logic.Command;

public class CleanupCommand : Command
{
    public CleanupCommand(ILogger logger) : base(logger) { }

    public override void Execute()
    {
        _logger.LogInformation("Deleting temporary data...");

        var tempFolderLocation = ProgramStandards.TemporaryFolderLocation();
        _logger.LogDebug($"deleting folder: {tempFolderLocation}");
        Directory.Delete(tempFolderLocation, true);

        _logger.LogInformation("Deleted temporary data");
    }
}
