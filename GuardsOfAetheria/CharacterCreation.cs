using System;

namespace GuardsOfAetheria
{
    class CharacterCreation
    {
        readonly Utility utility = new Utility();
        public void CreateCharacter()
        {
            Console.Clear();
            Console.WriteLine("What is your character's name?");
            Player.Instance.Name = Console.ReadLine();
            ChooseClass();
            Player.Instance.InitialiseAtts();
            ManualAttributes();
        }
        private void ChooseClass()
        {
            Console.WriteLine("\nWhat is your class?");
            string[] options = {"Warrior", "Archer", "Mage"};
            int menuSelected = utility.SelectOption(options);
            switch (menuSelected)
            {
                case 1:
                    Player.Instance.PlayerClass = Player.Class.Melee;
                    return;
                case 2:
                    Player.Instance.PlayerClass = Player.Class.Ranged;
                    return;
                case 3:
                    Player.Instance.PlayerClass = Player.Class.Magic;
                    return;
            }
        }

        private void ManualAttributes()
        {
            Console.Clear();
            int pointsLeft = 16;
            int[] tempPoints = { 0, 0, 0 };
            int menuSelected = 1;
            AttributeGraphics(pointsLeft, tempPoints);
            Console.SetCursorPosition(14, 1);
            Console.Write('>');
            while (true)
            {
                ConsoleKey input = Console.ReadKey().Key;
                Console.SetCursorPosition(14, menuSelected);
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
                        if (tempPoints[menuSelected-1] > 0)
                        {
                            tempPoints[menuSelected-1]--;
                            pointsLeft++;
                            AttributeGraphics(pointsLeft, tempPoints);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (pointsLeft > 0)
                        {
                            tempPoints[menuSelected-1]++;
                            pointsLeft--;
                            AttributeGraphics(pointsLeft, tempPoints);
                        }
                        break;
                    case ConsoleKey.Enter:
                        Player.Instance.Strength += tempPoints[0];
                        Player.Instance.Dexterity += tempPoints[1];
                        Player.Instance.Wisdom += tempPoints[2];
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
                Console.SetCursorPosition(14, menuSelected);
                Console.Write('>');
            }
        }

        private void AttributeGraphics(int pointsLeft, int[] tempPoints)
        {
            Console.Clear();
            Console.WriteLine("Set your attributes manually. Points left are indicated below");
            Console.WriteLine("Strength:       {0}", Player.Instance.Strength + tempPoints[0]);
            Console.WriteLine("Dexterity:      {0}", Player.Instance.Dexterity + tempPoints[1]);
            Console.WriteLine("Wisdom:         {0}", Player.Instance.Wisdom + tempPoints[2]);
            Console.WriteLine("Points left to use: {0}", pointsLeft);
        }
    }
}
