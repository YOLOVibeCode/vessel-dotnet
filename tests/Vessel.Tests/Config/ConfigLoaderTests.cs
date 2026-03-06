using Vessel.Config;

namespace Vessel.Tests.Config;

public class ConfigLoaderTests
{
    private readonly IConfigFileLocator _locator = Substitute.For<IConfigFileLocator>();
    private readonly IConfigFileReader _reader = Substitute.For<IConfigFileReader>();
    private readonly IConfigCliParser _cliParser = Substitute.For<IConfigCliParser>();

    private ConfigLoader CreateLoader() => new(_locator, _reader, _cliParser);

    [Fact]
    public void Load_NoConfigFileFound_UsesDefaults()
    {
        _locator.GetCandidatePaths().Returns(new[] { "/a/config.json", "/b/config.json" });
        _reader.ReadFromFile(Arg.Any<string>()).Returns((VesselConfig?)null);
        _cliParser.ApplyOverrides(Arg.Any<VesselConfig>(), Arg.Any<string[]>())
            .Returns(ci => ci.Arg<VesselConfig>());

        var loader = CreateLoader();
        var config = loader.Load([]);

        Assert.Equal("about:blank", config.Url);
        Assert.Equal("Vessel", config.Title);
    }

    [Fact]
    public void Load_ConfigFileExists_UsesFileValues()
    {
        _locator.GetCandidatePaths().Returns(new[] { "/a/config.json" });
        _reader.ReadFromFile("/a/config.json").Returns(new VesselConfig
        {
            Url = "https://fromfile.com",
            Title = "From File"
        });
        _cliParser.ApplyOverrides(Arg.Any<VesselConfig>(), Arg.Any<string[]>())
            .Returns(ci => ci.Arg<VesselConfig>());

        var config = CreateLoader().Load([]);

        Assert.Equal("https://fromfile.com", config.Url);
        Assert.Equal("From File", config.Title);
    }

    [Fact]
    public void Load_CliOverridesFileConfig()
    {
        _locator.GetCandidatePaths().Returns(new[] { "/a/config.json" });
        _reader.ReadFromFile("/a/config.json").Returns(new VesselConfig
        {
            Url = "https://fromfile.com"
        });
        _cliParser.ApplyOverrides(Arg.Any<VesselConfig>(), Arg.Any<string[]>())
            .Returns(ci =>
            {
                var c = ci.Arg<VesselConfig>();
                c.Url = "https://fromcli.com";
                return c;
            });

        var config = CreateLoader().Load(["--url", "https://fromcli.com"]);

        Assert.Equal("https://fromcli.com", config.Url);
    }

    [Fact]
    public void Load_UsesFirstFoundConfigFile()
    {
        _locator.GetCandidatePaths().Returns(new[] { "/first/config.json", "/second/config.json" });
        _reader.ReadFromFile("/first/config.json").Returns(new VesselConfig { Url = "https://first.com" });
        _reader.ReadFromFile("/second/config.json").Returns(new VesselConfig { Url = "https://second.com" });
        _cliParser.ApplyOverrides(Arg.Any<VesselConfig>(), Arg.Any<string[]>())
            .Returns(ci => ci.Arg<VesselConfig>());

        var config = CreateLoader().Load([]);

        Assert.Equal("https://first.com", config.Url);
    }

    [Fact]
    public void Load_SkipsNullAndUsesNextFile()
    {
        _locator.GetCandidatePaths().Returns(new[] { "/missing/config.json", "/found/config.json" });
        _reader.ReadFromFile("/missing/config.json").Returns((VesselConfig?)null);
        _reader.ReadFromFile("/found/config.json").Returns(new VesselConfig { Url = "https://found.com" });
        _cliParser.ApplyOverrides(Arg.Any<VesselConfig>(), Arg.Any<string[]>())
            .Returns(ci => ci.Arg<VesselConfig>());

        var config = CreateLoader().Load([]);

        Assert.Equal("https://found.com", config.Url);
    }

    [Fact]
    public void Load_PassesArgsToCliParser()
    {
        _locator.GetCandidatePaths().Returns(new[] { "/a/config.json" });
        _reader.ReadFromFile(Arg.Any<string>()).Returns((VesselConfig?)null);
        _cliParser.ApplyOverrides(Arg.Any<VesselConfig>(), Arg.Any<string[]>())
            .Returns(ci => ci.Arg<VesselConfig>());

        var args = new[] { "--url", "https://test.com" };
        CreateLoader().Load(args);

        _cliParser.Received(1).ApplyOverrides(Arg.Any<VesselConfig>(), args);
    }
}
