using Vessel.Window;

namespace Vessel.Bridge.Handlers;

public class TitleChangedHandler : IBridgeCommandHandler
{
    private readonly IWindowStateController _window;

    public TitleChangedHandler(IWindowStateController window) => _window = window;

    public string CommandType => "titleChanged";

    public void Handle(BridgeCommand command)
    {
        if (!string.IsNullOrEmpty(command.Data))
            _window.SetTitle(command.Data);
    }
}
