using System;
using System.Linq;

namespace GuardsOfAetheria
{
    internal class CharacterCreation
    {
        private readonly Utility utility = new Utility();
        //TODO: name check
        public void CreateCharacter()
        {
            ChooseName();
            ChooseOrigin();
            ChooseClass();
            // TODO: AssignStartingEquipment();
            Player.Instance.InitialiseAtts();
            ManualAttributes();
        }

        private void static ChooseName()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Your name is ");
                Console.SetCursorPosition(13, 0);
                var name = Console.ReadLine();
                while (name != null && name.Contains("  "))
                {
                    name = name.Replace("  ", " ");
                } //TODO: optimise?
                Console.Clear();
                Console.Write("Your name is {0}", name);
                if (Console.ReadKey(true).Key == ConsoleKey.Enter &&
                    !(
                        string.IsNullOrEmpty(name) ||
                        !name.Any(Char.IsLetter) ||
                        (name.Any(t => !Char.IsLetter(t) && @" -'".All(u => u != t)))
                        ))
                {
                    Player.Instance.Name = name;
                    return;
                }
            }
        }

        private void ChooseOrigin()
        {
            Console.Clear();
            Console.WriteLine("You come from:");
            string[] options =
            {
                "an average house in the safe provinces, loyal to the king",
                "an average house in a war-torn province, loyal to your lord", //TODO: check correct title
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
            var options = new string[3];
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
            int[] permPoints = {Player.Instance.Strength, Player.Instance.Dexterity, Player.Instance.Wisdom};
            int[] tempPoints = {0, 0, 0};
            int[] cost = {1, 1, 1};
            string[] text =
            {
                "Set your attributes manually. Points left are indicated below.",
                "You have",
                "points left to use"
            };
            string[] attNames = {"Strength:", "Dexterity:", "Wisdom:"}; //TODO: second person
            permPoints = utility.Spend(text, attNames, permPoints, tempPoints, cost, 16, 14);
            Player.Instance.Strength = permPoints[0];
            Player.Instance.Dexterity = permPoints[1];
            Player.Instance.Wisdom = permPoints[2];
        }
    }
}