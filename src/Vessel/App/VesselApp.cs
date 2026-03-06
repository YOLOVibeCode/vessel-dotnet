using Vessel.Bridge;
using Vessel.Config;
using Vessel.Window;
using Vessel.Zoom;

namespace Vessel.App;

public class VesselApp
{
    private readonly IVesselWindow _window;
    private readonly IConfigLoader _configLoader;
    private readonly IBridgeMessageParser _parser;
    private readonly IBridgeCommandDispatcher _dispatcher;
    private readonly IBridgeScriptProvider _scriptProvider;
    private readonly IZoomController _zoom;

    public VesselApp(
        IVesselWindow window,
        IConfigLoader configLoader,
        IBridgeMessageParser parser,
        IBridgeCommandDispatcher dispatcher,
        IBridgeScriptProvider scriptProvider,
        IZoomController zoom)
    {
        _window = window;
        _configLoader = configLoader;
        _parser = parser;
        _dispatcher = dispatcher;
        _scriptProvider = scriptProvider;
        _zoom = zoom;
    }

    public void Run(string[] args)
    {
        var config = _configLoader.Load(args);

        _window.SetTitle(config.Title);
        _window.SetSize(config.Width, config.Height);
        _window.SetTopMost(config.AlwaysOnTop);
        _window.SetDevToolsEnabled(true);
        _window.Center();

        _window.OnMessageFromWeb(message =>
        {
            var command = _parser.Parse(message);
            _dispatcher.Dispatch(command);
        });

        var bridgeScript = _scriptProvider.GetBridgeScript();
        var html = _scriptProvider.GetBootstrapHtml(config.Url, bridgeScript);
        _window.LoadHtml(html);

        _window.WaitForClose();
    }
}
