using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Improved.Consoles;

namespace GuardsOfAetheria
{
    internal class Bar : ProgressBar
    {
        public int Unbuffed;

        public Bar(){}

        public Bar(int unbuffed)
        {
            Unbuffed = unbuffed;
            Current = unbuffed;
            Maximum = unbuffed;
        }

        public Bar(int unbuffed, int current, int maximum)
        {
            Unbuffed = unbuffed;
            Current = current;
            Maximum = maximum;
        }
    }
    
    internal class Entity
    {
        public string Name;

        public Bar Vitality = new Bar { InitialColour = ConsoleColor.DarkRed, NewColour = ConsoleColor.Red };
        public Bar Endurance = new Bar { InitialColour = ConsoleColor.DarkYellow, NewColour = ConsoleColor.Yellow };
        public Bar Mana = new Bar { InitialColour = ConsoleColor.DarkBlue, NewColour = ConsoleColor.Blue };
        public Bar Stamina = new Bar { InitialColour = ConsoleColor.DarkGreen, NewColour = ConsoleColor.DarkGreen };

        //restrict to 2? or will there be octopus enemies?
        public List<Weapon> Weapons;
        public List<Armour> Armour;
    }
    internal class Player : Entity
    {
        //attributes class? db stuff apart from tables?
        private static readonly Lazy<Player> Lazy = new Lazy<Player>(() => new Player());
        public static Player Instance { get { return Lazy.Value; } }
        private Player(){}
        
        public enum Class : byte { Melee, Magic, Ranged }
        public enum Origin : byte { Nation, Treaty, Refugee }
        public enum Type : byte { Weapon, Armour, Comsumable, Material }
        public Class PlayerClass { get; set; }
        public Origin PlayerOrigin { get; set; }

        public int Experience { get; set; }
        public int Level { get; set; }

        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Wisdom { get; set; }

        public int AttributePoints { get; set; }
        public int RoomId { get; set; }
        public int InventorySpace { get; set; }

        public int Attack { get; set; }
        public int Defence { get; set; }
        public int Shield { get; set; } // Magic Resist
        public int Accuracy { get; set; }
        public int Evasion { get; set; }
        public int Luck { get; set; }
        public int Perception { get; set; }

        public int PrimaryAttribute { get; set; }
        public int SecondaryAttribute { get; set; }
        public int TertiaryAttribute { get; set; }

        //TODO BLOCK: Inventory stuff
        //Compartments
        //make compartments less accessible depending on stuff e.g. being in combat/leaving them behind (at home)
        public List<Item> Inventory { get; set; }
        public List<Equipment> Equipped { get; set; } 
        //Weapon, Offhand, Head, Chest, Arms, Gauntlets, Legs, Shoes

        public void InitialiseAttributes() { PrimaryAttribute = 13; SecondaryAttribute = 10; TertiaryAttribute = 7; UpdateAttributes(); }

        public void UpdateAttributes()
        {
            switch (PlayerClass)
            {
                case Class.Melee: Strength = PrimaryAttribute; Wisdom = SecondaryAttribute; Dexterity = TertiaryAttribute; break;
                case Class.Magic: Strength = TertiaryAttribute; Wisdom = PrimaryAttribute; Dexterity = SecondaryAttribute; break;
                case Class.Ranged: Strength = SecondaryAttribute; Wisdom = TertiaryAttribute; Dexterity = PrimaryAttribute; break;
            }
            Endurance.Unbuffed = 50 + Strength * 5 + Level * 5;
            Mana.Unbuffed = 50 + Wisdom * 5 + Level * 5;
            Stamina.Unbuffed = 50 + Dexterity * 5 + Level * 5;
            Vitality.Unbuffed = (int) (10 + Endurance.Unbuffed * 0.01); //TODO: buffed stats
        }

        public void AssignAttributes()
        { switch (PlayerClass) {
                case Class.Melee: PrimaryAttribute = Strength; SecondaryAttribute = Wisdom; TertiaryAttribute = Dexterity; break;
                case Class.Magic: TertiaryAttribute = Strength; PrimaryAttribute = Wisdom; SecondaryAttribute = Dexterity; break;
                case Class.Ranged: SecondaryAttribute = Strength; TertiaryAttribute = Wisdom; PrimaryAttribute = Dexterity; break;
        } }

        public void Equip(int inventoryIndex)
        {
            //TODO:
        }

        public static void PrioritiseInventoryItems()
        {
            //label by importance, .Aggregate() by importance, Quicksort;
        }

        //more compartments, large compartments, List<Equipment>
        //subtract inventoryspace once item is bought/sold
        
        public void TryLevelUp()
        {
            var expNeeded = (int) Math.Pow(1.05, Level) * 1000;
            if (Experience >= expNeeded) Level++; Experience -= expNeeded;
        }

        public void ShowMenu()
        {
            Console.Clear(); Console.WriteLine("Inventory");
            (from i in Inventory select i.Name).ToArray().Choose();
            //Add more options, centre text, SelectContinue
        }
    }

    internal class CharacterCreation
    {
        public const string Nonletters = @"-'";
        public static void Create()
        {
            while (true)
            {
                Console.Clear(); Console.Write("Your name is ");
                //TODO: more extensive character creation
                var name = Console.ReadLine();
                if (name == null) continue;
                var nonlettersAreSpammed = false;
                var names = name.Split(' ');
                foreach (var s in names)
                {
                    nonlettersAreSpammed = s.Where(letter => Nonletters.Any(nonletter => letter == nonletter)).ToList().Count > 1;
                    if (nonlettersAreSpammed) break;
                }
                //TODO: shorten, notify player
                name = new Regex(" {2,}").Replace(name.Trim(' ', '-', '\''), " ");
                Console.Clear(); Console.Write("Your name is {0}.", name);
                if (Console.ReadKey(true).Key != ConsoleKey.Enter ||
                    String.IsNullOrWhiteSpace(name) ||
                    !name.Any(Char.IsLetter) ||
                    nonlettersAreSpammed ||
                    name.Any(letter => !Char.IsLetter(letter) && Nonletters.All(nonletter => nonletter != letter))) continue;
                Player.Instance.Name = name;
                break;
            }
            #region Choose Origin
            Console.Clear(); Console.WriteLine("You come from");
            Player.Instance.PlayerOrigin = (Player.Origin) new[]
            {
                "an average house in the safe provinces, loyal to the king",
                "an average house in a war-torn province, loyal to your lord", //TODO: find correct title
                "a refugee tent in a war-torn province, loyal to nobody"
            }.Choose();
            #endregion
            #region Choose Class
            Console.Clear(); Console.WriteLine("You are");
            var options = new string[3];
            switch (Player.Instance.PlayerOrigin)
            {
                case Player.Origin.Nation:
                    options = new[] {
                        "a skilled warrior, able to knock back a training dummy 10 metres with one blow",
                        "a skilled archer, able to hit a training dummy's heart from 100 metres away",
                        "a skilled mage, able to burn training dummies to a crisp in 10 seconds flat" };
                    break;
                case Player.Origin.Treaty:
                    options = new[] {
                        "a warrior, able to knock back an invader 10 metres with one blow",
                        "an archer, able to hit an invader's heart from 100 metres away",
                        "a mage, able to burn invaders to a crisp in 10 seconds flat" }; //TODO: rename invaders, and king maybe
                    break;
                case Player.Origin.Refugee:
                    options = new[] {
                        "a born warrior, able to knock a sack of potatoes 10 metres back with one blow",
                        "a born archer, able to hit a bullseye from 100 metres away",
                        "a born mage, able to burn a tree in 10 seconds flat" };
                    break;
            }
            Player.Instance.PlayerClass = (Player.Class) options.Choose();
            #endregion
            //TODO: AssignStartingEquipment();
            Player.Instance.InitialiseAttributes();
            int attPoints;
            Consoles.WordWrap("Set your attributes manually. Points left are indicated below.");
            var permanentPoints = Consoles.Spend(
                new List<string> { "You have ", " points", " left to use" },
                new List<string> { "Strength:", "Dexterity:", "Wisdom:" },
                new List<int> { Player.Instance.Strength, Player.Instance.Dexterity, Player.Instance.Wisdom },
                new List<int> { 1, 1, 1 }, 16, out attPoints, arrowPosition: 14);
            Player.Instance.Strength = permanentPoints[0]; Player.Instance.Dexterity = permanentPoints[1]; Player.Instance.Wisdom = permanentPoints[2];
            Player.Instance.AttributePoints = attPoints;
            Player.Instance.RoomId = 1;
        }
    }
}                                                                                                                                                 
