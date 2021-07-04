using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Hangman
{
    public class PlayGame
    {
        private string capital { get; }
        private string country { get; }
        private string playerName { get; }
        private int playerLife { get; set; }
        private DateTime startTime { get; }
        private int gameTime { get; set; }
        private List<char> guessed { get; set; }
        private List<char> wrongLetters { get; set; }
        private string lastGuess { get; set; }
        private bool win { get; set; }
        private bool done { get; set; }
        private List<string> gallows { get; set; }
        private int guessCount { get; set; }
        public PlayGame(string name)
        {
            this.gallows = new List<string> {
                @"    ╔═╤═══╤ ",
                @"    ║/    │ ",
                @"    ║       ",
                @"    ║       ",
                @"    ║       ",
                @"   /║\      ",
            };
            var pair = GetRandomPair().Split(" | ");
            this.country = pair[0];
            this.capital = pair[1];
            this.playerName = name;
            this.playerLife = 6;
            this.startTime = DateTime.Now;
            this.guessed = Enumerable.Repeat('_', this.capital.Length).ToList();
            this.wrongLetters = new List<char>();
            this.lastGuess = "";
            this.win = false;
            this.done = false;
            this.guessCount = 0;
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
                    GuessLetter(Program.GetInputKey());
                }
                else if (key == '2')
                {
                    GuessWord(Console.ReadLine());
                }
            } while (this.playerLife > 0 && !this.win);
            if (this.win)
            {
                this.gameTime = ((int)(DateTime.Now - this.startTime).TotalSeconds);
                Leaderboard.UpdateLeaderboard(this.playerName, this.startTime, this.gameTime, this.capital);
            }
            this.done = true;
            DisplayGameScreen();
        }
        
        
        private void GuessLetter(char letter)
        {
            this.guessCount += 1;
            var capital = this.capital.ToUpper();
            letter = Char.ToUpper(letter);
            if (capital.Contains(letter))
            {
                this.lastGuess = $"{"Good guess!",45}";
                for (int i = 0; i < capital.Length; i++)
                {
                    if (capital[i] == letter)
                    {
                        this.guessed[i] = letter;
                    }
                }
                if (!this.guessed.Contains('_'))
                {
                    this.win = true;
                }
            }
            else
            {
                this.lastGuess = $"{"Wrong guess! You have loost one life!",58}";
                if (!this.wrongLetters.Contains(letter))
                {
                    this.wrongLetters.Add(letter);
                }
                this.playerLife -= 1;
                DrawStickman();
            }
        }
        
        private void GuessWord(string word)
        {
            this.guessCount += 1;
            var capital = this.capital.ToUpper();
            word = word.ToUpper();
            if (capital == word)
            {
                for (int i = 0; i < capital.Length; i++)
                {
                    this.guessed[i] = capital[i];
                }
                this.win = true;
            }
            else
            {
                this.lastGuess = $"{"Wrong guess! You have loost two lifes!",58}";
                this.playerLife -= 2;
                DrawStickman();
            }
        }
        
        private void DrawStickman()
        {
            if (this.playerLife < 6)
                this.gallows[2] = @"    ║     Ö ";
            if (this.playerLife < 5)
                this.gallows[3] = @"    ║    /  ";
            if (this.playerLife < 4)
                this.gallows[3] = @"    ║    /| ";
            if (this.playerLife < 3)
                this.gallows[3] = @"    ║    /|\";
            if (this.playerLife < 2)
                this.gallows[4] = @"    ║    /  ";
            if (this.playerLife < 1)
                this.gallows[4] = @"    ║    / \";
        }
        
        private void DisplayGameScreen()
        {
            string hint = ""; 
            Console.Clear();
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
            if (!this.done)
            {
                Console.WriteLine("\nPress:");
                Console.WriteLine("1 - If you want to guess a letter (loose 1 life if you guess wrong)");
                Console.WriteLine("2 - If you want to guess whole word (loose 2 life if you guess wrong)");
            }
            else
            {
                DisplaySummary();
                Leaderboard.ShowLeaderboard();
            }
        }
        
        private void DisplaySummary()
        {
            if (this.win)
            {
                Console.WriteLine($"\n{"Congratulation! You won!", 52}\n");
                Console.WriteLine($"You guesses the capital after {this.guessCount} guesses. It took you {this.gameTime} seconds.");
            }
            else
            {
                Console.WriteLine($"\n{"You lost all lifes!",49}");
                Console.WriteLine($"{"Better luck next time!",50}\n");
            }
            Console.WriteLine($"The city {this.capital} is capital of {this.country}\n");
            Leaderboard.ShowLeaderboard();
            Console.Write("Press any key to return to main menu...");
            Console.ReadKey(false);
        }
        
        private string GetRandomPair()
        {
            var pairs = Program.ReadFrom("countries_and_capitals.txt");
            var rand = new Random();
            return pairs[rand.Next(0, pairs.Count)];
        }
        
    }
}