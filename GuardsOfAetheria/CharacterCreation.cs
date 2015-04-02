using System;
using System.Linq;

namespace GuardsOfAetheria
{
    internal class CharacterCreation
    {
        public void CreateCharacter()
        {
            while (true)
            {
                Console.Clear(); Console.Write("Your name is ");
                //TODO: more extensive character creation
                Console.SetCursorPosition(13, 0); var name = Console.ReadLine();
                if (name == null) continue;
                const string nonletters = @" -'";
                var nonlettersAreSpammed = false;
                var names = name.Split(' ');
                for (var i = 1; i < names.Length && !nonlettersAreSpammed; i++)
                    nonlettersAreSpammed = names[i].Where(t => nonletters.Any(u => t == u)).ToArray().Length > 1 ||
                        nonletters.Any(t => t == name.First() || t == name.Last());
                if (nonlettersAreSpammed) continue;
                //TODO: error message?

                Console.Clear();
                Console.Write("Your name is {0}", name);
                if (Console.ReadKey(true).Key != ConsoleKey.Enter ||
                    (String.IsNullOrEmpty(name) ||
                    !name.Any(Char.IsLetter) ||
                    (name.Any(t => !Char.IsLetter(t) && nonletters.All(u => u != t))))) continue;
                Player.Instance.Name = name;
                break;
            }

            Console.Clear();
            Console.WriteLine("You come from:");
            //TODO: special plot selectoption that shows 1 at a time right after from?
            var option = new[]
            {
                "an average house in the safe provinces, loyal to the king",
                "an average house in a war-torn province, loyal to your lord", //TODO: find correct title
                "a refugee tent in a war-torn province, loyal to nobody"
            }.SelectOption();
            switch (option)
            {
                case 1: Player.Instance.PlayerOrigin = Player.Origin.Nation; break;
                case 2: Player.Instance.PlayerOrigin = Player.Origin.Treaty; break;
                case 3: Player.Instance.PlayerOrigin = Player.Origin.Refugee; break;
            }

            Console.Clear();
            Console.WriteLine("You are:");
            var options = new string[3];
            switch (Player.Instance.PlayerOrigin)
            {
                case Player.Origin.Nation:
                    options = new[] {
                        "a skilled warrior, able to knock back a training dummy 10 metres with one blow",
                        "a skilled archer, able to hit a training dummy's heart from 100 metres away",
                        "a skilled hotmial.com  mage, able to burn training dummies to a crisp in 10 seconds flat" };
                    break;
                case Player.Origin.Treaty:
                    options = new[] {
                        "a warrior, able to knock back an invader 10 metres with one blow",
                        "an archer, able to hit an invader's heart from 100 metres away",
                        "a mage, able to burn invaders to a crisp in 10 seconds flat" }; //TODO: rename invaders, and king maybe
                    break;
                case Player.Origin.Refugee:
                    options = new[] {
                        "a born warrior, able to knock back a sack of potatoes 10 metres with one blow",
                        "a born archer, able to hit a bullseye from 100 metres away",
                        "a born mage, able to burn a tree in 10 seconds flat" };
                    break;
            }
            option = options.SelectOption();
            switch (option)
            {
                case 1: Player.Instance.PlayerClass = Player.Class.Melee; break;
                case 2: Player.Instance.PlayerClass = Player.Class.Ranged; break;
                case 3: Player.Instance.PlayerClass = Player.Class.Magic; break;
            }
            // TODO: AssignStartingEquipment();
            Player.Instance.InitialiseAtts();
            Console.CursorLeft = 14;
            var permPoints = Utility.Spend(new[]
                {
                    "Set your attributes manually. Points left are indicated below.",
                    "You have ", " points left to use", " point left to use"
                },
                new[] { "Strength:", "Dexterity:", "Wisdom:" },
                new[] { Player.Instance.Strength, Player.Instance.Dexterity, Player.Instance.Wisdom },
                new[] { 0, 0, 0 }, new[] { 1, 1, 1 }, 16);
            Player.Instance.Strength = permPoints[0];
            Player.Instance.Dexterity = permPoints[1];
            Player.Instance.Wisdom = permPoints[2];
            //TODO: array? look for vs addin that finds what argument name something refers to
        }
    }
}