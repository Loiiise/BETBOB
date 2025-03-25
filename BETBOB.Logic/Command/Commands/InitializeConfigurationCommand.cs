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
        throw new NotImplementedException();
    }

    private readonly IBackupConfigurationFactory _backupConfigurationFactory;
    private readonly IFileWriter _fileWriter;
}
