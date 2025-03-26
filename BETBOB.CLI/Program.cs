using BETBOB.Logic.Configuration;
using BETBOB.Logic.FileHandling;
using Microsoft.Extensions.Logging;

namespace BETBOB.CLI;

internal class Program
{
    static void Main(string[] args)
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
        ILogger logger = factory.CreateLogger("BETBOB");
        
        var fileCopyer = new FileCopyer();
        var commandFactory = new MagicCommandFactory(
            logger,
            new BackupConfigurationFactory(),
            fileCopyer,
            new FileReader(),
            new FileWriter(),
            new FolderCopyer(fileCopyer));

        var command = commandFactory.FromArguments(args);
        command.Execute();
    }
}
