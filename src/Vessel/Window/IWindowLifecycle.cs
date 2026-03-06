namespace Vessel.Window;

public interface IWindowLifecycle
{
    void WaitForClose();
    void Close();
}
