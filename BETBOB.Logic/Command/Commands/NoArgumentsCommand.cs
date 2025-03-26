namespace BETBOB.Logic.Command;

public abstract class NoArgumentsCommand : Command
{
    protected NoArgumentsCommand() : base(Array.Empty<string>()) { }
}
