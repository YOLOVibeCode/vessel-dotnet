namespace Vessel.Window;

public interface IVesselWindow :
    IWindowStateController,
    IWindowContentLoader,
    IWebMessageBridge,
    IWindowLifecycle,
    IWindowInitializer
{
}
