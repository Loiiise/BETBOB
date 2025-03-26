using BETBOB.Logic.Configuration;
using BETBOB.Logic.FileHandling;

namespace BETBOB.Logic.Command;

public class BackupCommand : Command
{
    public BackupCommand(string[] arguments, IFileReader fileReader, IBackupConfigurationFactory backupConfigurationFactory, IFileCopyer fileCopyer, IFolderCopyer folderCopyer) : base(arguments)
    {
        _fileReader = fileReader;
        _backupConfigurationFactory = backupConfigurationFactory;
        _fileCopyer = fileCopyer;
        _folderCopyer = folderCopyer;
    }

    public override void Execute()
    {
        throw new NotImplementedException();
    }

    private readonly IFileReader _fileReader;
    private readonly IBackupConfigurationFactory _backupConfigurationFactory;
    private readonly IFileCopyer _fileCopyer;
    private readonly IFolderCopyer _folderCopyer;
}
