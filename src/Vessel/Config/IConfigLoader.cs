namespace Vessel.Config;

public interface IConfigLoader
{
    VesselConfig Load(string[] args);
}
