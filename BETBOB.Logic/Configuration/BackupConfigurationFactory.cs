﻿using System.Text.Json;

namespace BETBOB.Logic.Configuration;

public class BackupConfigurationFactory : IBackupConfigurationFactory
{
    public readonly string DefaultDestinationPath = Path.Combine("C", "BETBOB");

    public BackupConfiguration Default() => new BackupConfiguration
    {
        InputFolders = Array.Empty<string>(),
        InputFiles = Array.Empty<string>(),

        OutputPath = DefaultDestinationPath,
    };

    public string DefaultString() => ToJson(Default());

    public BackupConfiguration FromJson(string json)
        => JsonSerializer.Deserialize<BackupConfiguration>(json)!;
    
    public string ToJson(BackupConfiguration configuration)
        => JsonSerializer.Serialize(configuration);    
}
