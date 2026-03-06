using Vessel.Zoom;

namespace Vessel.Bridge.Handlers;

public class ResetZoomHandler : IBridgeCommandHandler
{
    private readonly IZoomController _zoom;

    public ResetZoomHandler(IZoomController zoom) => _zoom = zoom;

    public string CommandType => "resetZoom";

    public void Handle(BridgeCommand command) => _zoom.ResetZoom();
}
