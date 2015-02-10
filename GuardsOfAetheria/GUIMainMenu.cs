using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardsOfAetheria
{
    class GUIMainMenu
    {
        public void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Guards of Atheria");
            Console.WriteLine("A simple game, set in the land of Aesrin");
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("> New Game\n  Load Game\n  Options\n  Credits\n  Quit Game");

            int menuSelected = SetMainCursor();
            ActivateSelectedMenu(menuSelected);
            DisplayMainMenu();
        }

        private void ActivateSelectedMenu(int menuSelected)
        {
            switch (menuSelected)
            {
                case 1:
                    var characterCreation = new CharacterCreation();
                    characterCreation.CreateCharacter();
                    break;
                case 2:
                //LoadGame
                case 3:
                    DisplayOptions();
                    break;
                case 4:
                    DisplayCredits();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
            }
        }

        private int SetMainCursor()
        {
            int menuSelected = 1;
            while (true)
            {
                ConsoleKey input = Console.ReadKey().Key;
                Console.SetCursorPosition(0, menuSelected + 3);
                Console.Write(' ');

                switch (input)
                {
                    case ConsoleKey.UpArrow:
                        menuSelected--;
                        break;
                    case ConsoleKey.DownArrow:
                        menuSelected++;
                        break;
                    case ConsoleKey.Enter:
                        return menuSelected;
                    default:
                        DisplayMainMenu();
                        break;
                }

                if (menuSelected < 1)
                {
                    menuSelected = 5;
                }
                else if (menuSelected > 5)
                {
                    menuSelected = 1;
                }
                Console.SetCursorPosition(0, menuSelected + 3);
                Console.Write('>');
            }
        }

        private void DisplayOptions()
        {
            Console.Clear();
            //Options
            return;
        }
        private void DisplayCredits()
        {
            Console.Clear();
            //Credits
            return;
        }
    }
}
