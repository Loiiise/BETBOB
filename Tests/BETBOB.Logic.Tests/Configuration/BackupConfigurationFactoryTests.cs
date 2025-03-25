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
        configuration.OutputPath.ShouldContain("C");
        configuration.OutputPath.ShouldContain("BETBOB");
    }

    [Theory]
    [InlineData("""
        {
            "folders": ["folderString"],
            "files": ["fileString"],
            "output_path": ""
        }
        """)]
    [InlineData("""
        {
            "folders": ["folderString"],
            "output_path": "",
            "files": ["fileString"]
        }
        """)]
    [InlineData("""
        {
            "output_path": "",
            "folders": ["folderString"],
            "files": ["fileString"]
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
            "another_field": []
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
            "output_path": ""
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
            "output_path": ""
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

    [Fact]
    public void JsonFieldsCanBeParsed()
    {
        var factory = new BackupConfigurationFactory();

        foreach (var inputFolders in new string[][]
            {
                new string[] { "very_important_data", "something else" },
                new string[] { "smth" },
            })
            foreach (var inputFiles in new string[][]
                {
                    new string[] { "balblabalblla" },
                    new string[] { "safe this file!", "this is important too", "don't forget me pls!" },
                })
                foreach (var outputPath in new string[]
                {
                    "ThisIsAnOutputPath",
                    "C/supersecret",
                    "SaveMeLocation",
                })
                {
                    var inputFoldersString = string.Join(',', inputFolders.Select(f => "\"" + f + "\""));
                    var inputFilesString = string.Join(',', inputFiles.Select(f => "\"" + f + "\""));

                    var stringifiedConfiguration = $$"""
                        {
                            "folders": [{{inputFoldersString}}],
                            "files": [{{inputFilesString}}],
                            "output_path": "{{outputPath}}"
                        }
                        """;

                    var result = factory.FromJson(stringifiedConfiguration);

                    result.ShouldNotBeNull();
                    result.InputFolders.ShouldBe(inputFolders);
                    result.InputFiles.ShouldBe(inputFiles);
                    result.OutputPath.ShouldBe(outputPath);
                }
    }

    /// <summary>
    /// Assumes <see cref="BackupConfigurationFactory.FromJson"/> works
    /// </summary>
    [Fact]
    public void ConfigurationsCanBeStringified()
    {
        var factory = new BackupConfigurationFactory();

        foreach (var configuration in new BackupConfiguration[]
            {
                new BackupConfiguration()
                {
                    InputFolders = Array.Empty<string>(),
                    InputFiles = Array.Empty<string>(),
                    OutputPath = string.Empty,
                },
                new BackupConfiguration()
                {
                    InputFolders = new string[] { "special folder", "dont look here" },
                    InputFiles = new string[] { "myfile.secret" },
                    OutputPath = "balbalpath",
                },
                new BackupConfiguration()
                {
                    InputFolders = new string[] { "directory", "dir", "folder", "fldr" },
                    InputFiles = new string[] { "supercoolfile.extension" },
                    OutputPath = "findingmyway",
                },
            })
        {
            var result = factory.FromJson(factory.ToJson(configuration));

            result.ShouldNotBeNull();
            result.InputFolders.ShouldBe(configuration.InputFolders);
            result.InputFiles.ShouldBe(configuration.InputFiles);
            result.OutputPath.ShouldBe(configuration.OutputPath);
        }
    }
}
