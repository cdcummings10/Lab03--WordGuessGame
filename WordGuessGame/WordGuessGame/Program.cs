﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WordGuessGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            UserInterface();
        }
        /// <summary>
        /// Displays main menu for game. Runs functions based on user input in a switch statement.
        /// </summary>
        public static void UserInterface()
        {
            string path = "../../../../docs/words.txt";
            bool gameOn = true;
            Console.WriteLine("Welcome to the Word Guess Game!");
            while (gameOn)
            {
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1) Start Game");
                Console.WriteLine("2) Admin");
                Console.WriteLine("3) Exit");
                try
                {
                    switch (Console.ReadLine())
                    {
                        case "1":
                            StartGame(path);
                            break;
                        case "2":
                            AdminMenu(path);
                            break;
                        case "3":
                            gameOn = false;
                            ExitGame();
                            break;
                        default:
                            Console.WriteLine("Please input valid menu number.");
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Something went wrong! Please try again!");
                }
            }


        }
        /// <summary>
        /// Displayed admin menu. Runs functions based on user input in a switch statement.
        /// </summary>
        /// <param name="path"></param>
        public static void AdminMenu(string path)
        {
            bool adminOn = true;

            while (adminOn)
            {
                Console.WriteLine("------ADMIN PANEL-----");
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1) View Current Words");
                Console.WriteLine("2) Add a Word");
                Console.WriteLine("3) Remove a Word");
                Console.WriteLine("4) Start Game");
                Console.WriteLine("5) Exit");

                try
                {
                    switch (Console.ReadLine())
                    {
                        case "1":
                            Console.WriteLine("Current Words:");
                            Console.WriteLine(ViewWords(path));
                            break;
                        case "2":
                            Console.Write("Please enter a word you want to add: ");
                            string addWord = Console.ReadLine();
                            AddWord(path, addWord);
                            break;
                        case "3":
                            Console.Write("Please enter a word you want to remove: ");
                            string removeWord = Console.ReadLine();
                            RemoveWords(path, removeWord.ToLower());
                            break;
                        case "4":
                            StartGame(path);
                            break;
                        case "5":
                            ExitGame();
                            break;
                        default:
                            Console.WriteLine("Please input valid menu number.");
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Something went wrong! Please try again!");
                }
            }
        }
        /// <summary>
        /// Takes in a file path and returns the words in the file as a string.
        /// </summary>
        /// <param name="path">Takes in a file path as a string.</param>
        /// <returns>Returns word file as a string.</returns>
        public static string ViewWords(string path)
        {
            string wordsFile = File.ReadAllText(path);
            return wordsFile;
        }
        /// <summary>
        /// Takes in a file path and a word to remove. Reads lines from the file path.
        /// Iterates through array and returns a new array with the word removed. Rewrites .txt file.
        /// </summary>
        /// <param name="path">Takes in a file path as a string.</param>
        /// <param name="word">Takes in a word to remove as a string.</param>
        /// <returns>Returns new array of words with parameter word removed.</returns>
        public static string[] RemoveWords(string path, string word)
        {
            string[] words = File.ReadAllLines(path);
            if (words.Contains(word))
            {
                string[] newWords = new string[words.Length - 1];
                bool removed = false;
                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i] == word)
                    {
                        removed = true;
                    }
                    else if (removed == true)
                    {
                        newWords[i - 1] = words[i];
                    }
                    else
                    {
                        newWords[i] = words[i];
                    }
                }
                File.WriteAllLines(path, newWords);
                Console.WriteLine($"{word} has been removed from the list.");
                return newWords;
            }
            else
            {
                Console.WriteLine("Word not found on stored list.");
                return words;
            }
        }
        /// <summary>
        /// Takes in a file path and a word to add. Adds word to the end of file.
        /// Returns array of the updated words list.
        /// </summary>
        /// <param name="path">Takes in a file path as a string.</param>
        /// <param name="word">Takes in a word to add to file as a string.</param>
        /// <returns>Returns an array of the updated word list.</returns>
        public static string[] AddWord(string path, string word)
        {
            string[] lines = new string[] { word.ToLower() };
            File.AppendAllLines(path, lines);
            Console.WriteLine($"{word} has been added to the list.");
            return File.ReadAllLines(path);
        }
        /// <summary>
        /// Exits the game via Environment.Exit(0).
        /// </summary>
        public static void ExitGame()
        {
            Environment.Exit(0);
        }
        /// <summary>
        /// Runs the main game. Grabs a random word from the stored words file and renders letters as 
        /// underscores with spaces in between. Runs function to check if a guess is in a char array. 
        /// Ends game when all letters are guessed.
        /// </summary>
        /// <param name="path">Takes in a file path as a string.</param>
        public static void StartGame(string path)
        {
            string[] wordsList = File.ReadAllLines(path);
            Random rand = new Random();
            string answerWord = wordsList[rand.Next(wordsList.Length)];
            char[] answerWordArray = answerWord.ToCharArray();
            char[] currentDisplay = new char[answerWordArray.Length];
            bool victory = false;
            for (int i = 0; i < currentDisplay.Length; i++)
            {
                currentDisplay[i] = '_';
            }
            List<char> guessedLetters = new List<char>();
            while (victory == false)
            {
                Console.WriteLine(String.Join(" ", currentDisplay));

                try
                {
                    char guess = Console.ReadLine()[0];
                    if (guessedLetters.Contains(guess))
                    {
                        Console.WriteLine("Please guess a new letter!");
                    }
                    else
                    {
                        guessedLetters.Add(guess);
                        CheckWordForChar(guess, currentDisplay, answerWordArray);
                        Console.Write("Letters guessed so far: ");
                        foreach (char letter in guessedLetters)
                        {
                            Console.Write(letter + ", ");
                        }
                        Console.WriteLine();
                    }

                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Please enter a valid guess.");
                }
                catch (Exception)
                {
                    Console.WriteLine("Something went wrong! Please try again!");
                }

                if (!currentDisplay.Contains('_'))
                {
                    victory = true;
                    Console.WriteLine("The answer: " + String.Join(" ", currentDisplay));
                    Console.WriteLine("Congratulations! You won!\n\n");
                }
            }
        }
        /// <summary>
        /// Takes in a guess, current display word as '_' and answer word. Checks if guess is 
        /// a valid character in a word. Returns the current display with the char revealed 
        /// if the char is inside the answer array.
        /// </summary>
        /// <param name="guess">Takes in a guess as a char.</param>
        /// <param name="currentWord">Takes in the current display as a char array.</param>
        /// <param name="answerWord">Takes in the answer word as a char Array.</param>
        /// <returns>Returns updated current display as a char array.</returns>
        public static char[] CheckWordForChar(char guess, char[] currentWord, char[] answerWord)
        {
            for (int i = 0; i < answerWord.Length; i++)
            {
                if (Char.ToUpper(guess) == Char.ToUpper(answerWord[i]))
                {
                    currentWord[i] = Char.ToUpper(answerWord[i]);
                }
            }
            return currentWord;
        }
    }
}
