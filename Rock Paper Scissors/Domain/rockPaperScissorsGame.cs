using System.Text.Json;

public class rockPaperScissorsGame
{
    private Player player;
    private Computer computer;
    private int roundsToWin;
    private int playerScore;
    private int computerScore;


    public rockPaperScissorsGame(string playerName)
    {
        player = new Player() { Name = playerName };
        computer = new Computer();
        playerScore = 0;
        computerScore = 0;
    }

    public void Start()
    {
        Console.WriteLine("Welcome to Rock, Paper, Scissors!");

        while (true)
        {
            Console.WriteLine("\nMain Menu:");
            Console.WriteLine("1. Start playing");
            Console.WriteLine("2. Rules");
            Console.WriteLine("3. Quit");

            Console.Write("Please select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    PlayGame();
                    break;
                case "2":
                    DisplayRules();
                    break;
                case "3":
                    Console.WriteLine("Thank you for playing. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }

    private void PlayGame()
    {
        LoadPlayerStats();

        Console.WriteLine($"Welcome back, {player.Name}!");
        Console.WriteLine($"Your current stats: Wins - {player.TotalWins}, Losses - {player.TotalLosses}, Win Percentage - {player.WinPercentage}%");

        while (true)
        {
            Console.Write("Enter the number of rounds to win (best of): ");
            roundsToWin = int.Parse(Console.ReadLine());

            // Reset scores for a new game
            playerScore = 0;
            computerScore = 0;

            while (playerScore < roundsToWin && computerScore < roundsToWin)
            {
                Console.WriteLine($"Round {playerScore + computerScore + 1}");

                Move playerMove = player.ChooseMove();
                Move computerMove = computer.ChooseMove();

                Console.WriteLine($"{player.Name} chose {playerMove}");
                Console.WriteLine($"{Computer.Name} chose {computerMove}");

                DetermineWinner(playerMove, computerMove);

                Console.WriteLine($"Score: {player.Name} - {playerScore}, {Computer.Name} - {computerScore}");
                Console.WriteLine();
            }

            Console.WriteLine("Game Over!");


            if (playerScore > computerScore)
            {
                Console.WriteLine($"{player.Name} wins the game!");
                player.TotalWins++;
            }
            else if (playerScore < computerScore)
            {
                Console.WriteLine($"{Computer.Name} wins the game!");
                player.TotalLosses++;
            }
            else
            {
                Console.WriteLine("It's a tie!");
            }

            SavePlayerStats();

            // Go back to the main menu
            break;
        }
    }



    private void DetermineWinner(Move playerMove, Move computerMove)
    {
        if ((playerMove == Move.Rock && computerMove == Move.Scissors) ||
            (playerMove == Move.Paper && computerMove == Move.Rock) ||
            (playerMove == Move.Scissors && computerMove == Move.Paper))
        {
            Console.WriteLine($"{player.Name} wins this round!");
            playerScore++;
            computer.UpdateResult(false);
        }
        else if (playerMove == computerMove)
        {
            Console.WriteLine("It's a tie!");
            computer.UpdateResult(false);
        }
        else
        {
            Console.WriteLine($"{Computer.Name} wins this round!");
            computerScore++;
            computer.UpdateResult(true);
        }
        
    }

    private void LoadPlayerStats()
    {
        string filePath = $"{player.Name.ToLower()}.json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            player = JsonSerializer.Deserialize<Player>(json);
        }
    }

    private void SavePlayerStats()
    {
        string filePath = $"{player.Name.ToLower()}.json";
        string json = JsonSerializer.Serialize(player);
        File.WriteAllText(filePath, json);
    }

    private void DisplayRules()
    {
        Console.WriteLine("\nRules of Rock, Paper, Scissors:");
        Console.WriteLine("1. Rock beats Scissors");
        Console.WriteLine("2. Scissors beats Paper");
        Console.WriteLine("3. Paper beats Rock");
    }
}
