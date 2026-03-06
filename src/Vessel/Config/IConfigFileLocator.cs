namespace Vessel.Config;

public interface IConfigFileLocator
{
    IReadOnlyList<string> GetCandidatePaths();
}
