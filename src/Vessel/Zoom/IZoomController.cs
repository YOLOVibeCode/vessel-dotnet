namespace Vessel.Zoom;

public interface IZoomController
{
    void ZoomIn();
    void ZoomOut();
    void ResetZoom();
    int CurrentPercent { get; }
}
