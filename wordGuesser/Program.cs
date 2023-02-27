using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace wordGuesser
{
    public static class Program
    {
        public static string generateWord()
        {
            Random rnd = new Random();
            int randomNum = rnd.Next(1,8900);
            int i = 0;
            bool found = false;
            //Console.WriteLine(randomNum); #Outputs the random number generated
            String fromFile = @"./words.txt";
            while (found != true)
            {
                foreach (string line in File.ReadAllLines(fromFile)) //Reads all lines in file 1 by 1
                {
                    if (i == randomNum) //if the line number is equal to the random number run
                    {
                        //Console.WriteLine("FOUND");
                        found = true;
                        //Console.WriteLine($"The word was {line} found on line {i+1}");

                        string guessWord = line;
                        return guessWord;
                        break;
                        
                    }
                    else //if the line number is not equal to the random number run
                    {
                        //Console.WriteLine(line);
                        i = i + 1;
                        //Console.WriteLine(i);
                    }

                }
            }

            return null;
        }
        static void Main(string[] args)
        {
            Menu();
        }

        public static void Menu()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"
   _____                      _______ _           __          __           _ 
  / ____|                    |__   __| |          \ \        / /          | |
 | |  __ _   _  ___ ___ ___     | |  | |__   ___   \ \  /\  / ___  _ __ __| |
 | | |_ | | | |/ _ / __/ __|    | |  | '_ \ / _ \   \ \/  \/ / _ \| '__/ _` |
 | |__| | |_| |  __\__ \__ \    | |  | | | |  __/    \  /\  | (_) | | | (_| |
  \_____|\__,_|\___|___|___/    |_|  |_| |_|\___|     \/  \/ \___/|_|  \__,_|
                                
                                Author: Jxde                                                                         
                                                                             
");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("                             Press enter to play!");
            Console.ReadKey();
            Console.Clear();
            Game();
        }
        public static void Game()
        {
            List<char> UserGuesses = new List<char>();
            int attempts = 0;
            List<char> guessWordList = new List<char>();
            List<char> hiddenGuessWordList = new List<char>();
            string guessWord = generateWord();

            foreach (char c in guessWord)
            {
                guessWordList.Add(c);
                hiddenGuessWordList.Add(c);
            }

            //Console.WriteLine($"Returned word: {guessWord}");

            int maxWordLength = guessWord.Length;
            //Console.WriteLine(maxWordLength);
            Random rnd = new Random();
            int randomNum = rnd.Next(0,maxWordLength);
            int i = 1;
            
            //ADD _ TO EACH INDEX
            while (i < hiddenGuessWordList.Count)
            {
                hiddenGuessWordList[i] = '_';
                i = i + 1;
            }


            bool guessed = false;
            string guess = "";
        
            while (guessed != true)
            {
                bool validInput = false;
                
                while (validInput != true)
                {
                    var hiddenGuessWord = String.Concat(hiddenGuessWordList);
                    var strUserGuesses = String.Concat(UserGuesses);
                    if (UserGuesses.Count != 0)
                    {
                        Console.WriteLine($"Guessed letters: {strUserGuesses}");
                    }
                    Console.WriteLine(hiddenGuessWord);
    
                    Console.WriteLine("\nGuess a letter: ");
                    guess = Console.ReadLine();
                    attempts = attempts + 1;
                    
                    if (guess.All(char.IsLetter) == true && guess.Length == 1)
                    {
                        Console.Clear();
                        Console.WriteLine($"You guessed the letter {guess}");
                        validInput = true;
                    }
                }
                List<int> Indexes = new List<int>();
                if (guessWord.Contains(guess))
                {
                    Console.Clear();
                    Console.WriteLine($"The word contains the letter {guess}");
                    UserGuesses.Add(char.Parse(guess));
                    UserGuesses.Add(',');
                    int letterIndex = guessWord.IndexOf(guess);
                    i = 0;
                    foreach (char c in guessWord)
                    {
                        i = i + 1;
                        if (c == char.Parse(guess))
                        {
                            //Console.WriteLine($"Index: {i-1}");
                            Indexes.Add(i-1);
                        }
                    }

                    foreach (int index in Indexes)
                    {
                        hiddenGuessWordList[index] = char.Parse(guess);
                    }
                    var hiddenGuessWord = String.Concat(hiddenGuessWordList);
                    if (hiddenGuessWord == guessWord)
                    {
                        Console.Clear();
                        Console.WriteLine($"You guessed correctly, the word was {guessWord}");
                        Console.WriteLine($"It took you {attempts} guesses");
                        guessed = true;
                        Console.WriteLine($"\nPress Enter to return to the main menu...");
                        Console.ReadKey();
                        Console.Clear();
                        Menu();
                    }
                    


                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"The word does not contain the letter {guess}");
                    UserGuesses.Add(char.Parse(guess));
                    UserGuesses.Add(',');
                }
            }
        }
    }
}