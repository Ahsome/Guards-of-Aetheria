using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardsOfAetheria
{
    class CharacterCreation
    {
        public static Player player = new Player();
        public void CreateCharacter()
        {
            Console.Clear();
            Console.WriteLine("What is your character's name?");
            player.PlayerName = Console.ReadLine();
            ChooseClass();
            SetClassAttributes();
            ManualAttributes();
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
                int optionSelected = UniversalMethods.SelectOption(3, 7);

                switch (optionSelected)
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
        }

        private void SetClassAttributes()
        {
            player.PrimaryAtt = 13;
            player.SecondaryAtt = 10;
            player.TertiaryAtt = 7;
            player.VitalityAtt = 130;
            player.EnduranceAtt = 100; 
            player.ManaAtt = 70;            
        }

        private void ManualAttributes()
        {
            Console.Clear();
            int pointsLeft = 16;
            int[] tempPoints = { 0, 0, 0, 0, 0, 0 };
            int menuSelected = 0;
            CreationAttributeGraphics(pointsLeft, tempPoints);
            Console.SetCursorPosition(14, 1);
            Console.Write('>');
            while (true)
            {
                ConsoleKey input = Console.ReadKey().Key;
                Console.SetCursorPosition(14, menuSelected + 1);
                Console.Write(' ');

                switch (input)
                {
                    case ConsoleKey.UpArrow:
                        menuSelected--;
                        break;
                    case ConsoleKey.DownArrow:
                        menuSelected++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (tempPoints[menuSelected] > 0)
                        {
                            tempPoints[menuSelected]--;
                            pointsLeft++;
                            CreationAttributeGraphics(pointsLeft, tempPoints);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (pointsLeft > 0)
                        {
                            tempPoints[menuSelected]++;
                            pointsLeft--;
                            CreationAttributeGraphics(pointsLeft, tempPoints);
                        }
                        break;
                    case ConsoleKey.Enter:
                        player.StrengthAtt += tempPoints[0];
                        player.DexterityAtt +=tempPoints[1];
                        player.WisdomAtt +=tempPoints[2];
                        player.VitalityAtt +=tempPoints[3];
                        player.ManaAtt +=tempPoints[4];
                        player.EnduranceAtt += tempPoints[5];
                        return;
                }

                if (menuSelected < 0)
                {
                    menuSelected = 5;
                }
                else if (menuSelected > 5)
                {
                    menuSelected = 0;
                }
                Console.SetCursorPosition(14, menuSelected + 1);
                Console.Write('>');
            }
        }

        private void CreationAttributeGraphics(int pointsLeft, int[] tempPoints)
        {
            Console.Clear();
            Console.WriteLine("Set your attributes manually. Points left are indicated below");
            Console.WriteLine("Strength:       {0}", player.StrengthAtt + tempPoints[0]);
            Console.WriteLine("Dexterity:      {0}", player.DexterityAtt + tempPoints[1]);
            Console.WriteLine("Wisdom:         {0}", player.WisdomAtt + tempPoints[2]);
            Console.WriteLine("Vitality:       {0}", player.VitalityAtt + tempPoints[3]);
            Console.WriteLine("Mana:           {0}", player.ManaAtt + tempPoints[4]);
            Console.WriteLine("Endurance:      {0}\n", player.EnduranceAtt + tempPoints[5]);
            Console.WriteLine("Points left to use: {0}", pointsLeft);
        }
    }
}
