using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardsOfAetheria
{
    class UniversalMethods
    {
        static public int optionSelected;
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
        public void UpdateAtts ()
            {
                if (CharacterCreation.player.PlayerClass == Player.playerClass.Melee)
                {
                    CharacterCreation.player.StrengthAtt = CharacterCreation.player.PrimaryAtt;
                    CharacterCreation.player.WisdomAtt = CharacterCreation.player.SecondaryAtt;
                    CharacterCreation.player.DexterityAtt = CharacterCreation.player.TertiaryAtt;
                }
                if (CharacterCreation.player.PlayerClass == Player.playerClass.Magic)
                {
                    CharacterCreation.player.WisdomAtt = CharacterCreation.player.PrimaryAtt;
                    CharacterCreation.player.DexterityAtt = CharacterCreation.player.SecondaryAtt;
                    CharacterCreation.player.StrengthAtt = CharacterCreation.player.TertiaryAtt;
                }
                if (CharacterCreation.player.PlayerClass == Player.playerClass.Ranged)
                {
                    CharacterCreation.player.DexterityAtt = CharacterCreation.player.PrimaryAtt;
                    CharacterCreation.player.StrengthAtt = CharacterCreation.player.SecondaryAtt;
                    CharacterCreation.player.WisdomAtt = CharacterCreation.player.TertiaryAtt;
                }
            }
    }
}
