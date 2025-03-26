using BETBOB.Logic.Configuration;
using BETBOB.Logic.FileHandling;

namespace BETBOB.CLI;

internal class Program
{
    static void Main(string[] args)
    {
        var fileCopyer = new FileCopyer();
        var commandFactory = new MagicCommandFactory(
            new BackupConfigurationFactory(),
            fileCopyer,
            new FileReader(),
            new FileWriter(),
            new FolderCopyer(fileCopyer));

        var command = commandFactory.FromArguments(args);
        command.Execute();
    }
}
