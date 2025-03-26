using BETBOB.Logic.Standards;
using Microsoft.Extensions.Logging;

namespace BETBOB.Logic.Command;

public class CleanupCommand : Command
{
    public CleanupCommand(ILogger logger) : base(logger) { }

    public override void Execute()
    {
        var tempFolderLocation = ProgramStandards.TemporaryFolderLocation();

        if (Directory.Exists(tempFolderLocation))
        {
            _logger.LogInformation("Deleting temporary data...");
            _logger.LogDebug($"deleting folder: {tempFolderLocation}");
            Directory.Delete(tempFolderLocation, true);
            _logger.LogInformation("Deleted temporary data");
        }
        else
        {
            _logger.LogInformation("No temporary data to delete!");
        }
    }
}
