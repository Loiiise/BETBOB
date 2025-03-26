using BETBOB.Logic.Configuration;
using BETBOB.Logic.FileHandling;
using BETBOB.Logic.Standards;
using Microsoft.Extensions.Logging;
using System.IO.Compression;

namespace BETBOB.Logic.Command;

public class BackupCommand : Command
{
    public BackupCommand(ILogger logger, IFileReader fileReader, IBackupConfigurationFactory backupConfigurationFactory, IFileCopyer fileCopyer, IFolderCopyer folderCopyer, IFileWriter fileWriter, string? backupConfigurationPath = null) : base(logger)
    {
        _fileReader = fileReader;
        _backupConfigurationFactory = backupConfigurationFactory;
        _fileCopyer = fileCopyer;
        _folderCopyer = folderCopyer;
        _fileWriter = fileWriter;
        _backupConfigurationPath = backupConfigurationPath;
    }

    public override void Execute()
    {
        var configuration = GetBackupConfiguration();
        var versionString = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-") + ProgramStandards.ProgramName;

        var outputPath = configuration.ZipResult ?
            ProgramStandards.ZipArchiveTemporaryFolderName(versionString) :
            configuration.CreateTimestampFolder ?
                Path.Join(configuration.OutputPath, versionString) :
                configuration.OutputPath;

        _logger.LogInformation(configuration.ZipResult ? "Setting up temporary files..." : $"Copying files to {outputPath}");

        foreach (var inputFolder in configuration.InputFolders)
        {
            var folderDestinationPath = Path.Join(outputPath, inputFolder.DropRootFromPath());
            _logger.LogDebug($"Copying {inputFolder} to {folderDestinationPath}");
            _folderCopyer.CopyFolder(inputFolder, folderDestinationPath, configuration.OverwriteDuplicates);
        }

        foreach (var inputFile in configuration.InputFiles)
        {
            var fileDestinationPath = Path.Join(outputPath, inputFile.DropRootFromPath());
            _logger.LogDebug($"Copying {inputFile} to {fileDestinationPath}");
            _fileCopyer.CopyFile(inputFile, fileDestinationPath, configuration.OverwriteDuplicates);
        }

        var configOutputPath = Path.Combine(outputPath, ProgramStandards.DefaultConfigurationFileName);
        _fileWriter.WriteToFile(configOutputPath, _backupConfigurationFactory.ToJson(configuration), true);

        if (configuration.ZipResult)
        {
            _logger.LogInformation("Zipping your files...");
            SystemsStandards.CreateFolderIfNotExists(configuration.OutputPath);

            var zipPath = Path.Combine(configuration.OutputPath, versionString + ".zip");
            ZipFile.CreateFromDirectory(outputPath, zipPath);

            _logger.LogInformation("Deleting temporary files...");
            Directory.Delete(outputPath, true);
        }

        _logger.LogInformation($"Backup completed! See {configuration.OutputPath}");
    }

    private BackupConfiguration GetBackupConfiguration()
    {
        if (_backupConfigurationPath != null &&
            File.Exists(_backupConfigurationPath))
        {
            _logger.LogInformation($"Using configuration as specified at: {_backupConfigurationPath}");
            var json = _fileReader.ReadFile(_backupConfigurationPath);
            return _backupConfigurationFactory.FromJson(json);
        }

        var defaultPath = Path.Join(SystemsStandards.GetExecutableLocation(), ProgramStandards.DefaultConfigurationFileName);

        if (File.Exists(defaultPath))
        {
            _logger.LogInformation($"Using configuration at current location: {_backupConfigurationPath}");
            var json = _fileReader.ReadFile(defaultPath);
            return _backupConfigurationFactory.FromJson(json);
        }

        _logger.LogInformation($"Using default configuration.");
        return _backupConfigurationFactory.Empty();
    }

    private readonly IFileReader _fileReader;
    private readonly IBackupConfigurationFactory _backupConfigurationFactory;
    private readonly IFileCopyer _fileCopyer;
    private readonly IFolderCopyer _folderCopyer;
    private readonly IFileWriter _fileWriter;

    private string? _backupConfigurationPath { get; init; }
}
