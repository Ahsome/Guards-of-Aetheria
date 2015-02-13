using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardsOfAetheria
{
    class CharacterCreation
    {
        public void CreateCharacter()
        {
            Console.Clear();
            Console.WriteLine("What is your character's name?");
            Player.Instance.PlayerName = Console.ReadLine();
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
                                Player.Instance.PlayerClass = "Knight"; // Melee
                                return;
                            case 2:
                                Player.Instance.PlayerClass = "Ranger"; // Range
                                return;
                            case 3:
                                Player.Instance.PlayerClass = "Wizard"; // Magic
                                return;
                        }
                        break;
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
            Player.Instance.PrimaryAtt = 13;
            Player.Instance.SecondaryAtt = 10;
            Player.Instance.TertiaryAtt = 7;
            var Utility = new Utility();
            Utility.UpdateAtts();
        }

        private void ManualAttributes()
        {
            Console.Clear();
            int pointsLeft = 16;
            int[] tempPoints = { 0, 0, 0 };
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
                        Player.Instance.Strength += tempPoints[0];
                        Player.Instance.DexterityAtt += tempPoints[1];
                        Player.Instance.Wisdom += tempPoints[2];
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
            Console.WriteLine("Strength:       {0}", Player.Instance.Strength + tempPoints[0]);
            Console.WriteLine("Dexterity:      {0}", Player.Instance.DexterityAtt + tempPoints[1]);
            Console.WriteLine("Wisdom:         {0}", Player.Instance.Wisdom + tempPoints[2]);
            Console.WriteLine("Points left to use: {0}", pointsLeft);
        }
    }
}
