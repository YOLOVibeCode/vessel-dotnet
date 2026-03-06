using Vessel.App;
using Vessel.Bridge;
using Vessel.Config;
using Vessel.Window;
using Vessel.Zoom;

namespace Vessel.Tests.App;

public class VesselAppTests
{
    private readonly IVesselWindow _window = Substitute.For<IVesselWindow>();
    private readonly IConfigLoader _configLoader = Substitute.For<IConfigLoader>();
    private readonly IBridgeMessageParser _parser = Substitute.For<IBridgeMessageParser>();
    private readonly IBridgeCommandDispatcher _dispatcher = Substitute.For<IBridgeCommandDispatcher>();
    private readonly IBridgeScriptProvider _scriptProvider = Substitute.For<IBridgeScriptProvider>();
    private readonly IZoomController _zoom = Substitute.For<IZoomController>();

    private VesselApp CreateApp() => new(
        _window, _configLoader, _parser, _dispatcher, _scriptProvider, _zoom);

    private void SetupDefaultConfig(string url = "https://example.com", string title = "Test",
        int width = 1280, int height = 800, bool alwaysOnTop = false)
    {
        _configLoader.Load(Arg.Any<string[]>()).Returns(new VesselConfig
        {
            Url = url,
            Title = title,
            Width = width,
            Height = height,
            AlwaysOnTop = alwaysOnTop
        });
        _scriptProvider.GetBridgeScript().Returns("// bridge.js");
        _scriptProvider.GetBootstrapHtml(Arg.Any<string>(), Arg.Any<string>())
            .Returns("<html>bootstrap</html>");
    }

    [Fact]
    public void Run_SetsWindowTitle()
    {
        SetupDefaultConfig(title: "My App");
        CreateApp().Run([]);

        _window.Received(1).SetTitle("My App");
    }

    [Fact]
    public void Run_SetsWindowSize()
    {
        SetupDefaultConfig(width: 1920, height: 1080);
        CreateApp().Run([]);

        _window.Received(1).SetSize(1920, 1080);
    }

    [Fact]
    public void Run_SetsAlwaysOnTop()
    {
        SetupDefaultConfig(alwaysOnTop: true);
        CreateApp().Run([]);

        _window.Received(1).SetTopMost(true);
    }

    [Fact]
    public void Run_LoadsBootstrapHtml()
    {
        SetupDefaultConfig(url: "https://example.com");
        CreateApp().Run([]);

        _scriptProvider.Received(1).GetBootstrapHtml("https://example.com", "// bridge.js");
        _window.Received(1).LoadHtml(Arg.Any<string>());
    }

    [Fact]
    public void Run_RegistersMessageHandler()
    {
        SetupDefaultConfig();
        CreateApp().Run([]);

        _window.Received(1).OnMessageFromWeb(Arg.Any<Action<string>>());
    }

    [Fact]
    public void Run_EnablesDevTools()
    {
        SetupDefaultConfig();
        CreateApp().Run([]);

        _window.Received(1).SetDevToolsEnabled(true);
    }

    [Fact]
    public void Run_CentersWindow()
    {
        SetupDefaultConfig();
        CreateApp().Run([]);

        _window.Received(1).Center();
    }

    [Fact]
    public void Run_WaitsForClose()
    {
        SetupDefaultConfig();
        CreateApp().Run([]);

        _window.Received(1).WaitForClose();
    }

    [Fact]
    public void MessageHandler_ParsesAndDispatches()
    {
        SetupDefaultConfig();
        var app = CreateApp();
        app.Run([]);

        // Capture the registered handler
        var handler = (Action<string>)_window.ReceivedCalls()
            .First(c => c.GetMethodInfo().Name == "OnMessageFromWeb")
            .GetArguments()[0]!;

        var command = new BridgeCommand("testCommand");
        _parser.Parse("test-json").Returns(command);

        handler("test-json");

        _parser.Received(1).Parse("test-json");
        _dispatcher.Received(1).Dispatch(command);
    }
}
