using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardsOfAetheria
{
    class UniversalMethods
    {
        public int optionSelected;
        static public int SelectOption(int startLine, int endLine)
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
        public void UpdateAtts (CharacterCreation.player.PlayerClass)
            {
                if (player.PlayerClass == Player.playerClass.Melee)
                {
                    player.StrengthAtt = player.PrimaryAtt;
                    player.DexterityAtt = player.SecondaryAtt;
                    player.WisdomAtt = player.TertiaryAtt;
                }
                if (player.PlayerClass == Player.playerClass.Magic)
                {
                    player.StrengthAtt = player.PrimaryAtt;
                    player.DexterityAtt = player.SecondaryAtt;
                    player.WisdomAtt = player.TertiaryAtt;
                }
                if (player.PlayerClass == Player.playerClass.Ranged)
                {
                    player.StrengthAtt = player.PrimaryAtt;
                    player.DexterityAtt = player.SecondaryAtt;
                    player.WisdomAtt = player.TertiaryAtt;
                }
            }
    }
}
