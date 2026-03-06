using Vessel.Bridge;

namespace Vessel.Tests.Bridge;

public class BridgeMessageParserTests
{
    private readonly BridgeMessageParser _parser = new();

    [Fact]
    public void Parse_ValidCommandNoData_ReturnsBridgeCommand()
    {
        var result = _parser.Parse("""{"type":"toggleFullScreen"}""");

        Assert.NotNull(result);
        Assert.Equal("toggleFullScreen", result!.Type);
        Assert.Null(result.Data);
    }

    [Fact]
    public void Parse_ValidCommandWithData_ReturnsBridgeCommandWithData()
    {
        var result = _parser.Parse("""{"type":"titleChanged","data":"New Title"}""");

        Assert.NotNull(result);
        Assert.Equal("titleChanged", result!.Type);
        Assert.Equal("New Title", result.Data);
    }

    [Fact]
    public void Parse_ZoomInCommand_ReturnsBridgeCommand()
    {
        var result = _parser.Parse("""{"type":"zoomIn"}""");

        Assert.NotNull(result);
        Assert.Equal("zoomIn", result!.Type);
    }

    [Fact]
    public void Parse_InvalidJson_ReturnsNull()
    {
        var result = _parser.Parse("not json at all");
        Assert.Null(result);
    }

    [Fact]
    public void Parse_MissingType_ReturnsNull()
    {
        var result = _parser.Parse("""{"data":"some data"}""");
        Assert.Null(result);
    }

    [Fact]
    public void Parse_EmptyString_ReturnsNull()
    {
        var result = _parser.Parse("");
        Assert.Null(result);
    }

    [Fact]
    public void Parse_NullType_ReturnsNull()
    {
        var result = _parser.Parse("""{"type":null}""");
        Assert.Null(result);
    }

    [Fact]
    public void Parse_EmptyType_ReturnsNull()
    {
        var result = _parser.Parse("""{"type":""}""");
        Assert.Null(result);
    }
}
