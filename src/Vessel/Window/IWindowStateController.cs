namespace Vessel.Window;

public interface IWindowStateController
{
    void SetTitle(string title);
    void SetSize(int width, int height);
    void Center();
    void SetTopMost(bool topMost);
    void SetFullScreen(bool fullScreen);
    void SetMinimized(bool minimized);
    bool IsTopMost { get; }
    bool IsFullScreen { get; }
}
