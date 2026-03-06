namespace Vessel.Bridge;

public interface IBridgeMessageParser
{
    BridgeCommand? Parse(string json);
}
