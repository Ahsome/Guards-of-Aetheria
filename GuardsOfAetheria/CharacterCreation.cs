using System;
using System.Net.WebSockets;

namespace GuardsOfAetheria
{
    class CharacterCreation
    {
        readonly Utility utility = new Utility();
        public void CreateCharacter()
        {
            Console.Clear();
            Console.WriteLine("Your name is:");
            Player.Instance.Name = Console.ReadLine();
            ChooseOrigin();
            ChooseClass();
            // TODO: AssignStartingEquipment();
            Player.Instance.InitialiseAtts();
            ManualAttributes();
        }

        private void ChooseOrigin()
        {
            Console.Clear();
            Console.WriteLine("You come from:");
            string[] options =
            {
                "an average house in the safe provinces, loyal to the king",
                "an average house in a war-torn province, loyal to your lord", //lord?
                "a refugee tent in a war-torn province, loyal to nobody"
            };
            var menuSelected = utility.SelectOption(options);
            switch (menuSelected)
            {
                case 1:
                    Player.Instance.PlayerOrigin = Player.Origin.Nation;
                    return;
                case 2:
                    Player.Instance.PlayerOrigin = Player.Origin.Treaty;
                    return;
                case 3:
                    Player.Instance.PlayerOrigin = Player.Origin.Refugee;
                    return;
            }
        }
        private void ChooseClass()
        {
            Console.Clear();
            Console.WriteLine("You are:");
            string[] options = {"", "", "" };
            switch (Player.Instance.PlayerOrigin)
            {
                case Player.Origin.Nation:
                    options = new[]
                    {
                        "a skilled warrior, able to knock back a training dummy 10 metres with one blow",
                        "a skilled archer, able to hit a training dummy's heart from 100 metres away",
                        "a skilled hotmial.com  mage, able to burn training dummies to a crisp in 10 seconds flat"
                    };
                    break;
                case Player.Origin.Treaty:
                    options = new[]
                    {
                        "a warrior, able to knock back an enemy 10 metres with one blow",
                        "an archer, able to hit an enemy's heart from 100 metres away",
                        "a mage, able to burn enemies to a crisp in 10 seconds flat"
                    };
                    break;
                case Player.Origin.Refugee:
                    options = new[]
                    {
                        "a born warrior, able to knock back a sack of potatoes 10 metres with one blow",
                        "a born archer, able to hit a bullseye from 100 metres away",
                        "a born mage, able to burn a tree in 10 seconds flat"
                    };
                    break;
            }
            var menuSelected = utility.SelectOption(options);
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
            var pointsLeft = 16;
            int[] tempPoints = { 0, 0, 0 };
            var menuSelected = 1;
            AttributeGraphics(pointsLeft, tempPoints);
            Console.SetCursorPosition(14, 1);
            Console.Write('>');
            while (true)
            {
                var input = Console.ReadKey().Key;
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
