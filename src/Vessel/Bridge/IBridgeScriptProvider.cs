namespace Vessel.Bridge;

public interface IBridgeScriptProvider
{
    string GetBridgeScript();
    string GetBootstrapHtml(string targetUrl, string bridgeScript);
}
