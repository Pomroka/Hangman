using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hangman
{
    public class PlayGame
    {
        private string capital { get; }
        private string country { get; }
        private string playerName { get; }
        private int playerLife { get; set; }
        private DateTime startTime { get; }
        private List<char> guessed { get; set; }
        private List<char> wrongLetters { get; set; }
        private string lastGuess { get; set; }
        private bool win { get; set; }
        private static string[] gallows = {
            @"    ╔═╤═══╤ ",
            @"    ║/    │ ",
            @"    ║       ",
            @"    ║       ",
            @"    ║       ",
            @"   /║\      ",
        };
        
        public PlayGame(string name)
        {
            var pair = GetRandomPair().Split(" | ");
            this.country = pair[0];
            this.capital = pair[1];
            this.playerName = name;
            this.playerLife = 6;
            this.guessed = Enumerable.Repeat('_', this.capital.Length).ToList();
            this.wrongLetters = new List<char>();
            this.lastGuess = "";
            this.win = false;
            NewGame();
        }
        
        private void NewGame()
        {
            do {
                DisplayGameScreen();
                var key = Program.GetInputKey();
                Console.Write($"Enter your guess {this.playerName}: ");
                if (key == '1')
                {
                    this.lastGuess = GuessLetter(Program.GetInputKey());
                }
                else if (key == '2')
                {
                    //this.lastGuess = GuessWord(Console.ReadLine());
                }
            } while (this.playerLife > 0 || !this.win);
        }
        
        private string GuessLetter(char letter)
        {
            string lastGuess = "";
            var capital = this.capital.ToUpper();
            letter = Char.ToUpper(letter);
            if (capital.Contains(letter))
            {
                lastGuess = $"{"Good guess!",45}";
                for (int i = 0; i < capital.Length; i++)
                {
                    if (capital[i] == letter)
                        this.guessed[i] = letter;
                }
                if (!this.guessed.Contains('_'))
                    this.win = true;
            }
            else
            {
                lastGuess = $"{"Wrong guess! You loost one life!",56}";
                this.wrongLetters.Add(letter);
                this.playerLife -= 1;
            }
            return lastGuess;
        }
        
        private void DisplayGameScreen()
        {
            string hint = ""; 
            Console.Clear();
            Console.WriteLine("---------|---------|---------|---------|---------|---------|---------|---------|");
            Console.WriteLine($"{"Guess a capital as fast as you can!", 57}\n");
            Console.WriteLine($"{gallows[0]}");
            Console.WriteLine($"{gallows[1]}    Capital to guess: {string.Join(" ", this.guessed)}");
            Console.WriteLine($"{gallows[2]}");
            Console.WriteLine($"{gallows[3]}{"Letters not in word: ",25}{string.Join(" ", this.wrongLetters)}");
            Console.WriteLine($"{gallows[4]}");
            if (this.playerLife == 1)
                hint = $"{"Hint: Capital of"} {this.country}";
            Console.WriteLine($"{gallows[5]}    Life left: {this.playerLife}    {hint}");
            if (this.lastGuess.Length > 0)
                Console.WriteLine(this.lastGuess);
            
            Console.WriteLine("\nPress:");
            Console.WriteLine("1 - If you want to guess a letter (loose 1 life if you guess wrong)");
            Console.WriteLine("2 - If you want to guess whole word (loose 2 life if you guess wrong)");
            
        }
        
        private string GetRandomPair()
        {
            var pairs = Program.ReadFrom("countries_and_capitals.txt");
            var rand = new Random();
            return pairs[rand.Next(0, pairs.Count)];
        }
        
    }
}