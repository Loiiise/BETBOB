using Microsoft.Extensions.Logging;

namespace BETBOB.Logic.Command;

public class HelpCommand : Command
{
    public HelpCommand(ILogger logger) : base(logger) { }

    public override void Execute()
    {
        throw new NotImplementedException();
    }
}
