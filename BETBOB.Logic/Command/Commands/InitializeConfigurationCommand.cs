﻿using BETBOB.Logic.Configuration;
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
        var configurationDestinationPath = GetDesinationPath();

        var configuration = _backupConfigurationFactory.Empty() with
        {
            InputFolders = SystemsStandards.GetCommonFolders(),
            InputFiles = new string[] { configurationDestinationPath },
        };

        _fileWriter.WriteToFile(
            configurationDestinationPath,
            _backupConfigurationFactory.ToJson(configuration),
            true);
    }

    private string GetDesinationPath()
    {
        var executableDirectory = SystemsStandards.GetExecutableLocation();

        if (executableDirectory == null)
        {
            return Path.Join(SystemsStandards.DefaultDriveRoot, BETBOBStandards.DefaultConfigurationFileName);
        }

        return Path.Join(executableDirectory, BETBOBStandards.DefaultConfigurationFileName);
    }

    private readonly IBackupConfigurationFactory _backupConfigurationFactory;
    private readonly IFileWriter _fileWriter;
}
