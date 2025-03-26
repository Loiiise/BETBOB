using BETBOB.Logic.Configuration;
using BETBOB.Logic.FileHandling;
using System.Reflection;
using static System.Environment;

namespace BETBOB.Logic.Command;

public class InitializeConfigurationCommand : ICommand
{
    public InitializeConfigurationCommand(
        IBackupConfigurationFactory backupConfigurationFactory,
        IFileWriter fileWriter)
    {
        _backupConfigurationFactory = backupConfigurationFactory;
        _fileWriter = fileWriter;
    }

    public void Execute()
    {
        var configurationDestinationPath = GetDesinationPath();

        var configuration = _backupConfigurationFactory.Empty() with
        {
            InputFolders = GetStandardFolders(),
            InputFiles = new string[] { configurationDestinationPath },
        };

        _fileWriter.WriteToFile(
            configurationDestinationPath,
            _backupConfigurationFactory.ToJson(configuration),
            true);
    }

    private string GetDesinationPath()
    {
        var executableDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location);
        var defaultFileName = "BETBOB.config";

        return Path.Join(executableDirectory, defaultFileName);
    }

    private string[] GetStandardFolders()
        => new SpecialFolder[]
        {
            SpecialFolder.Desktop,
            SpecialFolder.MyDocuments,
            SpecialFolder.Personal,
            SpecialFolder.Favorites,
            SpecialFolder.MyMusic,
            SpecialFolder.MyVideos,
            SpecialFolder.DesktopDirectory,
            SpecialFolder.MyPictures,
            SpecialFolder.CommonDocuments,
            SpecialFolder.CommonMusic,
            SpecialFolder.CommonPictures,
            SpecialFolder.CommonVideos,
        }
        .Select(GetFolderPath)
        // SpecialFolder may point to the same folder on certain systems
        .Distinct()
        .ToArray();

    private readonly IBackupConfigurationFactory _backupConfigurationFactory;
    private readonly IFileWriter _fileWriter;
}
