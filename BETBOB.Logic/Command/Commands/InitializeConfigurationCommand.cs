using BETBOB.Logic.Configuration;
using BETBOB.Logic.FileHandling;
using BETBOB.Logic.Standards;

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
        var configuration = _backupConfigurationFactory.Empty() with 
        { 
            InputFolders = SystemsStandards.GetCommonFolders().Where(Directory.Exists).ToArray()
        };

        _fileWriter.WriteToFile(
            GetDesinationPath(),
            _backupConfigurationFactory.ToJson(configuration),
            true);
    }

    private string GetDesinationPath()
    {
        var executableDirectory = SystemsStandards.GetExecutableLocation();

        if (executableDirectory == null)
        {
            return Path.Join(SystemsStandards.DefaultDriveRoot, ProgramStandards.DefaultConfigurationFileName);
        }

        return Path.Join(executableDirectory, ProgramStandards.DefaultConfigurationFileName);
    }

    private readonly IBackupConfigurationFactory _backupConfigurationFactory;
    private readonly IFileWriter _fileWriter;
}
