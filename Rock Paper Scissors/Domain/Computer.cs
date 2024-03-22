using System.ComponentModel.Design;

public class Computer
{
    public IComputerMoveStrategy ComputerMoveStrategy;
    public static string Name { get; } = "Sandra";

    public bool ComputerHasWonLastRound { get; set; }

    public Computer()

    {
        ComputerMoveStrategy = new WinStayLoseSwitchStrategy(this);
    
    }
    public Move ChooseMove()
    {
       return ComputerMoveStrategy.ChooseMove();
    }
    public void UpdateResult(bool hasWon)
    {
        ComputerHasWonLastRound = hasWon;
    }
}

public interface IComputerMoveStrategy {
    Move ChooseMove();
}

public class RandomMoveStrategy : IComputerMoveStrategy
{
    public Move ChooseMove()
    {
        Random random = new Random();
        return (Move)random.Next(1, 4);
    }
}
public class WinStayLoseSwitchStrategy : IComputerMoveStrategy
{
    private Computer _computer;
    private Move lastMove;
    private Random random;

    public WinStayLoseSwitchStrategy(Computer computer)
    {
        _computer = computer;
        random = new Random();
    }

    public Move ChooseMove()
    {
        if (lastMove == Move.None)
        {
            // If it's the first move, choose randomly
            lastMove = (Move)random.Next(1, 4);
            return lastMove;
        }

        if (_computer.ComputerHasWonLastRound)
        {
            // If the computer won last round, stay with the same move
            return lastMove;
        }
        else
        {
            var potentialMoves = new List<Move> { Move.Rock, Move.Paper, Move.Scissors };
            potentialMoves = potentialMoves.Where(x => x != lastMove).ToList();
            var moveIndex = random.Next(0, potentialMoves.Count);
            // If the computer lost last round, switch to a different move
            // Switching to the move that beats the last move
            lastMove = potentialMoves[moveIndex];
            return potentialMoves[moveIndex];
        }
    }


    public void UpdateLastMove(Move move)
    {
        lastMove = move;
    }
}

