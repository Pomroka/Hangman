using System;
using System.Collections.Generic;
using System.IO;
using static System.Math;

namespace Hangman
{
    class Program
    {
        public static List<string> wordsPairs = new List<string>();
        public const string highscoreFile = "highscore.txt";
        static string[] gallows = {
            @" ╔═╤═══╤ ",
            @" ║/    │ ",
            @" ║     Ö ",
            @" ║    /│\",
            @" ║    / \",
            @"/║\      ",
        };
        static bool done = false;
        
        static void Main(string[] args)
        {
            string wordDictFile = args.Length > 0 ? args[0] : "countries_and_capitals.txt";
            
            string playerName = "Player";
            wordsPairs = LoadWordsFromFile(wordDictFile);
            new Leaderboard();
            
            while (!done) 
            {
                DisplayWelcomeScreen(playerName);
                var key = GetInputKey();
                if (key == '1')
                    playerName = ChangeName();
                else if (key == '2')
                    new NewGame(playerName);
                else if (key == '3')
                    ViewLeaderboard();
                else if (key == 'x' || key == 'X')
                    done = true;
                
            }
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
            if (File.Exists(file))
            {
                using (var reader = File.OpenText(file))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
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
        
        static List<string> LoadWordsFromFile(string file)
        {
            var words = new List<string>();
            if (!File.Exists(file))
            {
                var exeName = System.AppDomain.CurrentDomain.FriendlyName;
                Console.WriteLine($"Dictionary file \"{file}\" not found.");
                Console.WriteLine($"Make sure the file is in same folder as {exeName}.exe");
                Console.WriteLine($"Or You can load own word dictionary file using: \"{exeName}.exe dictionary_file.txt\"");
                Console.WriteLine("Example structure of dictionary file:");
                Console.WriteLine("hint_word1 | guess_word1\nhint_word2 | guess_word2");
                done = true;
            }
            else
            {
                words = ReadFrom(file);
            }
            return words;
        }
    }
}
