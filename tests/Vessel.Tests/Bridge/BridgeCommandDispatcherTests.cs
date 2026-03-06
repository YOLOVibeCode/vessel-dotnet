using Vessel.Bridge;

namespace Vessel.Tests.Bridge;

public class BridgeCommandDispatcherTests
{
    [Fact]
    public void Dispatch_RoutesToCorrectHandler()
    {
        var handler = Substitute.For<IBridgeCommandHandler>();
        handler.CommandType.Returns("testCommand");

        var dispatcher = new BridgeCommandDispatcher([handler]);
        var command = new BridgeCommand("testCommand");

        dispatcher.Dispatch(command);

        handler.Received(1).Handle(command);
    }

    [Fact]
    public void Dispatch_DoesNotCallUnrelatedHandler()
    {
        var handler = Substitute.For<IBridgeCommandHandler>();
        handler.CommandType.Returns("otherCommand");

        var dispatcher = new BridgeCommandDispatcher([handler]);
        var command = new BridgeCommand("testCommand");

        dispatcher.Dispatch(command);

        handler.DidNotReceive().Handle(Arg.Any<BridgeCommand>());
    }

    [Fact]
    public void Dispatch_UnknownCommand_DoesNotThrow()
    {
        var dispatcher = new BridgeCommandDispatcher([]);
        var command = new BridgeCommand("unknownCommand");

        var exception = Record.Exception(() => dispatcher.Dispatch(command));
        Assert.Null(exception);
    }

    [Fact]
    public void Dispatch_NullCommand_DoesNotThrow()
    {
        var dispatcher = new BridgeCommandDispatcher([]);

        var exception = Record.Exception(() => dispatcher.Dispatch(null));
        Assert.Null(exception);
    }

    [Fact]
    public void Dispatch_MultipleHandlers_RoutesCorrectly()
    {
        var handler1 = Substitute.For<IBridgeCommandHandler>();
        handler1.CommandType.Returns("command1");
        var handler2 = Substitute.For<IBridgeCommandHandler>();
        handler2.CommandType.Returns("command2");

        var dispatcher = new BridgeCommandDispatcher([handler1, handler2]);

        dispatcher.Dispatch(new BridgeCommand("command2"));

        handler1.DidNotReceive().Handle(Arg.Any<BridgeCommand>());
        handler2.Received(1).Handle(Arg.Any<BridgeCommand>());
    }
}
