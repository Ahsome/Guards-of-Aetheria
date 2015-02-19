using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardsOfAetheria
{
    class Utility
    {
        public int SelectOption(int startLine, int possibleOptions)
        {
            int menuSelected = 1;
            possibleOptions--;
            ConsoleKey input;
            while (true)
            {
                Console.SetCursorPosition(0, menuSelected + startLine);
                Console.Write('>');
                input = Console.ReadKey(true).Key;
                if (input == ConsoleKey.Enter)
                {
                    break;
                }
                Console.SetCursorPosition(0, menuSelected + startLine);
                Console.Write(' ');
                if (input == ConsoleKey.UpArrow) { menuSelected--; }
                else if (input == ConsoleKey.DownArrow) { menuSelected++; }
                if (menuSelected < 1) { menuSelected = possibleOptions + 1; }
                else if (menuSelected > possibleOptions + 1) { menuSelected = 1; }
                Console.SetCursorPosition(0, menuSelected + startLine);
                Console.Write('>');
            }
            return menuSelected;
        }

        public int SelectOption(int fromLeft, int startLine, int endLine)
        {
            int possibleOptions = endLine - startLine;
            int menuSelected = 1;
            ConsoleKey input;
            while (true)
            {
                Console.SetCursorPosition(0, menuSelected + startLine);
                Console.Write('>');
                input = Console.ReadKey().Key;
                if (input == ConsoleKey.Enter)
                {
                    break;
                }
                Console.SetCursorPosition(fromLeft, menuSelected + startLine);
                Console.Write(' ');
                if (input == ConsoleKey.UpArrow) { menuSelected--; }
                else if (input == ConsoleKey.DownArrow) { menuSelected++; }
                if (menuSelected < 1) { menuSelected = possibleOptions + 1; }
                else if (menuSelected > possibleOptions) { menuSelected = 1; }
                Console.SetCursorPosition(fromLeft, menuSelected + startLine);
                Console.Write('>');
            }
            return menuSelected;
        }

        public void UpdateExp()
        {
            int ExpNeeded = Convert.ToInt16(Math.Pow(1.05, Player.Instance.Level) * 1000);
            if (Player.Instance.Experience > ExpNeeded)
            {
                Player.Instance.Level++;
                Player.Instance.Experience = Player.Instance.Experience - ExpNeeded;
            }
        }
    }
}
