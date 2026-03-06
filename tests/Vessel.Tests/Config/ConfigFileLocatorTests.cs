using Vessel.Config;

namespace Vessel.Tests.Config;

public class ConfigFileLocatorTests
{
    [Fact]
    public void GetCandidatePaths_ReturnsThreePaths()
    {
        var locator = new ConfigFileLocator();
        var paths = locator.GetCandidatePaths();

        Assert.Equal(3, paths.Count);
    }

    [Fact]
    public void GetCandidatePaths_AllEndWithConfigJson()
    {
        var locator = new ConfigFileLocator();
        var paths = locator.GetCandidatePaths();

        Assert.All(paths, p => Assert.EndsWith("config.json", p));
    }

    [Fact]
    public void GetCandidatePaths_ContainsExeDirectoryPath()
    {
        var locator = new ConfigFileLocator();
        var paths = locator.GetCandidatePaths();

        var exeDir = AppContext.BaseDirectory;
        Assert.Contains(paths, p => p.StartsWith(exeDir));
    }

    [Fact]
    public void GetCandidatePaths_ContainsCurrentDirectoryPath()
    {
        var locator = new ConfigFileLocator();
        var paths = locator.GetCandidatePaths();

        var cwd = Directory.GetCurrentDirectory();
        Assert.Contains(paths, p => p.StartsWith(cwd));
    }
}
