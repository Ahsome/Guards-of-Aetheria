using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Improved;

namespace GuardsOfAetheria
{
    internal class Bar : ProgressBar
    {
        public int Unbuffed;

        private Bar(int unbuffed)
        {
            Unbuffed = unbuffed;
            Current = unbuffed;
            Maximum = unbuffed;
        }

        private Bar(int unbuffed, int current, int maximum)
        {
            Unbuffed = unbuffed;
            Current = current;
            Maximum = maximum;
        }
    }
    
    internal class Entity
    {
        public string Name;

        public Bar Vitality = (Bar)new ProgressBar{ InitialColour = ConsoleColor.DarkRed, NewColour = ConsoleColor.Red };
        public Bar Endurance = (Bar)new ProgressBar { InitialColour = ConsoleColor.DarkYellow, NewColour = ConsoleColor.Yellow };
        public Bar Mana = (Bar)new ProgressBar { InitialColour = ConsoleColor.DarkBlue, NewColour = ConsoleColor.Blue };
        public Bar Stamina = (Bar)new ProgressBar { InitialColour = ConsoleColor.DarkGreen, NewColour = ConsoleColor.DarkGreen };

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
        
        public enum Classes : byte { Melee, Magic, Ranged }
        public enum Origins : byte { Nation, Treaty, Refugee }
        public enum Types : byte { Weapon, Armour, Comsumable, Material }
        public Classes Class { get; set; }
        public Origins Origin { get; set; }

        public int Experience { get; set; }
        public int Level { get; set; }

        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Wisdom { get; set; }

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

        public int AttributePoints { get; set; }
        public int RoomId { get; set; }
        public int InventorySpace { get; set; }
        //TODO BLOCK: Inventory stuff
        //Compartments
        //make compartments less accessible depending on stuff e.g. being in combat/leaving them behind (at home)
        //public List<Material> Materials { get; set; }
        //public List<Consumable> Consumables { get; set; }
        //Weapon, Offhand, Head, Chest, Arms, Gauntlets, Legs, Shoes
        //public List<Equipment> Equipped { get; set; }

        public void InitialiseAttributes() { PrimaryAttribute = 13; SecondaryAttribute = 10; TertiaryAttribute = 7; UpdateAttributes(); }

        public void UpdateAttributes()
        {
            Endurance.Unbuffed = 50 + Strength * 5 + Level * 5;
            Mana.Unbuffed = 50 + Wisdom * 5 + Level * 5;
            Stamina.Unbuffed = 50 + Dexterity * 5 + Level * 5;
            Vitality.Unbuffed = (int) (10 + Endurance.Unbuffed * 0.01); //TODO: buffed stats
            switch (Class)
            {
                case Classes.Melee: Strength = PrimaryAttribute; Wisdom = SecondaryAttribute; Dexterity = TertiaryAttribute; break;
                case Classes.Magic: Strength = TertiaryAttribute; Wisdom = PrimaryAttribute; Dexterity = SecondaryAttribute; break;
                case Classes.Ranged: Strength = SecondaryAttribute; Wisdom = TertiaryAttribute; Dexterity = PrimaryAttribute; break;
            }
        }

        public void AssignAttributes()
        {
            switch (Class)
            {
                case Classes.Melee: PrimaryAttribute = Strength; SecondaryAttribute = Wisdom; TertiaryAttribute = Dexterity; break;
                case Classes.Magic: TertiaryAttribute = Strength; PrimaryAttribute = Wisdom; SecondaryAttribute = Dexterity; break;
                case Classes.Ranged: SecondaryAttribute = Strength; TertiaryAttribute = Wisdom; PrimaryAttribute = Dexterity; break;
            }
        }

        public void Equip(int position)
        {
            //TODO:
        }

        public static void PrioritiseInventoryItems()
        {
            //TODO: label by importance, .Aggregate() by importance, Quicksort;
        }

        public static void SpaceLeft()
        {
            /*var spaceLeft = InventorySpace;
            //TODO: more compartments, large compartments, List<Equipment>
            for (var i = 0; i < 50; i++)
            {
                if (Inventory[i][1] == 0) continue;
                if (Inventory[i][0] == 2) spaceLeft -= Inventory[i][7];
                else spaceLeft--;
            }
            return spaceLeft;*/
        }
        
        public void TryLevelUp()
        {
            var expNeeded = (int) Math.Pow(1.05, Level) * 1000;
            if (Experience >= expNeeded) Level++; Experience -= expNeeded;
        }

        public void ShowMenu()
        {
            Console.Clear(); Console.WriteLine("Inventory");
            //SelectOption(InventoryName);
            //TODO: Add more options, centre text, SelectContinue
        }
    }

    internal class CharacterCreation
    {
        public static void Create()
        {
            while (true)
            {
                Console.Clear(); Console.Write("Your name is ");
                //TODO: more extensive character creation
                Console.SetCursorPosition(13, 0); var name = Console.ReadLine();
                if (name == null) continue;
                const string nonletters = @"-'";
                var nonlettersAreSpammed = false;
                var names = name.Split(' ');
                for (var i = 1; i < names.Length && !nonlettersAreSpammed; i++)
                    nonlettersAreSpammed = names[i].Where(t => nonletters.Any(u => t == u)).ToArray().Length > 1 ||
                        nonletters.Any(t => t == name.First() || t == name.Last());
                if (nonlettersAreSpammed) continue;
                //TODO: error message?
                name = new Regex(@" {2,}").Replace(name.Trim(), @" ");
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
            switch (new List<string> {
                "an average house in the safe provinces, loyal to the king",
                "an average house in a war-torn province, loyal to your lord", //TODO: find correct title
                "a refugee tent in a war-torn province, loyal to nobody" }.Select())
            {
                case 1: Player.Instance.Origin = Player.Origins.Nation; break;
                case 2: Player.Instance.Origin = Player.Origins.Treaty; break;
                case 3: Player.Instance.Origin = Player.Origins.Refugee; break;
            }

            Console.Clear();
            Console.WriteLine("You are:");
            var options = new List<string>();
            switch (Player.Instance.Origin)
            {
                case Player.Origins.Nation:
                    options = new List<string> {
                        "a skilled warrior, able to knock back a training dummy 10 metres with one blow",
                        "a skilled archer, able to hit a training dummy's heart from 100 metres away",
                        "a skilled mage, able to burn training dummies to a crisp in 10 seconds flat" };
                    break;
                case Player.Origins.Treaty:
                    options = new List<string> {
                        "a warrior, able to knock back an invader 10 metres with one blow",
                        "an archer, able to hit an invader's heart from 100 metres away",
                        "a mage, able to burn invaders to a crisp in 10 seconds flat" }; //TODO: rename invaders, and king maybe
                    break;
                case Player.Origins.Refugee:
                    options = new List<string> {
                        "a born warrior, able to knock back a sack of potatoes 10 metres with one blow",
                        "a born archer, able to hit a bullseye from 100 metres away",
                        "a born mage, able to burn a tree in 10 seconds flat" };
                    break;
            }
            switch (options.Select())
            {
                case 1: Player.Instance.Class = Player.Classes.Melee; break;
                case 2: Player.Instance.Class = Player.Classes.Ranged; break;
                case 3: Player.Instance.Class = Player.Classes.Magic; break;
            }
            // TODO: AssignStartingEquipment();
            Player.Instance.InitialiseAttributes();
            int attPoints;
            int numberOfLines;
            Consoles.WordWrap("Set your attributes manually. Points left are indicated below.", out numberOfLines);
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