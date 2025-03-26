using BETBOB.Logic.Command;
using BETBOB.Logic.Configuration;
using BETBOB.Logic.FileHandling;
using Microsoft.Extensions.Logging;

namespace BETBOB.CLI;

internal class MagicCommandFactory
{
    internal MagicCommandFactory(
        ILogger logger,
        IBackupConfigurationFactory backupConfigurationFactory,
        IFileCopyer fileCopyer,
        IFileReader fileReader,
        IFileWriter fileWriter,
        IFolderCopyer folderCopyer)
    {
        _logger = logger;
        _backupConfigurationFactory = backupConfigurationFactory;
        _fileCopyer = fileCopyer;
        _fileReader = fileReader;
        _fileWriter = fileWriter;
        _folderCopyer = folderCopyer;
    }

    public ICommand FromArguments(string[] arguments)
    {
        if (arguments.Length < 1) throw new ArgumentException("No command string was provided");

        var commandConfigurations = _commandConfigurations.Where(c => c.Tag == arguments[0]);
        if (commandConfigurations.Count() != 1) throw new ArgumentException($"Invalid command: {arguments[0]}");

        // First items is the name of the command
        var commandArguments = arguments.Skip(1).ToArray();

        var commandConfiguration = commandConfigurations.Single();
        if (commandArguments.Length < commandConfiguration.MinimumAmountOfArguments || commandArguments.Length > commandConfiguration.MaximumAmountOfArguments)
            throw new ArgumentException($"{commandConfiguration.Tag} expected between {commandConfiguration.MinimumAmountOfArguments} and {commandConfiguration.MaximumAmountOfArguments}, received {commandArguments.Length}");

        return commandConfiguration.CommandName switch
        {
            nameof(BackupCommand) => commandArguments.Length > 0 ? 
                new BackupCommand(_logger, _fileReader, _backupConfigurationFactory, _fileCopyer, _folderCopyer, _fileWriter, commandArguments[0]) :
                new BackupCommand(_logger, _fileReader, _backupConfigurationFactory, _fileCopyer, _folderCopyer, _fileWriter),
            nameof(CleanupCommand) => new CleanupCommand(_logger),
            nameof(HelpCommand) => new HelpCommand(_logger),
            nameof(InitializeConfigurationCommand) => new InitializeConfigurationCommand(_logger, _backupConfigurationFactory, _fileWriter),
            _ => throw new ArgumentException("Command not found"),
        };
    }

    private CommandConfiguration[] _commandConfigurations = new[]
    {
        new CommandConfiguration(nameof(BackupCommand), "backup", 0, 1),
        new CommandConfiguration(nameof(CleanupCommand), "cleanup", 0, 0),
        new CommandConfiguration(nameof(HelpCommand), "help", 0, 0),
        new CommandConfiguration(nameof(InitializeConfigurationCommand), "init", 0, 0),
    };
    private readonly ILogger _logger;
    private readonly IBackupConfigurationFactory _backupConfigurationFactory;
    private readonly IFileCopyer _fileCopyer;
    private readonly IFileReader _fileReader;
    private readonly IFileWriter _fileWriter;
    private readonly IFolderCopyer _folderCopyer;

    private record CommandConfiguration(string CommandName, string Tag, int MinimumAmountOfArguments, int MaximumAmountOfArguments);
}

