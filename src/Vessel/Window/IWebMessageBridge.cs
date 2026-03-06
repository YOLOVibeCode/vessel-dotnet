namespace Vessel.Window;

public interface IWebMessageBridge
{
    void SendMessageToWeb(string message);
    void OnMessageFromWeb(Action<string> handler);
}
