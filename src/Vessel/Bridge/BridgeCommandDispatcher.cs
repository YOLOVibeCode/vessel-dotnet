namespace Vessel.Bridge;

public class BridgeCommandDispatcher : IBridgeCommandDispatcher
{
    private readonly Dictionary<string, IBridgeCommandHandler> _handlers;

    public BridgeCommandDispatcher(IEnumerable<IBridgeCommandHandler> handlers)
    {
        _handlers = handlers.ToDictionary(h => h.CommandType);
    }

    public void Dispatch(BridgeCommand? command)
    {
        if (command is null)
            return;

        if (_handlers.TryGetValue(command.Type, out var handler))
            handler.Handle(command);
    }
}
