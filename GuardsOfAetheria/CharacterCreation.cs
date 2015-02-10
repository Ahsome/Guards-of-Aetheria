using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardsOfAetheria
{
    class CharacterCreation
    {
        static Player player = new Player();
        public void CreateCharacter()
        {
            Console.Clear();
            Console.WriteLine("What is your character's name?");
            player.PlayerName = Console.ReadLine();
            ChooseClass();
            SetClassAttributes();
            ManualAttributes();
            Console.ReadLine();
        }
        private void ChooseClass()
        {
            Console.WriteLine("\nWhat is your class?\n> Warrior\n  Archer\n  Mage");
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
                        switch (menuSelected)
                        {
                            case 1:
                                player.PlayerClass = Player.playerClass.Melee;
                                return;
                            case 2:
                                player.PlayerClass = Player.playerClass.Ranged;
                                return;
                            case 3:
                                player.PlayerClass = Player.playerClass.Magic;
                                return;
                        }
                        return;
                }

                if (menuSelected < 1)
                {
                    menuSelected = 3;
                }
                else if (menuSelected > 3)
                {
                    menuSelected = 1;
                }
                Console.SetCursorPosition(0, menuSelected + 3);
                Console.Write('>');
            }
        }

        private void SetClassAttributes()
        {
            switch(player.PlayerClass)
            {
                case Player.playerClass.Melee:
                    player.StrengthAtt = 13;
                    player.DexterityAtt = 10;
                    player.WisdomAtt = 7;
                    player.VitalityAtt = 130;
                    player.ManaAtt = 70;
                    player.EnduranceAtt = 100;
                    break;
                case Player.playerClass.Magic:
                    player.StrengthAtt = 13;
                    player.DexterityAtt = 10;
                    player.WisdomAtt = 7;
                    player.VitalityAtt = 130;
                    player.ManaAtt = 70;
                    player.EnduranceAtt = 100;
                    break;
                case Player.playerClass.Ranged:
                    player.StrengthAtt = 13;
                    player.DexterityAtt = 10;
                    player.WisdomAtt = 7;
                    player.VitalityAtt = 130;
                    player.ManaAtt = 70;
                    player.EnduranceAtt = 100;
                    break;
            }
        }

        private void ManualAttributes()
        {
            Console.Clear();
            int pointsLeft = 16;
            int menuSelected = 1;
            Console.WriteLine("Set your attributes manually. Points left are indicated below");
            Console.WriteLine("Strength:       {0}", player.StrengthAtt );
            Console.WriteLine("Dexterity:      {0}", player.DexterityAtt);
            Console.WriteLine("Wisdom:         {0}", player.WisdomAtt);
            Console.WriteLine("Vitality:       {0}", player.VitalityAtt);
            Console.WriteLine("Mana:           {0}", player.ManaAtt);
            Console.WriteLine("Endurance:      {0}\n", player.EnduranceAtt);
            Console.WriteLine("Points left to use: {0}", pointsLeft);
            while (true)
            {
                ConsoleKey input = Console.ReadKey().Key;
                Console.SetCursorPosition(16, menuSelected);
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
                        break;
                }

                if (menuSelected < 1)
                {
                    menuSelected = 6;
                }
                else if (menuSelected > 6)
                {
                    menuSelected = 1;
                }
                Console.SetCursorPosition(16, menuSelected);
                Console.Write('>');
            }
        }
    }
}
