using Vessel.Window;

namespace Vessel.Zoom;

public class ZoomController : IZoomController
{
    private const int Step = 10;
    private const int MinPercent = 30;
    private const int MaxPercent = 500;

    private readonly IWindowInitializer _window;

    public ZoomController(IWindowInitializer window)
    {
        _window = window;
    }

    public int CurrentPercent { get; private set; } = 100;

    public void ZoomIn()
    {
        CurrentPercent = Math.Min(CurrentPercent + Step, MaxPercent);
        _window.SetZoom(CurrentPercent);
    }

    public void ZoomOut()
    {
        CurrentPercent = Math.Max(CurrentPercent - Step, MinPercent);
        _window.SetZoom(CurrentPercent);
    }

    public void ResetZoom()
    {
        CurrentPercent = 100;
        _window.SetZoom(CurrentPercent);
    }
}
