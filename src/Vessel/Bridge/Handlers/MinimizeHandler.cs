using Vessel.Window;

namespace Vessel.Bridge.Handlers;

public class MinimizeHandler : IBridgeCommandHandler
{
    private readonly IWindowStateController _window;

    public MinimizeHandler(IWindowStateController window) => _window = window;

    public string CommandType => "minimize";

    public void Handle(BridgeCommand command) => _window.SetMinimized(true);
}
