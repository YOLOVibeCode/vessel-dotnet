using Vessel.Config;

namespace Vessel.Tests.Config;

public class ConfigCliParserTests
{
    private readonly ConfigCliParser _parser = new();

    private VesselConfig DefaultConfig() => new();

    [Fact]
    public void ApplyOverrides_NoArgs_ReturnsUnchangedConfig()
    {
        var config = DefaultConfig();
        var result = _parser.ApplyOverrides(config, []);

        Assert.Equal("about:blank", result.Url);
        Assert.Equal("Vessel", result.Title);
        Assert.Equal(1280, result.Width);
        Assert.Equal(800, result.Height);
        Assert.False(result.AlwaysOnTop);
    }

    [Fact]
    public void ApplyOverrides_UrlFlag_SetsUrl()
    {
        var result = _parser.ApplyOverrides(DefaultConfig(), ["--url", "https://test.com"]);
        Assert.Equal("https://test.com", result.Url);
    }

    [Fact]
    public void ApplyOverrides_TitleFlag_SetsTitle()
    {
        var result = _parser.ApplyOverrides(DefaultConfig(), ["--title", "My Title"]);
        Assert.Equal("My Title", result.Title);
    }

    [Fact]
    public void ApplyOverrides_WidthFlag_SetsWidth()
    {
        var result = _parser.ApplyOverrides(DefaultConfig(), ["--width", "1920"]);
        Assert.Equal(1920, result.Width);
    }

    [Fact]
    public void ApplyOverrides_HeightFlag_SetsHeight()
    {
        var result = _parser.ApplyOverrides(DefaultConfig(), ["--height", "1080"]);
        Assert.Equal(1080, result.Height);
    }

    [Fact]
    public void ApplyOverrides_FloatFlag_SetsAlwaysOnTop()
    {
        var result = _parser.ApplyOverrides(DefaultConfig(), ["--float"]);
        Assert.True(result.AlwaysOnTop);
    }

    [Fact]
    public void ApplyOverrides_NoFloatFlag_UnsetsAlwaysOnTop()
    {
        var config = new VesselConfig { AlwaysOnTop = true };
        var result = _parser.ApplyOverrides(config, ["--no-float"]);
        Assert.False(result.AlwaysOnTop);
    }

    [Fact]
    public void ApplyOverrides_MultipleArgs_AppliesAll()
    {
        var result = _parser.ApplyOverrides(DefaultConfig(),
            ["--url", "https://multi.com", "--title", "Multi", "--width", "800", "--height", "600", "--float"]);

        Assert.Equal("https://multi.com", result.Url);
        Assert.Equal("Multi", result.Title);
        Assert.Equal(800, result.Width);
        Assert.Equal(600, result.Height);
        Assert.True(result.AlwaysOnTop);
    }

    [Fact]
    public void ApplyOverrides_InvalidWidth_IgnoresIt()
    {
        var result = _parser.ApplyOverrides(DefaultConfig(), ["--width", "notanumber"]);
        Assert.Equal(1280, result.Width);
    }

    [Fact]
    public void ApplyOverrides_InvalidHeight_IgnoresIt()
    {
        var result = _parser.ApplyOverrides(DefaultConfig(), ["--height", "notanumber"]);
        Assert.Equal(800, result.Height);
    }

    [Fact]
    public void ApplyOverrides_UnknownFlag_IgnoresIt()
    {
        var result = _parser.ApplyOverrides(DefaultConfig(), ["--unknown", "value"]);
        Assert.Equal("about:blank", result.Url);
    }
}
