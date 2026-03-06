namespace Vessel.Config;

public class VesselConfig
{
    public string Url { get; set; } = "about:blank";
    public string Title { get; set; } = "Vessel";
    public int Width { get; set; } = 1280;
    public int Height { get; set; } = 800;
    public bool AlwaysOnTop { get; set; }
}
