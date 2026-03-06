using Vessel.Bridge;
using Vessel.Bridge.Handlers;
using Vessel.Window;

namespace Vessel.Tests.Bridge.Handlers;

public class ToggleTopMostHandlerTests
{
    [Fact]
    public void CommandType_IsToggleTopMost()
    {
        var window = Substitute.For<IWindowStateController>();
        var handler = new ToggleTopMostHandler(window);

        Assert.Equal("toggleTopMost", handler.CommandType);
    }

    [Fact]
    public void Handle_WhenNotTopMost_SetsTopMostTrue()
    {
        var window = Substitute.For<IWindowStateController>();
        window.IsTopMost.Returns(false);

        var handler = new ToggleTopMostHandler(window);
        handler.Handle(new BridgeCommand("toggleTopMost"));

        window.Received(1).SetTopMost(true);
    }

    [Fact]
    public void Handle_WhenTopMost_SetsTopMostFalse()
    {
        var window = Substitute.For<IWindowStateController>();
        window.IsTopMost.Returns(true);

        var handler = new ToggleTopMostHandler(window);
        handler.Handle(new BridgeCommand("toggleTopMost"));

        window.Received(1).SetTopMost(false);
    }
}
