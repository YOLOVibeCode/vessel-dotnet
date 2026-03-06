using Vessel.Window;
using Vessel.Zoom;

namespace Vessel.Tests.Zoom;

public class ZoomControllerTests
{
    private readonly IWindowInitializer _window = Substitute.For<IWindowInitializer>();

    private ZoomController CreateController() => new(_window);

    [Fact]
    public void InitialPercent_Is100()
    {
        Assert.Equal(100, CreateController().CurrentPercent);
    }

    [Fact]
    public void ZoomIn_IncreasesBy10Percent()
    {
        var controller = CreateController();
        controller.ZoomIn();

        Assert.Equal(110, controller.CurrentPercent);
        _window.Received(1).SetZoom(110);
    }

    [Fact]
    public void ZoomIn_Twice_CompoundsCorrectly()
    {
        var controller = CreateController();
        controller.ZoomIn();
        controller.ZoomIn();

        Assert.Equal(120, controller.CurrentPercent);
        _window.Received(1).SetZoom(120);
    }

    [Fact]
    public void ZoomOut_DecreasesBy10Percent()
    {
        var controller = CreateController();
        controller.ZoomOut();

        Assert.Equal(90, controller.CurrentPercent);
        _window.Received(1).SetZoom(90);
    }

    [Fact]
    public void ZoomOut_DoesNotGoBelowMinimum()
    {
        var controller = CreateController();
        for (var i = 0; i < 20; i++)
            controller.ZoomOut();

        Assert.True(controller.CurrentPercent >= 30);
    }

    [Fact]
    public void ZoomIn_DoesNotExceedMaximum()
    {
        var controller = CreateController();
        for (var i = 0; i < 50; i++)
            controller.ZoomIn();

        Assert.True(controller.CurrentPercent <= 500);
    }

    [Fact]
    public void ResetZoom_ResetsTo100()
    {
        var controller = CreateController();
        controller.ZoomIn();
        controller.ZoomIn();
        controller.ResetZoom();

        Assert.Equal(100, controller.CurrentPercent);
        _window.Received(1).SetZoom(100);
    }

    [Fact]
    public void ZoomInThenOut_ReturnsToOriginal()
    {
        var controller = CreateController();
        controller.ZoomIn();
        controller.ZoomOut();

        Assert.Equal(100, controller.CurrentPercent);
    }
}
