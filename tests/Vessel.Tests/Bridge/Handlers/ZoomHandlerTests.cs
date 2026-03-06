using Vessel.Bridge;
using Vessel.Bridge.Handlers;
using Vessel.Zoom;

namespace Vessel.Tests.Bridge.Handlers;

public class ZoomHandlerTests
{
    [Fact]
    public void ZoomInHandler_CommandType_IsZoomIn()
    {
        var zoom = Substitute.For<IZoomController>();
        Assert.Equal("zoomIn", new ZoomInHandler(zoom).CommandType);
    }

    [Fact]
    public void ZoomInHandler_Handle_CallsZoomIn()
    {
        var zoom = Substitute.For<IZoomController>();
        new ZoomInHandler(zoom).Handle(new BridgeCommand("zoomIn"));
        zoom.Received(1).ZoomIn();
    }

    [Fact]
    public void ZoomOutHandler_CommandType_IsZoomOut()
    {
        var zoom = Substitute.For<IZoomController>();
        Assert.Equal("zoomOut", new ZoomOutHandler(zoom).CommandType);
    }

    [Fact]
    public void ZoomOutHandler_Handle_CallsZoomOut()
    {
        var zoom = Substitute.For<IZoomController>();
        new ZoomOutHandler(zoom).Handle(new BridgeCommand("zoomOut"));
        zoom.Received(1).ZoomOut();
    }

    [Fact]
    public void ResetZoomHandler_CommandType_IsResetZoom()
    {
        var zoom = Substitute.For<IZoomController>();
        Assert.Equal("resetZoom", new ResetZoomHandler(zoom).CommandType);
    }

    [Fact]
    public void ResetZoomHandler_Handle_CallsResetZoom()
    {
        var zoom = Substitute.For<IZoomController>();
        new ResetZoomHandler(zoom).Handle(new BridgeCommand("resetZoom"));
        zoom.Received(1).ResetZoom();
    }
}
