using BETBOB.Logic.Command;

namespace BETBOB.CLI;

internal class MagicCommandFactory : ICommandFactory
{
    public ICommand FromArguments(string[] arguments)
    {
        if (arguments.Length < 1) throw new ArgumentException("No command string was provided");

        var commandConfigurations = _commandConfigurations.Where(c => c.Tag == arguments[0]);
        if (commandConfigurations.Count() != 1) throw new ArgumentException($"Invalid command: {arguments[0]}");

        var commandConfiguration = commandConfigurations.Single();
        if (arguments.Length != 1 + commandConfiguration.ExpectedAmountOfArguments) throw new ArgumentException($"{commandConfiguration.Tag} expected {commandConfiguration.ExpectedAmountOfArguments}, received {arguments.Length - 1}");

        return commandConfiguration.CommandName switch
        {
            nameof(BackupCommand) => new BackupCommand(),
            nameof(HelpCommand) => new HelpCommand(),
            nameof(InitializeConfigurationCommand) => new InitializeConfigurationCommand(),
            _ => throw new ArgumentException("Command not found"),
        };
    }

    public ICommand FromString(string commandString) => FromArguments(commandString.Split(' '));

    private CommandConfiguration[] _commandConfigurations = new[]
    {
        new CommandConfiguration(nameof(BackupCommand), "backup", 0),
        new CommandConfiguration(nameof(HelpCommand), "help", 0),
        new CommandConfiguration(nameof(InitializeConfigurationCommand), "init", 0),
    }; 

    private record CommandConfiguration(string CommandName, string Tag, int ExpectedAmountOfArguments);
}

