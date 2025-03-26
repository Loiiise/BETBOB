using BETBOB.Logic.Configuration;
using BETBOB.Logic.FileHandling;

namespace BETBOB.Logic.Command;

public class BackupCommand : ICommand
{
    public BackupCommand(IFileReader fileReader, IBackupConfigurationFactory backupConfigurationFactory, IFileCopyer fileCopyer, IFolderCopyer folderCopyer)
    {
        _fileReader = fileReader;
        _backupConfigurationFactory = backupConfigurationFactory;
        _fileCopyer = fileCopyer;
        _folderCopyer = folderCopyer;
    }

    public void Execute()
    {
        throw new NotImplementedException();
    }

    private readonly IFileReader _fileReader;
    private readonly IBackupConfigurationFactory _backupConfigurationFactory;
    private readonly IFileCopyer _fileCopyer;
    private readonly IFolderCopyer _folderCopyer;
}
