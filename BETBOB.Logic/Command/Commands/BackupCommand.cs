using BETBOB.Logic.Configuration;
using BETBOB.Logic.FileHandling;
using BETBOB.Logic.Standards;
using System.IO.Compression;

namespace BETBOB.Logic.Command;

public class BackupCommand : ICommand
{
    public BackupCommand(IFileReader fileReader, IBackupConfigurationFactory backupConfigurationFactory, IFileCopyer fileCopyer, IFolderCopyer folderCopyer, IFileWriter fileWriter, string? backupConfigurationPath = null)
    {
        _fileReader = fileReader;
        _backupConfigurationFactory = backupConfigurationFactory;
        _fileCopyer = fileCopyer;
        _folderCopyer = folderCopyer;
        _fileWriter = fileWriter;
        _backupConfigurationPath = backupConfigurationPath;
    }

    public void Execute()
    {
        var configuration = GetBackupConfiguration();
        var versionString = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-") + ProgramStandards.ProgramName;

        var outputPath = configuration.ZipResult ?
            ProgramStandards.ZipArchiveTemporaryFolderName(versionString) :
            configuration.CreateTimestampFolder ?
                Path.Join(configuration.OutputPath, versionString) :
                configuration.OutputPath;

        foreach (var inputFolder in configuration.InputFolders)
        {
            var folderDestinationPath = Path.Join(outputPath, inputFolder.DropRootFromPath());
            _folderCopyer.CopyFolder(inputFolder, folderDestinationPath, configuration.OverwriteDuplicates);
        }

        foreach (var file in configuration.InputFiles)
        {
            var fileDestinationPath = Path.Join(outputPath, file.DropRootFromPath());
            _fileCopyer.CopyFile(file, fileDestinationPath, configuration.OverwriteDuplicates);
        }

        var configOutputPath = Path.Combine(outputPath, ProgramStandards.DefaultConfigurationFileName);
        _fileWriter.WriteToFile(configOutputPath, _backupConfigurationFactory.ToJson(configuration), true);

        if (configuration.ZipResult)
        {
            SystemsStandards.CreateFolderIfNotExists(configuration.OutputPath);

            var zipPath = Path.Combine(configuration.OutputPath, versionString + ".zip");
            ZipFile.CreateFromDirectory(outputPath, zipPath);

            Directory.Delete(outputPath, true);
        }
    }

    private BackupConfiguration GetBackupConfiguration()
    {
        if (_backupConfigurationPath != null &&
            File.Exists(_backupConfigurationPath))
        {
            var json = _fileReader.ReadFile(_backupConfigurationPath);
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

    private string? _backupConfigurationPath { get; init; }
}
