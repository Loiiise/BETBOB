namespace BETBOB.Logic.Command;

public interface ICommand
{
    string[] Arguments { get; init; }
    void Execute();
}
