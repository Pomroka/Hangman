using System;
using System.Collections.Generic;
using System.IO;

namespace Hangman
{
    class Program
    {
        private const string topRow = "╔══════╦═══════════════════╦══════════════════╦══════╦════════════════════════╗";
        private const string middleRow = "╠══════╬═══════════════════╬══════════════════╬══════╬════════════════════════╣";
        private const string bottomRow = "╚══════╩═══════════════════╩══════════════════╩══════╩════════════════════════╝";
        private const string sep = "║";
        private static string labelRow = $"{sep}{"No.",5}{sep,2}{"Player name",15}{sep,5}{"Date",11}{sep,8}{"Time",5}{sep,2}{"Capital",15}{sep,10}";
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
            
            do {
                DisplayWelcomeScreen(playerName);
                var key = GetInputKey();
                if (key == '1')
                    playerName = ChangeName();
                else if (key == '2')
                    new PlayGame(playerName);
                else if (key == '3')
                    ShowLeaderboard();
                else if (key == 'x' || key == 'X')
                    done = true;
                
            } while (!done);
        }
        
        static string ChangeName()
        {
            Console.Write("Enter new name: ");
            return Console.ReadLine();
        }
        
        static void ShowLeaderboard()
        {
            
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
            // var top3 = ReadFrom("highscore.txt");
            Console.Clear();
            Console.WriteLine($"{"Hangman",43}");
            Console.WriteLine($"{"Game written by Ania",49}\n");
            
            Console.WriteLine($"{"Top 3 games",45}");
            Console.WriteLine(topRow);
            Console.WriteLine(labelRow);
            Console.WriteLine(middleRow);
            // for (int i = 0; i < top3.Count; i++)
            Console.WriteLine(bottomRow);
            
            Console.WriteLine($"\nWelcome {player,-35}{gallows[0]}");
            Console.WriteLine($"Press:{gallows[1],46}");
            Console.WriteLine($"1 - To change name{gallows[2],34}");
            Console.WriteLine($"2 - To start New Game{gallows[3],31}");
            Console.WriteLine($"3 - To view Leaderboard{gallows[4],29}");
            Console.WriteLine($"X - To Exit{gallows[5],41}\n");
        }
        
    }
}
