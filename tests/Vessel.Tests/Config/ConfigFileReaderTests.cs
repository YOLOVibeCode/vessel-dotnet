using Vessel.Config;

namespace Vessel.Tests.Config;

public class ConfigFileReaderTests
{
    private readonly ConfigFileReader _reader = new();

    private string FixturePath(string filename) =>
        Path.Combine(AppContext.BaseDirectory, "Fixtures", "TestConfigs", filename);

    [Fact]
    public void ReadFromFile_ValidFullJson_ReturnsConfigWithAllProperties()
    {
        var config = _reader.ReadFromFile(FixturePath("valid_full.json"));

        Assert.NotNull(config);
        Assert.Equal("https://example.com", config!.Url);
        Assert.Equal("My App", config.Title);
        Assert.Equal(1920, config.Width);
        Assert.Equal(1080, config.Height);
        Assert.True(config.AlwaysOnTop);
    }

    [Fact]
    public void ReadFromFile_ValidPartialJson_ReturnsConfigWithDefaults()
    {
        var config = _reader.ReadFromFile(FixturePath("valid_partial.json"));

        Assert.NotNull(config);
        Assert.Equal("https://partial.example.com", config!.Url);
        Assert.Equal("Vessel", config.Title);
        Assert.Equal(1280, config.Width);
        Assert.Equal(800, config.Height);
        Assert.False(config.AlwaysOnTop);
    }

    [Fact]
    public void ReadFromFile_InvalidJson_ReturnsNull()
    {
        var config = _reader.ReadFromFile(FixturePath("invalid.json"));
        Assert.Null(config);
    }

    [Fact]
    public void ReadFromFile_MissingFile_ReturnsNull()
    {
        var config = _reader.ReadFromFile("/nonexistent/path/config.json");
        Assert.Null(config);
    }

    [Fact]
    public void ReadFromFile_EmptyFile_ReturnsNull()
    {
        var config = _reader.ReadFromFile(FixturePath("empty.json"));
        Assert.Null(config);
    }
}
