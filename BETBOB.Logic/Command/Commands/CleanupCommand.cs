using BETBOB.Logic.Standards;

namespace BETBOB.Logic.Command;

public class CleanupCommand : ICommand
{
    public void Execute() => Directory.Delete(ProgramStandards.TemporaryFolderLocation(), true);
}
