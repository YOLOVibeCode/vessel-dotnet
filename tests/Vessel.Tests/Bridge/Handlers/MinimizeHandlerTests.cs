using Vessel.Bridge;
using Vessel.Bridge.Handlers;
using Vessel.Window;

namespace Vessel.Tests.Bridge.Handlers;

public class MinimizeHandlerTests
{
    [Fact]
    public void CommandType_IsMinimize()
    {
        var window = Substitute.For<IWindowStateController>();
        Assert.Equal("minimize", new MinimizeHandler(window).CommandType);
    }

    [Fact]
    public void Handle_CallsSetMinimized()
    {
        var window = Substitute.For<IWindowStateController>();
        new MinimizeHandler(window).Handle(new BridgeCommand("minimize"));
        window.Received(1).SetMinimized(true);
    }
}
