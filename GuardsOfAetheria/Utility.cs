using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardsOfAetheria
{
    class Utility
    {
        public int menuSelected = 1;
        int optionSelected = 0;
        public int SelectOption(int startLine, int possibleOptions)
        {
            ConsoleKey input;
            while (true)
            {
                input = Console.ReadKey().Key;
                if (input == ConsoleKey.Enter) 
                {
                    optionSelected = menuSelected;
                    break; 
                }
                Console.SetCursorPosition(0, menuSelected + startLine);
                Console.Write(' ');
                if (input == ConsoleKey.UpArrow) { menuSelected--; }
                if (input == ConsoleKey.DownArrow) { menuSelected++; }
                if (menuSelected < 1) { menuSelected = possibleOptions+1; }
                if (menuSelected > possibleOptions+1) { menuSelected = 1; }
                Console.SetCursorPosition(0, menuSelected + startLine);
                Console.Write('>');
            }
            menuSelected = 1;
            return optionSelected;
        }
    }
}
