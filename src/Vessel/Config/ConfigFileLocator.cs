namespace Vessel.Config;

public class ConfigFileLocator : IConfigFileLocator
{
    private const string ConfigFileName = "config.json";

    public IReadOnlyList<string> GetCandidatePaths()
    {
        return new[]
        {
            // 1. Next to the executable
            Path.Combine(AppContext.BaseDirectory, ConfigFileName),
            // 2. macOS .app bundle Resources directory
            Path.Combine(AppContext.BaseDirectory, "..", "Resources", ConfigFileName),
            // 3. Current working directory
            Path.Combine(Directory.GetCurrentDirectory(), ConfigFileName)
        };
    }
}
