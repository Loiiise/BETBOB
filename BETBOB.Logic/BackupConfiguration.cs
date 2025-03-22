namespace BETBOB.Logic;

public class BackupConfiguration
{
    public required string[] InputFolders { get; init; }
    public required string[] InputFiles { get; init; }

    public required string OutputPath { get; init; }
}
