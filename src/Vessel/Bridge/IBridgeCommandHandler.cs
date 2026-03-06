namespace Vessel.Bridge;

public interface IBridgeCommandHandler
{
    string CommandType { get; }
    void Handle(BridgeCommand command);
}
