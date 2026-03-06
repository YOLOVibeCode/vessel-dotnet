using Vessel.Window;

namespace Vessel.Bridge.Handlers;

public class ToggleFullScreenHandler : IBridgeCommandHandler
{
    private readonly IWindowStateController _window;

    public ToggleFullScreenHandler(IWindowStateController window) => _window = window;

    public string CommandType => "toggleFullScreen";

    public void Handle(BridgeCommand command) => _window.SetFullScreen(!_window.IsFullScreen);
}
