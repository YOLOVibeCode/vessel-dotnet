namespace Vessel.Window;

public interface IWindowInitializer
{
    void SetDevToolsEnabled(bool enabled);
    void SetIgnoreCertErrors(bool ignore);
    void SetResizable(bool resizable);
    void SetIconFile(string path);
    void SetZoom(int percent);
}
