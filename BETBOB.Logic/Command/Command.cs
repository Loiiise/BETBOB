using Microsoft.Extensions.Logging;

namespace BETBOB.Logic.Command;

public abstract class Command : ICommand
{
    public Command(ILogger logger)
    {
        _logger = logger;
    }

    public abstract void Execute();

    protected readonly ILogger _logger;
}
