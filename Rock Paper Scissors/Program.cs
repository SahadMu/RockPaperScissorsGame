using System;
using System.IO;

public enum Move
{
    None = 0,
    Rock = 1,
    Paper = 2,
    Scissors = 3
}

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Please enter your name: ");
        string playerName = Console.ReadLine();
        rockPaperScissorsGame game = new rockPaperScissorsGame(playerName);
        game.Start();
    }
}