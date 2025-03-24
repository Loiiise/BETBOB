namespace BETBOB.Logic.Command;

public interface ICommandFactory
{
    ICommand FromArguments(string[] arguments);
    ICommand FromString(string commandString);
}
