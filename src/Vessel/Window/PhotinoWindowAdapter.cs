using Photino.NET;

namespace Vessel.Window;

public class PhotinoWindowAdapter : IVesselWindow
{
    private readonly PhotinoWindow _window;
    private bool _isTopMost;
    private bool _isFullScreen;

    public PhotinoWindowAdapter(PhotinoWindow window)
    {
        _window = window;
    }

    // IWindowStateController
    public bool IsTopMost => _isTopMost;
    public bool IsFullScreen => _isFullScreen;

    public void SetTitle(string title) => _window.SetTitle(title);

    public void SetSize(int width, int height) => _window.SetSize(width, height);

    public void Center() => _window.Center();

    public void SetTopMost(bool topMost)
    {
        _isTopMost = topMost;
        _window.SetTopMost(topMost);
    }

    public void SetFullScreen(bool fullScreen)
    {
        _isFullScreen = fullScreen;
        _window.SetFullScreen(fullScreen);
    }

    public void SetMinimized(bool minimized) => _window.SetMinimized(minimized);

    // IWindowContentLoader
    public void LoadUrl(string url) => _window.Load(url);

    public void LoadHtml(string html) => _window.LoadRawString(html);

    // IWebMessageBridge
    public void SendMessageToWeb(string message) => _window.SendWebMessage(message);

    public void OnMessageFromWeb(Action<string> handler) =>
        _window.RegisterWebMessageReceivedHandler((_, message) => handler(message));

    // IWindowLifecycle
    public void WaitForClose() => _window.WaitForClose();

    public void Close() => _window.Close();

    // IWindowInitializer
    public void SetDevToolsEnabled(bool enabled) => _window.SetDevToolsEnabled(enabled);

    public void SetIgnoreCertErrors(bool ignore) => _window.SetGrantBrowserPermissions(ignore);

    public void SetResizable(bool resizable) => _window.SetResizable(resizable);

    public void SetIconFile(string path) => _window.SetIconFile(path);

    public void SetZoom(int percent) => _window.SetZoom(percent);
}
