using BETBOB.Logic.Configuration;
using BETBOB.Logic.FileHandling;
using System.Reflection;

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
        var configuration = _backupConfigurationFactory.Empty() with { InputFolders = GetStandardFolders() };
        
        _fileWriter.WriteToFile(
            GetDesinationPath(), 
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
    {
        throw new NotImplementedException();
    }

    private readonly IBackupConfigurationFactory _backupConfigurationFactory;
    private readonly IFileWriter _fileWriter;
}
