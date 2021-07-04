using System;
using System.Collections.Generic;
using System.IO;
using static System.Math;

namespace Hangman
{
    class Program
    {
        public const string highscoreFile = "highscore.txt";
        private static string[] gallows = {
            @" ╔═╤═══╤ ",
            @" ║/    │ ",
            @" ║     Ö ",
            @" ║    /│\",
            @" ║    / \",
            @"/║\      ",
        };
        
        static void Main(string[] args)
        {
            string playerName = "Player";
            bool done = false;
            new Leaderboard();
            
            do {
                DisplayWelcomeScreen(playerName);
                var key = GetInputKey();
                if (key == '1')
                    playerName = ChangeName();
                else if (key == '2')
                    new PlayGame(playerName);
                else if (key == '3')
                    ViewLeaderboard();
                else if (key == 'x' || key == 'X')
                    done = true;
                
            } while (!done);
        }
        
        static void ViewLeaderboard()
        {
            Console.Clear();
            Console.WriteLine($"{"Leaderboard",45}\n");
            Leaderboard.ShowLeaderboard(20);
            Console.WriteLine("\nPress any key to return to main menu...");
            Console.ReadKey(false);
        }
        
        static string ChangeName()
        {
            Console.Write("Enter new name: ");
            return Console.ReadLine();
        }
        
        
        public static char GetInputKey()
        {
            var key = Console.ReadKey(true);
            return key.KeyChar;
        }
        
        public static List<string> ReadFrom(string file)
        {
            string line;
            var lines = new List<string>();
            if (!File.Exists(file))
            {
                File.CreateText(file);
                return lines;
            }
            using (var reader = File.OpenText(file))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return lines;
        }
        
        static void DisplayWelcomeScreen(string player)
        {
            Console.Clear();
            Console.WriteLine($"{"Hangman",43}");
            Console.WriteLine($"{"Game written by Ania",49}\n");
            
            if (File.Exists(highscoreFile))
            {
                Console.WriteLine($"{"Top 3 games",45}");
                Leaderboard.ShowLeaderboard(3);
            }
            Console.WriteLine($"\nWelcome {player,-35}{gallows[0]}");
            Console.WriteLine($"Press:{gallows[1],46}");
            Console.WriteLine($"1 - To change name{gallows[2],34}");
            Console.WriteLine($"2 - To start New Game{gallows[3],31}");
            Console.WriteLine($"3 - To view Leaderboard{gallows[4],29}");
            Console.WriteLine($"X - To Exit{gallows[5],41}\n");
        }
        
    }
}
