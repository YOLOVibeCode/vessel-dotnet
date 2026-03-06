using System.Text.Json;

namespace Vessel.Bridge;

public class BridgeMessageParser : IBridgeMessageParser
{
    public BridgeCommand? Parse(string json)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(json))
                return null;

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (!root.TryGetProperty("type", out var typeElement) ||
                typeElement.ValueKind != JsonValueKind.String)
                return null;

            var type = typeElement.GetString();
            if (string.IsNullOrEmpty(type))
                return null;

            string? data = null;
            if (root.TryGetProperty("data", out var dataElement) &&
                dataElement.ValueKind == JsonValueKind.String)
            {
                data = dataElement.GetString();
            }

            return new BridgeCommand(type, data);
        }
        catch
        {
            return null;
        }
    }
}
