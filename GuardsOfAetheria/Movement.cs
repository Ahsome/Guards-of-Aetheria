using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardsOfAetheria
{
    class Movement
    {
        public void ShowLocation()
        {
            Console.Clear();
            switch (Player.Instance.Location)
            {
                case "TutorialArea":
                    Console.WriteLine("Welcome to the game, {0}. Test the movement system. Good luck with that", Player.Instance.PlayerName);
                    Console.WriteLine("Your given options are located below");
                    Console.WriteLine("Use your arrow keys to move, press ENTER to select");
                    break;
                case "Corridor":
                    Console.WriteLine("Welcome to the corridor, {0}", Player.Instance.PlayerName);
                    break;
            }
            LocationOption();
        }

        public int LocationOption()
        {
            switch (Player.Instance.Location)
            {
                case "TutorialArea":
                    Console.SetCursorPosition(0, 6);
                    Console.WriteLine("> Corridor\n  Random House (NOT WORKING)\n  A subway (NOT WORKING)\n  Heaven (NOT DEAD ENOUGH)");
                    int menuSelected = 0;
                    while (true)
                    {
                        ConsoleKey input = Console.ReadKey().Key;
                        Console.SetCursorPosition(0, menuSelected + 6);
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
                                SetLocation("Corridor");
                                return menuSelected;
                        }

                        if (menuSelected < 0)
                        {
                            menuSelected = 3;
                        }
                        else if (menuSelected > 3)
                        {
                            menuSelected = 0;
                        }
                        Console.SetCursorPosition(0, menuSelected + 6);
                        Console.Write('>');
                    }
                    break;
                default:
                    Console.WriteLine("Sorry I haven't bothered to give any options, {0}", Player.Instance.PlayerName);
                    Console.ReadKey();
                    break;
            }
            return 0;
        }

        private void SetLocation(string newArea)
        {
            Player.Instance.Location = newArea;
        }
    }
}
