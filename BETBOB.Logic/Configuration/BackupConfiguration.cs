namespace BETBOB.Logic.Configuration;

public record BackupConfiguration
{
    public required string[] InputFolders { get; init; }
    public required string[] InputFiles { get; init; }

    public required string OutputPath { get; init; }
}
