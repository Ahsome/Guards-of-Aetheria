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
            ConsoleKey input;
            while (true)
            {
                input = Console.ReadKey().Key;
                if (input == ConsoleKey.Enter)
                {
                    break;
                }
                Console.SetCursorPosition(0, menuSelected + startLine);
                Console.Write(' ');
                if (input == ConsoleKey.UpArrow) { menuSelected--; }
                if (input == ConsoleKey.DownArrow) { menuSelected++; }
                if (menuSelected < 1) { menuSelected = possibleOptions + 1; }
                if (menuSelected > possibleOptions + 1) { menuSelected = 1; }
                Console.SetCursorPosition(0, menuSelected + startLine);
                Console.Write('>');
            }
            return menuSelected;
        }

        public int SelectOption(int fromLeft, int startLine, int endLine)
        {
            int possibleOptions = endLine - startLine + 1;
            int menuSelected = 1;
            ConsoleKey input;
            while (true)
            {
                input = Console.ReadKey().Key;
                if (input == ConsoleKey.Enter)
                {
                    break;
                }
                Console.SetCursorPosition(fromLeft, menuSelected + startLine);
                Console.Write(' ');
                if (input == ConsoleKey.UpArrow) { menuSelected--; }
                if (input == ConsoleKey.DownArrow) { menuSelected++; }
                if (menuSelected < 1) { menuSelected = possibleOptions + 1; }
                if (menuSelected > possibleOptions) { menuSelected = 1; }
                Console.SetCursorPosition(fromLeft, menuSelected + startLine);
                Console.Write('>');
            }
            return menuSelected;
        }
        public void UpdateAtts()
        {
            Player.Instance.BaseVitality = Player.Instance.StrengthAtt * 10;
            Player.Instance.CurrentVitality = Player.Instance.BaseVitality + 0;
            Player.Instance.BaseMana = Player.Instance.WisdomAtt * 10;
            Player.Instance.CurrentMana = Player.Instance.BaseMana + 0;
            Player.Instance.BaseEndurance = Player.Instance.DexterityAtt * 10;
            Player.Instance.CurrentEndurance = Player.Instance.BaseEndurance + 0;
            switch (Player.Instance.PlayerClass)
            {
                case Player.Class.Melee:
                    Player.Instance.StrengthAtt = Player.Instance.PrimaryAtt;
                    Player.Instance.WisdomAtt = Player.Instance.SecondaryAtt;
                    Player.Instance.DexterityAtt = Player.Instance.TertiaryAtt;
                    break;
                case Player.Class.Magic:
                    Player.Instance.WisdomAtt = Player.Instance.PrimaryAtt;
                    Player.Instance.DexterityAtt = Player.Instance.SecondaryAtt;
                    Player.Instance.StrengthAtt = Player.Instance.TertiaryAtt;
                    break;
                case Player.Class.Ranged:
                    Player.Instance.StrengthAtt = Player.Instance.PrimaryAtt;
                    Player.Instance.WisdomAtt = Player.Instance.SecondaryAtt;
                    Player.Instance.DexterityAtt = Player.Instance.TertiaryAtt;
                    break;
            }
        }
        public void AssignAtts()
        {
            switch (Player.Instance.PlayerClass)
            {
                case Player.Class.Melee:
                    Player.Instance.PrimaryAtt = Player.Instance.StrengthAtt;
                    Player.Instance.SecondaryAtt = Player.Instance.WisdomAtt;
                    Player.Instance.TertiaryAtt = Player.Instance.DexterityAtt;
                    break;
                case Player.Class.Magic:
                    Player.Instance.PrimaryAtt = Player.Instance.WisdomAtt;
                    Player.Instance.SecondaryAtt = Player.Instance.DexterityAtt;
                    Player.Instance.TertiaryAtt = Player.Instance.StrengthAtt;
                    break;
                case Player.Class.Ranged:
                    Player.Instance.PrimaryAtt = Player.Instance.StrengthAtt;
                    Player.Instance.SecondaryAtt = Player.Instance.WisdomAtt;
                    Player.Instance.TertiaryAtt = Player.Instance.DexterityAtt;
                    break;
            }
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
