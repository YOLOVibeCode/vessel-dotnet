using Vessel.Window;

namespace Vessel.Bridge.Handlers;

public class QuitHandler : IBridgeCommandHandler
{
    private readonly IWindowLifecycle _lifecycle;

    public QuitHandler(IWindowLifecycle lifecycle) => _lifecycle = lifecycle;

    public string CommandType => "quit";

    public void Handle(BridgeCommand command) => _lifecycle.Close();
}
