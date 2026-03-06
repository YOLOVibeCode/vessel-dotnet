namespace Vessel.Bridge;

public interface IBridgeCommandDispatcher
{
    void Dispatch(BridgeCommand? command);
}
