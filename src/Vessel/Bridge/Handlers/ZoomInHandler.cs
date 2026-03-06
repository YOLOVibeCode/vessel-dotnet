using Vessel.Zoom;

namespace Vessel.Bridge.Handlers;

public class ZoomInHandler : IBridgeCommandHandler
{
    private readonly IZoomController _zoom;

    public ZoomInHandler(IZoomController zoom) => _zoom = zoom;

    public string CommandType => "zoomIn";

    public void Handle(BridgeCommand command) => _zoom.ZoomIn();
}
