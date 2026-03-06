using Photino.NET;
using Vessel.App;
using Vessel.Bridge;
using Vessel.Bridge.Handlers;
using Vessel.Config;
using Vessel.Window;
using Vessel.Zoom;

// Create the Photino window
var photinoWindow = new PhotinoWindow()
    .SetUseOsDefaultSize(false);

var window = new PhotinoWindowAdapter(photinoWindow);

// Config
var configLoader = new ConfigLoader(
    new ConfigFileLocator(),
    new ConfigFileReader(),
    new ConfigCliParser());

// Bridge
var parser = new BridgeMessageParser();
var scriptProvider = new EmbeddedBridgeScriptProvider();

// Zoom
var zoomController = new ZoomController(window);

// Command handlers (each depends only on narrow interfaces - ISP)
IBridgeCommandHandler[] handlers =
[
    new ToggleFullScreenHandler(window),
    new ToggleTopMostHandler(window),
    new ZoomInHandler(zoomController),
    new ZoomOutHandler(zoomController),
    new ResetZoomHandler(zoomController),
    new TitleChangedHandler(window),
    new MinimizeHandler(window),
    new QuitHandler(window)
];

var dispatcher = new BridgeCommandDispatcher(handlers);

// Run
var app = new VesselApp(window, configLoader, parser, dispatcher, scriptProvider, zoomController);
app.Run(args);
