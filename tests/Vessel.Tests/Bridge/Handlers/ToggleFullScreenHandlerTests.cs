using Vessel.Bridge;
using Vessel.Bridge.Handlers;
using Vessel.Window;

namespace Vessel.Tests.Bridge.Handlers;

public class ToggleFullScreenHandlerTests
{
    [Fact]
    public void CommandType_IsToggleFullScreen()
    {
        var window = Substitute.For<IWindowStateController>();
        var handler = new ToggleFullScreenHandler(window);

        Assert.Equal("toggleFullScreen", handler.CommandType);
    }

    [Fact]
    public void Handle_WhenNotFullScreen_SetsFullScreenTrue()
    {
        var window = Substitute.For<IWindowStateController>();
        window.IsFullScreen.Returns(false);

        var handler = new ToggleFullScreenHandler(window);
        handler.Handle(new BridgeCommand("toggleFullScreen"));

        window.Received(1).SetFullScreen(true);
    }

    [Fact]
    public void Handle_WhenFullScreen_SetsFullScreenFalse()
    {
        var window = Substitute.For<IWindowStateController>();
        window.IsFullScreen.Returns(true);

        var handler = new ToggleFullScreenHandler(window);
        handler.Handle(new BridgeCommand("toggleFullScreen"));

        window.Received(1).SetFullScreen(false);
    }
}
