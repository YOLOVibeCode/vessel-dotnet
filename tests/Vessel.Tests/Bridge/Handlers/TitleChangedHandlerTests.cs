using Vessel.Bridge;
using Vessel.Bridge.Handlers;
using Vessel.Window;

namespace Vessel.Tests.Bridge.Handlers;

public class TitleChangedHandlerTests
{
    [Fact]
    public void CommandType_IsTitleChanged()
    {
        var window = Substitute.For<IWindowStateController>();
        Assert.Equal("titleChanged", new TitleChangedHandler(window).CommandType);
    }

    [Fact]
    public void Handle_WithTitle_CallsSetTitle()
    {
        var window = Substitute.For<IWindowStateController>();
        new TitleChangedHandler(window).Handle(new BridgeCommand("titleChanged", "New Title"));
        window.Received(1).SetTitle("New Title");
    }

    [Fact]
    public void Handle_WithNullData_DoesNotCallSetTitle()
    {
        var window = Substitute.For<IWindowStateController>();
        new TitleChangedHandler(window).Handle(new BridgeCommand("titleChanged"));
        window.DidNotReceive().SetTitle(Arg.Any<string>());
    }

    [Fact]
    public void Handle_WithEmptyData_DoesNotCallSetTitle()
    {
        var window = Substitute.For<IWindowStateController>();
        new TitleChangedHandler(window).Handle(new BridgeCommand("titleChanged", ""));
        window.DidNotReceive().SetTitle(Arg.Any<string>());
    }
}
