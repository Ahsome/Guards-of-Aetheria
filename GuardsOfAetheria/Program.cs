using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardsOfAetheria
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.DisplayMainMenu();
        }

        public void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Guards of Atheria\nA simple game, set in the land of Aesrin\nWhat would you like to do?\n\n> New Game\n  Load Game\n  Options Game\n  Credits Game\n  Quit Game");
            int menu = 1;
            ConsoleKey input;
            int enter = 0;
            while (enter == 0)
            {
                input = Console.ReadKey().Key;
                if (input == ConsoleKey.Enter) { enter = 1; } else { enter = 0; }
                Console.SetCursorPosition(0, menu + 3);
                Console.Write(' ');
                if (input == ConsoleKey.UpArrow) { menu--; }
                if (input == ConsoleKey.DownArrow) { menu++; }
                if (menu < 1) { menu = 5; }
                if (menu > 5) { menu = 1; }
                Console.SetCursorPosition(0, menu + 3);
                Console.Write('>');
            }

            switch (menu)
            {
                case 1:
                    return;
                case 2:
                //LoadGame
                case 3:
                //DisplayOptionsMenu()2
                case 4:
                //DisplayCreditsMeny()
                case 5:
                    Environment.Exit(0);
                    break;
            }
            DisplayMainMenu();
        }
    }
}
