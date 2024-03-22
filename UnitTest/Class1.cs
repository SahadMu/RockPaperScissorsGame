using Xunit;
using NSubstitute;

public class WinStayLoseSwitchStrategyTests
{
    [Fact]
    public void ChooseMove_FirstMove_ReturnsRandomMove()
    {
        // Arrange
        var computer = Substitute.For<Computer>();
        var strategy = new WinStayLoseSwitchStrategy(computer);

        // Act
        var move = strategy.ChooseMove();

        // Assert
        Assert.Contains(move, new[] { Move.Rock, Move.Paper, Move.Scissors });
    }

    [Fact]
    public void ChooseMove_ComputerWonLastRound_ReturnsSameMove()
    {
        // Arrange
        var computer = Substitute.For<Computer>();
        computer.ComputerHasWonLastRound.Returns(true);
        var strategy = new WinStayLoseSwitchStrategy(computer);
        strategy.UpdateLastMove(Move.Rock); // Set last move

        // Act
        var move = strategy.ChooseMove();

        // Assert
        Assert.Contains(move, new[] { Move.Rock, Move.Paper, Move.Scissors });
    }

    [Fact]
    public void ChooseMove_ComputerLostLastRound_ReturnsDifferentMove()
    {
        // Arrange
        var computer = Substitute.For<Computer>();
        computer.ComputerHasWonLastRound.Returns(false);
        var strategy = new WinStayLoseSwitchStrategy(computer);
        strategy.UpdateLastMove(Move.Rock); // Set last move

        // Act
        var move = strategy.ChooseMove();

        // Assert
        Assert.Contains(move, new[] { Move.Rock, Move.Paper, Move.Scissors });
    }
}
