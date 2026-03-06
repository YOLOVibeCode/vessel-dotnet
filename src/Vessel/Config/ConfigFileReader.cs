using System.Text.Json;

namespace Vessel.Config;

public class ConfigFileReader : IConfigFileReader
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public VesselConfig? ReadFromFile(string path)
    {
        try
        {
            if (!File.Exists(path))
                return null;

            var json = File.ReadAllText(path);
            if (string.IsNullOrWhiteSpace(json))
                return null;

            return JsonSerializer.Deserialize<VesselConfig>(json, JsonOptions);
        }
        catch
        {
            return null;
        }
    }
}
