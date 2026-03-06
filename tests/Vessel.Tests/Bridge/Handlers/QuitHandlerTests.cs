using Vessel.Bridge;
using Vessel.Bridge.Handlers;
using Vessel.Window;

namespace Vessel.Tests.Bridge.Handlers;

public class QuitHandlerTests
{
    [Fact]
    public void CommandType_IsQuit()
    {
        var lifecycle = Substitute.For<IWindowLifecycle>();
        Assert.Equal("quit", new QuitHandler(lifecycle).CommandType);
    }

    [Fact]
    public void Handle_CallsClose()
    {
        var lifecycle = Substitute.For<IWindowLifecycle>();
        new QuitHandler(lifecycle).Handle(new BridgeCommand("quit"));
        lifecycle.Received(1).Close();
    }
}
