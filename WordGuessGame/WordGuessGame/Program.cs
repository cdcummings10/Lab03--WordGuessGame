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
                        StartGame();
                        break;
                    case "2":
                        AdminMenu();
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

        public static void AdminMenu()
        {
            bool adminOn = true;
            string path = "../../../../docs/words.txt";
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
                        StartGame();
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

        }

        public static void AddWord(string path, string word)
        {

        }

        public static void ExitGame()
        {
            Environment.Exit(0);
        }

        public static void StartGame()
        {
            UserInterface();
        }
    }
}
