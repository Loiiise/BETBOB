using System.Text.Json.Serialization;

namespace BETBOB.Logic.Configuration;

public record BackupConfiguration
{
    [JsonPropertyName("folders")]
    public required string[] InputFolders { get; init; }
    [JsonPropertyName("files")]
    public required string[] InputFiles { get; init; }

    [JsonPropertyName("output_path")]
    public required string OutputPath { get; init; }
}
