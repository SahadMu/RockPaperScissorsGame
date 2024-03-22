public class Player
{
    public string Name { get; set; }
    public int TotalWins { get; set; }
    public int TotalLosses { get; set; }
    public double WinPercentage => TotalWins == 0 ? 0 : Math.Round((double)TotalWins / (TotalWins + TotalLosses) * 100, 2);

    public Move ChooseMove()
    {
        while (true)
        {
            Console.Write("Enter your move (1 for Rock, 2 for Paper, 3 for Scissors): ");
            if (Enum.TryParse(Console.ReadLine(), out Move move) && Enum.IsDefined(typeof(Move), move))
            {
                return move;
            }
            else
            {
                Console.WriteLine("Invalid move. Please try again.");
            }
        }
    }
}
