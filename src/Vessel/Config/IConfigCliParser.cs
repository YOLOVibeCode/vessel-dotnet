namespace Vessel.Config;

public interface IConfigCliParser
{
    VesselConfig ApplyOverrides(VesselConfig config, string[] args);
}
