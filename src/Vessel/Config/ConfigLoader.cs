namespace Vessel.Config;

public class ConfigLoader : IConfigLoader
{
    private readonly IConfigFileLocator _locator;
    private readonly IConfigFileReader _reader;
    private readonly IConfigCliParser _cliParser;

    public ConfigLoader(IConfigFileLocator locator, IConfigFileReader reader, IConfigCliParser cliParser)
    {
        _locator = locator;
        _reader = reader;
        _cliParser = cliParser;
    }

    public VesselConfig Load(string[] args)
    {
        var config = LoadFromFile() ?? new VesselConfig();
        return _cliParser.ApplyOverrides(config, args);
    }

    private VesselConfig? LoadFromFile()
    {
        foreach (var path in _locator.GetCandidatePaths())
        {
            var config = _reader.ReadFromFile(path);
            if (config is not null)
                return config;
        }

        return null;
    }
}
