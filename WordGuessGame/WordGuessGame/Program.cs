using System;
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

        public static void UserInterface()
        {
            string path = "../../../../docs/words.txt";
            bool gameOn = true;
            while (gameOn)
            {
                Console.WriteLine("Welcome to the Word Guess Game!");
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1) Start Game");
                Console.WriteLine("2) Admin");
                Console.WriteLine("3) Exit");
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


        }

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
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine(ViewWords(path));
                        break;
                    case "2":
                        string addWord = Console.ReadLine();
                        AddWord(path, addWord);
                        break;
                    case "3":
                        string removeWord = Console.ReadLine();
                        RemoveWords(path, removeWord);
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
        }

        public static string ViewWords(string path)
        {
            string wordsFile = File.ReadAllText(path);
            return wordsFile;
        }

        public static void RemoveWords(string path, string word)
        {
            string[] words = File.ReadAllLines(path);
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
        }

        public static void AddWord(string path, string word)
        {
            string[] lines = new string[] { word };
            File.AppendAllLines(path, lines);
        }

        public static void ExitGame()
        {
            Environment.Exit(0);
        }

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
            while (victory == false)
            {
            Console.WriteLine(String.Join(" ",currentDisplay));

            char guess = Console.ReadLine()[0];

            for (int i = 0; i < answerWordArray.Length; i++)
            {
                if (Char.ToUpper(guess) == Char.ToUpper(answerWordArray[i]))
                {
                    currentDisplay[i] = Char.ToUpper(answerWordArray[i]);
                }
            }
            }
            //UserInterface();
        }
    }
}
