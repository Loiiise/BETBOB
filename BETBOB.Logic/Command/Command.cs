namespace BETBOB.Logic.Command;

public abstract class Command : ICommand
{
    protected Command(string[] arguments)
    {
        Arguments = arguments;
    }

    public string[] Arguments { get; init; }

    public abstract void Execute();
}
