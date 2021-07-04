using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Math;

namespace Hangman
{
    public class Leaderboard
    {
        public const string highscoreFile = "highscore.txt";
        private const string topRow = "╔══════╦═══════════════════╦══════════════════╦══════╦════════════════════════╗";
        private const string middleRow = "╠══════╬═══════════════════╬══════════════════╬══════╬════════════════════════╣";
        private const string bottomRow = "╚══════╩═══════════════════╩══════════════════╩══════╩════════════════════════╝";
        private const string sep = "║";
        private static string labelRow = $"{sep}{"No.",5}{sep,2}{"Player name",15}{sep,5}{"Date",11}{sep,8}{"Time",5}{sep,2}{"Capital",15}{sep,10}";
        
        public static List<List<string>> leaderboard { get; set; }
        public Leaderboard()
        {
            ReadHSFromFile();
        }

        private void ReadHSFromFile()
        {
            
            var lines = Program.ReadFrom(highscoreFile);
            leaderboard = new List<List<string>>();
            foreach (var line in lines)
            {
                leaderboard.Add(line.Split(" | ").ToList());
            }
        }
        
        private static void SaveHSToFile()
        {
            using (var writer = File.CreateText(highscoreFile))
            {
                foreach (var line in leaderboard)
                {
                    writer.WriteLine(String.Join(" | ", line));
                }
            }
        }
        
        public static void UpdateLeaderboard(string name, DateTime startTime, int gameTime, string capital)
        {
            leaderboard.Add(new List<string>() { name, $"{startTime:g}", $"{gameTime}", capital });
            leaderboard.Sort( (a, b) => a[2].CompareTo(b[2]) );
            SaveHSToFile();
        }
        public static void ShowLeaderboard(int size = 10)
        {
            
            Console.WriteLine(topRow);
            Console.WriteLine(labelRow);
            Console.WriteLine(middleRow);
            int i = 1;
            foreach (var hsRow in leaderboard.GetRange(0, Min(size, leaderboard.Count)))
            {
                var name = hsRow[0].Length > 17 ? hsRow[0].Substring(0, 15) + ".." : hsRow[0];
                string row = $"{sep}{i,5} {sep} {name,-17} {sep} {hsRow[1],-15} {sep} {hsRow[2],4} {sep} {hsRow[3],-22} {sep}";
                Console.WriteLine(row);
                i++;
            }
            Console.WriteLine(bottomRow);
        }
    }
}
