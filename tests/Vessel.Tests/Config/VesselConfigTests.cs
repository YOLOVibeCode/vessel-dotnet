using Vessel.Config;

namespace Vessel.Tests.Config;

public class VesselConfigTests
{
    [Fact]
    public void DefaultUrl_IsAboutBlank()
    {
        var config = new VesselConfig();
        Assert.Equal("about:blank", config.Url);
    }

    [Fact]
    public void DefaultTitle_IsVessel()
    {
        var config = new VesselConfig();
        Assert.Equal("Vessel", config.Title);
    }

    [Fact]
    public void DefaultWidth_Is1280()
    {
        var config = new VesselConfig();
        Assert.Equal(1280, config.Width);
    }

    [Fact]
    public void DefaultHeight_Is800()
    {
        var config = new VesselConfig();
        Assert.Equal(800, config.Height);
    }

    [Fact]
    public void DefaultAlwaysOnTop_IsFalse()
    {
        var config = new VesselConfig();
        Assert.False(config.AlwaysOnTop);
    }

    [Fact]
    public void CanSetAllProperties()
    {
        var config = new VesselConfig
        {
            Url = "https://test.com",
            Title = "Test",
            Width = 800,
            Height = 600,
            AlwaysOnTop = true
        };

        Assert.Equal("https://test.com", config.Url);
        Assert.Equal("Test", config.Title);
        Assert.Equal(800, config.Width);
        Assert.Equal(600, config.Height);
        Assert.True(config.AlwaysOnTop);
    }
}
