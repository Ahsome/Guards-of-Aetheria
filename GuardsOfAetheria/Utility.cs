using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardsOfAetheria
{
    class Utility
    {
        public int optionSelected;
        public int SelectOption(int startLine, int endLine)
        {
            int numberOfOptions = endLine - startLine + 1;
            optionSelected = 1;
            ConsoleKey input;
            int enter = 0;
            while (enter == 0)
            {
                input = Console.ReadKey().Key;
                if (input == ConsoleKey.Enter) { enter = 1; }
                Console.SetCursorPosition(0, optionSelected + startLine);
                Console.Write(' ');
                if (input == ConsoleKey.UpArrow) { optionSelected--; }
                if (input == ConsoleKey.DownArrow) { optionSelected++; }
                if (optionSelected < 1) { optionSelected = numberOfOptions; }
                if (optionSelected > numberOfOptions) { optionSelected = 1; }
                Console.SetCursorPosition(0, optionSelected + startLine);
                Console.Write('>');
            }
            return optionSelected;
        }
    }
}
