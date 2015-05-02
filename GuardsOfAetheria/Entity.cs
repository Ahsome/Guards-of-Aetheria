using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Improved.Consoles;
using static System.Console;

namespace GuardsOfAetheria
{
    public class Bar : ProgressBar
    {
        public int Unbuffed;

        public Bar(){}

        public Bar(int unbuffed) { Unbuffed = Current = Maximum = unbuffed; }

        public Bar(int unbuffed, int current, int maximum) { Unbuffed = unbuffed; Current = current; Maximum = maximum; }
    }

    public class Bars
    {
        public Bar Vitality = new Bar { InitialColour = ConsoleColor.DarkRed, NewColour = ConsoleColor.Red };
        public Bar Endurance = new Bar { InitialColour = ConsoleColor.DarkYellow, NewColour = ConsoleColor.Yellow };
        public Bar Mana = new Bar { InitialColour = ConsoleColor.DarkBlue, NewColour = ConsoleColor.Blue };
        public Bar Stamina = new Bar { InitialColour = ConsoleColor.DarkGreen, NewColour = ConsoleColor.DarkGreen };
    }

    internal class Entity
    {
        public string Name;

        public Bars Bars = new Bars();

        //restrict to 2? or will there be octopus enemies?
        public List<Weapon> Weapons;
        public List<Armour> Armour;
    }

    public class Atts
    {
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Wisdom { get; set; }

        public int Primary { get; set; }
        public int Secondary { get; set; }
        public int Tertiary { get; set; }

        public int Points { get; set; }
    }

    public class Stats
    {
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int Shield { get; set; } // Magic Resist
        public int Accuracy { get; set; }
        public int Evasion { get; set; }
        public int Luck { get; set; }
        public int Perception { get; set; }

    }

    internal class Player : Entity
    {
        private static readonly Lazy<Player> Lazy = new Lazy<Player>(() => new Player());
        public static Player Instance => Lazy.Value;
        private Player(){}
        
        public enum Class : byte { Melee, Magic, Ranged }
        public enum Origin : byte { Nation, Treaty, Refugee }
        public enum Type : byte { Weapon, Armour, Comsumable, Material }
        public Class PlayerClass { get; set; }
        public Origin PlayerOrigin { get; set; }

        public int Experience { get; set; }
        public int Level { get; set; }

        public int RoomId { get; set; }
        public int InventorySpace { get; set; }

        public Atts Atts = new Atts();

        //TODO BLOCK: Inventory stuff
        //Compartments
        //more compartments, large compartments
        //subtract inventoryspace once item is bought/sold
        //make compartments less accessible depending on stuff e.g. being in combat/leaving them behind (at home)
        public List<Item> Inventory { get; set; }
        public List<Equipment> Equipped { get; set; } 
        //Weapon, Offhand, Head, Chest, Arms, Gauntlets, Legs, Shoes

        public void UpdateAtts()
        {
            switch (PlayerClass)
            {
                case Class.Melee: Atts.Strength = Atts.Primary; Atts.Wisdom = Atts.Secondary; Atts.Dexterity = Atts.Tertiary; break;
                case Class.Magic: Atts.Strength = Atts.Tertiary; Atts.Wisdom = Atts.Primary; Atts.Dexterity = Atts.Secondary; break;
                case Class.Ranged: Atts.Strength = Atts.Secondary; Atts.Wisdom = Atts.Tertiary; Atts.Dexterity = Atts.Primary; break;
            }
            Bars.Endurance.Unbuffed = 50 + Atts.Strength * 5 + Level * 5;
            Bars.Mana.Unbuffed = 50 + Atts.Wisdom * 5 + Level * 5;
            Bars.Stamina.Unbuffed = 50 + Atts.Dexterity * 5 + Level * 5;
            Bars.Vitality.Unbuffed = (int) (10 + Bars.Endurance.Unbuffed * 0.01); //TODO: buffed stats
        }

        public void AssignAtts()
        { switch (PlayerClass) {
                case Class.Melee: Atts.Primary = Atts.Strength; Atts.Secondary = Atts.Wisdom; Atts.Tertiary = Atts.Dexterity; break;
                case Class.Magic: Atts.Tertiary = Atts.Strength; Atts.Primary = Atts.Wisdom; Atts.Secondary = Atts.Dexterity; break;
                case Class.Ranged: Atts.Secondary = Atts.Strength; Atts.Tertiary = Atts.Wisdom; Atts.Primary = Atts.Dexterity; break;
        } }

        public void Equip(int inventoryIndex)
        {
            //TODO:
        }

        public static void SortInventory()
        {
            //label by importance, .Aggregate() by importance, Quicksort;
        }

        public void TryLevelUp()
        {
            var expNeeded = (int) Math.Pow(1.05, Level) * 1000;
            if (Experience >= expNeeded) Level++; Experience -= expNeeded;
        }

        public void ShowMenu()
        {
            Clear(); WriteLine("Inventory");
            (from i in Inventory select i.Name).ToArray().Choose();
            //Add more options, centre text, SelectContinue
        }
    }

    internal class CharacterCreation
    {
        const string NonLetters = " -'";
        public static void Create()
        {
            var name = "";
            do
            {
                Clear();
                Write("Your name is ");
                //TODO: more extensive character creation
                #region Choose Name
                while (string.IsNullOrWhiteSpace(name)) name = ReadLine();
                //TODO: shorten, notify player
                Clear();
                Write("Your name is {0}.", name = new Regex(" {2,}").Replace(name.Trim(NonLetters.ToCharArray()), " "));
            } while (new Regex($"[{NonLetters}]" + "{2,}").Match(name).Success ||
                     ReadKey(true).Key != ConsoleKey.Enter ||
                     string.IsNullOrWhiteSpace(name) ||
                     !name.Any(char.IsLetter) ||
                     name.Any(l => !char.IsLetter(l) && NonLetters.All(n => n != l)));
            Player.Instance.Name = name;
            #endregion
            #region Choose Origin
            Clear(); WriteLine("You come from");
            Player.Instance.PlayerOrigin = (Player.Origin) new[]
            {
                "an average house in the safe provinces, loyal to the king",
                "an average house in a war-torn province, loyal to your lord", //TODO: find correct title
                "a refugee tent in a war-torn province, loyal to nobody"
            }.Choose();
            #endregion
            #region Choose Class
            Clear(); WriteLine("You are");
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
                        "a mage, able to burn invaders to a crisp in 10 seconds flat" };
                    //TODO: rename invaders, and king maybe
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
            Player.Instance.Atts.Primary = 13;
            Player.Instance.Atts.Secondary = 10;
            Player.Instance.Atts.Tertiary = 7;
            Player.Instance.UpdateAtts();
            int attPoints;
            Consoles.WordWrap("Set your attributes manually. Points left are indicated below.");
            var permanentPoints = Consoles.Spend(
                new List<string> { "You have ", " points", " left to use" },
                new List<string> { "Strength:", "Dexterity:", "Wisdom:" },
                new List<int> { Player.Instance.Atts.Strength, Player.Instance.Atts.Dexterity, Player.Instance.Atts.Wisdom },
                new List<int> { 1, 1, 1 }, 16, out attPoints, arrowPosition: 14);
            Player.Instance.Atts.Strength = permanentPoints[0]; Player.Instance.Atts.Dexterity = permanentPoints[1]; Player.Instance.Atts.Wisdom = permanentPoints[2];
            Player.Instance.Atts.Points = attPoints;
            Player.Instance.RoomId = 1;
        }
    }
}                                                                                                                                                 
