using System;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Security.AccessControl;
using System.Threading;

// Поменять названия файлов, передавать их в функцию через переменные
// Добавить вступление с объяснением причины, почему хп не полные
// Сделать боевую логику, в том числе отображение хп противника
// Сделать инвентарь
// Прописать первую битву
// Поискать картинки для консоли

namespace TigerQuest
{
    internal class Program
    {
        private static void DrawBar(int value, int maxValue, ConsoleColor color = ConsoleColor.Green)
        {
            ConsoleColor defaultColor = Console.BackgroundColor;

            string bar = "";

            for (int i = 0; i < value; i++)
            {
                bar += " ";
            }

            Console.SetCursorPosition(1, 1);
            Console.Write('[');
            Console.BackgroundColor = color;
            Console.Write(bar);
            Console.BackgroundColor = defaultColor;

            bar = "";
            
            for (int i = value; i < maxValue; i++)
            {
                bar += " ";
            }

            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write(bar);
            Console.BackgroundColor = defaultColor;
            Console.Write(']');
            Console.WriteLine(" - Здоровье");

        }
        private static void DeleteObjectFromLocation(ref string[] location, int indexToDelete)
        {
            string[] tempLocation = new string[location.Length - 1];
            for (int i = 0; i < location.Length; i++)
            {
                if (i != indexToDelete)
                {
                    tempLocation[i] = location[i];
                }
            }
            location = tempLocation;
        }
        private static void PrintEnvironment(string[] location)
        {
            int cursorVertical = 5;
            foreach (string furniture in location)
            {
                Console.SetCursorPosition(40, cursorVertical);
                Console.Write(furniture);
                cursorVertical++;
            }
            Console.SetCursorPosition(40, cursorVertical++);
        }

        private static void InteractionWithEnvironment(int playerChoose, ref string[] location)
        {
            bool cycleCondition = true;
            while (cycleCondition)
            {

                if (playerChoose > location.Length)
                {
                    Console.WriteLine("Введено некорректное значение\nПопробуйте ещё раз");
                    Console.Write("Допустимые значения: ");
                    for (int i = 1; i <= location.Length; i++)
                    {
                        Console.Write(i + ' ');
                    }
                    Console.WriteLine();
                    playerChoose = Convert.ToInt32(Console.ReadLine());
                    continue;
                }
                Console.SetCursorPosition(40, 5);
                switch (playerChoose)
                {
                    case 1:
                        string[] fightingObject;
                        ReadLocation(out fightingObject, "PrisonLocation/PrisonObjects/DishWasher.txt");
                        PrintEnvironment(fightingObject);
                        Console.ReadKey();
                        DeleteObjectFromLocation(ref location, 1);
                        cycleCondition = false;
                        break;

                    case 2:
                        string[] escapingObject;
                        ReadLocation(out escapingObject, "PrisonLocation/PrisonObject/DishWasher");
                        PrintEnvironment(escapingObject);
                        Console.ReadKey();
                        DeleteObjectFromLocation(ref location, 1);
                        cycleCondition = false;
                        break;

                    case 3:
                        string[] usefulObject;
                        ReadLocation(out usefulObject, "PrisonLocation/PrisonObject/DishWasher");
                        PrintEnvironment(usefulObject);
                        Console.ReadKey();
                        DeleteObjectFromLocation(ref location, 1);
                        cycleCondition = false;
                        break;

                    case 4:
                        string[] mysteryObject;
                        ReadLocation(out mysteryObject, "PrisonLocation/PrisonObject/DishWasher");
                        PrintEnvironment(mysteryObject);
                        Console.ReadKey();
                        DeleteObjectFromLocation(ref location, 1);
                        cycleCondition = false;
                        break;
                    default:
                        Console.WriteLine("Введено некорректное значение\nПопробуйте ещё раз");
                        Console.Write("Допустимые значения");
                        for (int i = 1; i <= location.Length; i++)
                        {
                            Console.Write(i + ' ');
                        }
                        playerChoose = Convert.ToInt32(Console.ReadLine());
                        break;
                }
            }
        }

        private static void ReadLocation(out string[] location, string path)
        {
            string[] tempLocation = File.ReadAllLines(path);
            location = tempLocation;
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(180, 40);

            bool gameIsOpen = true ;

            while (gameIsOpen) {
                int playerHealth = 15;
                int playerMaxHealth = 20;
                string[] prisonLocation;
                int playerChoose;
                DrawBar(playerHealth, playerMaxHealth);
                ReadLocation(out prisonLocation, "PrisonLocation/PrisonObjects.txt");
                Console.SetCursorPosition(40, 4);
                Console.WriteLine("Вы видите: ");
                PrintEnvironment(prisonLocation);
                Console.WriteLine("С чем вы хотите взамиодействовать? (Введите соответвствующую цифру)");
                Console.SetCursorPosition(40, 25);
                playerChoose = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                DrawBar(playerHealth, playerMaxHealth);
                InteractionWithEnvironment(playerChoose, ref prisonLocation);
                Console.ReadKey();
            }
        }
    }
}
