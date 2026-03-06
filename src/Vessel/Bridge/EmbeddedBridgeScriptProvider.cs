using System.Reflection;

namespace Vessel.Bridge;

public class EmbeddedBridgeScriptProvider : IBridgeScriptProvider
{
    private const string BridgeScriptResource = "Vessel.Resources.bridge.js";
    private const string IndexHtmlResource = "Vessel.Resources.index.html";

    public string GetBridgeScript()
    {
        return ReadEmbeddedResource(BridgeScriptResource);
    }

    public string GetBootstrapHtml(string targetUrl, string bridgeScript)
    {
        var html = ReadEmbeddedResource(IndexHtmlResource);
        return html
            .Replace("{{TARGET_URL}}", targetUrl)
            .Replace("{{BRIDGE_SCRIPT}}", bridgeScript);
    }

    private static string ReadEmbeddedResource(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream(resourceName)
            ?? throw new InvalidOperationException($"Embedded resource '{resourceName}' not found.");
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}
