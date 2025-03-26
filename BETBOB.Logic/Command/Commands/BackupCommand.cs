using BETBOB.Logic.Configuration;
using BETBOB.Logic.FileHandling;
using BETBOB.Logic.Standards;

namespace BETBOB.Logic.Command;

public class BackupCommand : Command
{
    public BackupCommand(string[] arguments, IFileReader fileReader, IBackupConfigurationFactory backupConfigurationFactory, IFileCopyer fileCopyer, IFolderCopyer folderCopyer, IFileWriter fileWriter) : base(arguments)
    {
        _fileReader = fileReader;
        _backupConfigurationFactory = backupConfigurationFactory;
        _fileCopyer = fileCopyer;
        _folderCopyer = folderCopyer;
        _fileWriter = fileWriter;
    }

    public override void Execute()
    {
        var configuration = GetBackupConfiguration();

        foreach (var inputFolder in configuration.InputFolders)
        {
            var folderDestinationPath = Path.Join(configuration.OutputPath, inputFolder.DropRootFromPath());
            _folderCopyer.CopyFolder(inputFolder, folderDestinationPath, true);
        }

        foreach (var file in configuration.InputFiles)
        {
            var fileDestinationPath = Path.Join(configuration.OutputPath, file.DropRootFromPath());
            _fileCopyer.CopyFile(file, fileDestinationPath, true);
        }

        var configPath = Path.Combine(configuration.OutputPath, ProgramStandards.DefaultConfigurationFileName);
        _fileWriter.WriteToFile(configPath, _backupConfigurationFactory.ToJson(configuration), true);
    }

    private BackupConfiguration GetBackupConfiguration()
    {
        if (Arguments.Length == 1 &&
            File.Exists(Arguments[0]))
        {
            var json = _fileReader.ReadFile(Arguments[0]);
            return _backupConfigurationFactory.FromJson(json);
        }

        var defaultPath = Path.Join(SystemsStandards.GetExecutableLocation(), ProgramStandards.DefaultConfigurationFileName);

        if (File.Exists(defaultPath))
        {
            var json = _fileReader.ReadFile(defaultPath);
            return _backupConfigurationFactory.FromJson(json);
        }

        return _backupConfigurationFactory.Empty();
    }

    private readonly IFileReader _fileReader;
    private readonly IBackupConfigurationFactory _backupConfigurationFactory;
    private readonly IFileCopyer _fileCopyer;
    private readonly IFolderCopyer _folderCopyer;
    private readonly IFileWriter _fileWriter;
}
