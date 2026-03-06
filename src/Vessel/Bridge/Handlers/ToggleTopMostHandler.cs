using Vessel.Window;

namespace Vessel.Bridge.Handlers;

public class ToggleTopMostHandler : IBridgeCommandHandler
{
    private readonly IWindowStateController _window;

    public ToggleTopMostHandler(IWindowStateController window) => _window = window;

    public string CommandType => "toggleTopMost";

    public void Handle(BridgeCommand command) => _window.SetTopMost(!_window.IsTopMost);
}
