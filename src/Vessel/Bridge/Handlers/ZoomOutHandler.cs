using Vessel.Zoom;

namespace Vessel.Bridge.Handlers;

public class ZoomOutHandler : IBridgeCommandHandler
{
    private readonly IZoomController _zoom;

    public ZoomOutHandler(IZoomController zoom) => _zoom = zoom;

    public string CommandType => "zoomOut";

    public void Handle(BridgeCommand command) => _zoom.ZoomOut();
}
