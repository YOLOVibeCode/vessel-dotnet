namespace Vessel.Config;

public interface IConfigFileReader
{
    VesselConfig? ReadFromFile(string path);
}
