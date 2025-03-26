using BETBOB.Logic.Configuration;
using Shouldly;
using System.Text.Json;

namespace BETBOB.Logic.Tests.Configuration;

public class BackupConfigurationFactoryTests
{
    [Fact]
    public void EmptyShouldGiveEmptyListsAndCDriveDestination()
    {
        var factory = new BackupConfigurationFactory();

        var configuration = factory.Empty();

        configuration.ShouldNotBeNull();
        configuration.InputFolders.ShouldBeEmpty();
        configuration.InputFiles.ShouldBeEmpty();
        configuration.OutputPath.ShouldContain($"C:");
        configuration.OutputPath.ShouldContain("BETBOB");
    }

    [Theory]
    [InlineData("""
        {
            "create_timestamp_folder": true,
            "files": ["fileString"],
            "zip_files": false,
            "output_path": "",
            "overwrite_duplicate_files": true,    
            "folders": ["folderString"]
        }
        """)]
    [InlineData("""
        {
            "folders": ["folderString"],
            "create_timestamp_folder": true,
            "output_path": "",
            "overwrite_duplicate_files": true,    
            "zip_files": false,
            "files": ["fileString"]
        }
        """)]
    [InlineData("""
        {
            "output_path": "",
            "folders": ["folderString"],
            "zip_files": false,
            "files": ["fileString"],
            "overwrite_duplicate_files": true,    
            "create_timestamp_folder": true
        }
        """)]
    public void FieldCanBeParsedInAnyOrder(string json)
    {
        var factory = new BackupConfigurationFactory();
        Should.NotThrow(() => factory.FromJson(json));
    }

    [Theory]
    [InlineData("""
        {
            "files": ["fileString"],
            "output_path": ""
        }
        """)]
    [InlineData("""
        {
            "folders": ["folderString"],
            "output_path": ""
        }
        """)]
    [InlineData("""
        {
            "folders": ["folderString"],
            "files": ["fileString"]
        }
        """)]
    public void ShouldThrowOnMissingFields(string json)
    {
        var factory = new BackupConfigurationFactory();
        Should.Throw<JsonException>(() => factory.FromJson(json));
    }

    [Theory]
    [InlineData("""
        {
            "folders": ["folderString"],
            "files": ["fileString"],
            "output_path": "",
            "another_field": [],
            "overwrite_duplicate_files": true,    
            "zip_files": false,
            "create_timestamp_folder": true
        }
        """)]
    [InlineData("""
        {
            "folders": ["folderString"],
            "another_field_sooner": {
                "sub_field": [],
                "someone_else": "balbal"
            },
            "files": ["fileString"],
            "output_path": "",
            "overwrite_duplicate_files": true,    
            "zip_files": false,
            "create_timestamp_folder": true
        }
        """)]
    public void OtherFieldsAreIgnored(string json)
    {
        var factory = new BackupConfigurationFactory();
        Should.NotThrow(() => factory.FromJson(json));
    }

    [Theory]
    [InlineData("""
        {
            "folders": [],
            "files": [],
            "output_path": "",
            "overwrite_duplicate_files": true,    
            "zip_files": false,
            "create_timestamp_folder": true        
        }
        """)]
    public void EmptyJsonCanBeParsed(string json)
    {
        var factory = new BackupConfigurationFactory();

        var configuration = factory.FromJson(json);
        configuration.ShouldNotBeNull();
        configuration.InputFolders.ShouldBeEmpty();
        configuration.InputFiles.ShouldBeEmpty();
        configuration.OutputPath.ShouldBe("");
    }

    [Theory, CombinatorialData]
    public void JsonFieldsCanBeParsed(
        [CombinatorialMemberData(nameof(GenerateBasicStringArrays))] string[] inputFolders,
        [CombinatorialMemberData(nameof(GenerateBasicStringArrays))] string[] inputFiles,
        [CombinatorialMemberData(nameof(GenerateBasicStrings))] string outputPath,
        [CombinatorialValues(true, false)] bool overwriteDuplicates,
        [CombinatorialValues(true, false)] bool zipResult,
        [CombinatorialValues(true, false)] bool createTimestampFolder)
    {
        var factory = new BackupConfigurationFactory();


        var inputFoldersString = string.Join(',', inputFolders.Select(f => "\"" + f + "\""));
        var inputFilesString = string.Join(',', inputFiles.Select(f => "\"" + f + "\""));

        var stringifiedConfiguration = $$"""
                        {
                            "folders": [{{inputFoldersString}}],
                            "files": [{{inputFilesString}}],
                            "output_path": "{{outputPath}}",
                            "overwrite_duplicate_files": {{overwriteDuplicates.ToString().ToLower()}},    
                            "zip_files": {{zipResult.ToString().ToLower()}},
                            "create_timestamp_folder": {{createTimestampFolder.ToString().ToLower()}}                        
                        }
                        """;

        var result = factory.FromJson(stringifiedConfiguration);

        result.ShouldNotBeNull();
        result.InputFolders.ShouldBe(inputFolders);
        result.InputFiles.ShouldBe(inputFiles);
        result.OutputPath.ShouldBe(outputPath);

    }

    /// <summary>
    /// Assumes <see cref="BackupConfigurationFactory.FromJson"/> works
    /// </summary>
    [Theory, CombinatorialData]
    public void ConfigurationsCanBeStringified(
        [CombinatorialMemberData(nameof(GenerateBasicStringArrays))] string[] inputFolders,
        [CombinatorialMemberData(nameof(GenerateBasicStringArrays))] string[] inputFiles,
        [CombinatorialMemberData(nameof(GenerateBasicStrings))] string outputPath,
        [CombinatorialValues(true, false)] bool overwriteDuplicates,
        [CombinatorialValues(true, false)] bool zipResult,
        [CombinatorialValues(true, false)] bool createTimestampFolder)
    {
        var factory = new BackupConfigurationFactory();

        var configuration = new BackupConfiguration()
        {
            InputFolders = inputFolders,
            InputFiles = inputFiles,
            OutputPath = outputPath,

            OverwriteDuplicates = overwriteDuplicates,
            ZipResult = zipResult,
            CreateTimestampFolder = createTimestampFolder,
        };

        var result = factory.FromJson(factory.ToJson(configuration));

        result.ShouldNotBeNull();
        result.InputFolders.ShouldBe(configuration.InputFolders);
        result.InputFiles.ShouldBe(configuration.InputFiles);
        result.OutputPath.ShouldBe(configuration.OutputPath);
        result.OverwriteDuplicates.ShouldBe(configuration.OverwriteDuplicates);
        result.ZipResult.ShouldBe(configuration.ZipResult);
        result.CreateTimestampFolder.ShouldBe(configuration.CreateTimestampFolder);
    }

    private static IEnumerable<object[]> GenerateBasicStrings() => GetBasicStringArrays().SelectMany(x => x).Select(x => new object[] { x });
    private static IEnumerable<object[]> GenerateBasicStringArrays() => GetBasicStringArrays().Select(x => new object[] { x });

    private static string[][] GetBasicStringArrays() => new string[][]
    {
        Array.Empty<string>(),
        new string[] { "very_important_data", "something else" },
        new string[] { "smth" },
        new string[] { "balblabalblla" },
        new string[] { "safe this file!", "this is important too", "don't forget me pls!" },
        new string[] { "ThisIsAnOutputPath", "C/supersecret", "SaveMeLocation" },
    };
}
