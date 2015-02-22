using System;

namespace GuardsOfAetheria
{
    class Utility
    {
        public int SelectOption(int startLine, int possibleOptions)
        {
            int menuSelected = 1;
            possibleOptions--;
            while (true)
            {
                Console.SetCursorPosition(0, menuSelected + startLine);
                Console.Write('>');
                var input = Console.ReadKey(true).Key;
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
        
        public void UpdateExp()
        {
            int expNeeded = Convert.ToInt16(Math.Pow(1.05, Player.Instance.Level) * 1000);
            if (Player.Instance.Experience > expNeeded)
            {
                Player.Instance.Level++;
                Player.Instance.Experience = Player.Instance.Experience - expNeeded;
            }
        }
    }
}
