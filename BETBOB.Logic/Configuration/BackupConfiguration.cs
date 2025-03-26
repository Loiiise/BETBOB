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

    [JsonPropertyName("overwrite_duplicate_files")]
    public required bool OverwriteDuplicates { get; init; }
    [JsonPropertyName("zip_files")]
    public required bool ZipResult { get; init; }
    [JsonPropertyName("create_timestamp_folder")]
    public required bool CreateTimestampFolder { get; init; }
}
