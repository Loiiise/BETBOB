using BETBOB.Logic.Standards;
using System.Text.Json;

namespace BETBOB.Logic.Configuration;

public class BackupConfigurationFactory : IBackupConfigurationFactory
{
    public BackupConfiguration Empty() => new BackupConfiguration
    {
        InputFolders = Array.Empty<string>(),
        InputFiles = Array.Empty<string>(),

        OutputPath = Path.Combine(SystemsStandards.DefaultDriveRoot, ProgramStandards.ProgramName),

        OverwriteDuplicates = true,
        ZipResult = false,
        CreateTimestampFolder = true,
    };

    public BackupConfiguration FromJson(string json)
        => JsonSerializer.Deserialize<BackupConfiguration>(json)!;
    
    public string ToJson(BackupConfiguration configuration)
        => JsonSerializer.Serialize(configuration);    
}
