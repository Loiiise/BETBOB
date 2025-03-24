using BETBOB.Logic.Command;

namespace BETBOB.CLI;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    static void DummyFlow(string[] args, ICommandFactory commandFactory)
    {
        var command = commandFactory.FromArguments(args);
        command.Execute();
    }
}
