using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GuardsOfAetheria
{
    internal class Entity
    {
        //TODO: move stuff from player
    }
    internal class Player : Entity
    {
        //TODO: attributes class?
        private static readonly Lazy<Player> Lazy = new Lazy<Player>(() => new Player());
        public static Player Instance { get { return Lazy.Value; } }
        private Player(){}
        
        public enum Classes : byte { Melee, Magic, Ranged }
        public enum Origins : byte { Nation, Treaty, Refugee }
        public string Name { get; set; }
        public Classes Class { get; set; }
        public Origins Origin { get; set; }
        public int Experience { get; set; }
        public int Level { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Wisdom { get; set; }
        public int BaseVitality { get; set; }
        public int CurrentVitality { get; set; }
        public int MaxVitality { get; set; }
        public int BaseMana { get; set; }
        public int CurrentMana { get; set; }
        public int MaxMana { get; set; }
        public int BaseEndurance { get; set; }
        public int CurrentEndurance { get; set; }
        public int MaxEndurance { get; set; }
        public int BaseStamina { get; set; }
        public int CurrentStamina { get; set; }
        public int MaxStamina { get; set; }
        public int Defence { get; set; }
        public int Attack { get; set; }
        public int Shield { get; set; } // Magic Resist
        public int Accuracy { get; set; }
        public int Evasion { get; set; }
        public int Luck { get; set; }
        public int PrimaryAttribute { get; set; }
        public int SecondaryAttribute { get; set; }
        public int TertiaryAttribute { get; set; }
        public int Perception { get; set; }
        /*public string Region { get; set; }
        public string Area { get; set; }
        public string Building { get; set; }
        public string Room { get; set; }*/
        //TODO replace stuff with roomid, above only for names/maps
        public int RoomId { get; set; }
        public int InventorySpace { get; set; }
        // weapon = 0, arm = 1, con = 2, mat = 3
        //TODO: set 0 as weaponIndex etc
        //Compartments -> weapons/armours/consumables/materials -> details
        //details for weps: prefix, name, (suffix, avg damage, damage%, gem1 id, gem2 id, gem3 id, dictionary?
        //same for armours
        //TODO: make compartments less accessible depending on stuff e.g. being in combat/leaving them behind (at home)
        public List<List<int>> Inventory { get; set; }
        public List<string> InventoryName { get; set; }
        public List<List<int>> InventoryOld { get; set; }
        //Weapon, Offhand, Head, Chest, Arms, Gauntlets, Legs, Shoes
        public List<List<int>> Equipment { get; set; }

        // Melee = 1, Ranged = 2, Magic = 3
        // [] = {(Class, Class, Type, Material), (Weapon, Armour, Item), (Prefix, sortNumber), (Suffix), (Tier), (Rarity), (sortNumber)}

        public void InitialiseAttributes() { PrimaryAttribute = 13; SecondaryAttribute = 10; TertiaryAttribute = 7; UpdateAttributes(); }

        public void UpdateAttributes()
        {
            BaseEndurance = 50 + Strength * 5 + Level * 5;
            CurrentEndurance = BaseVitality + 0;
            BaseMana = 50 + Wisdom * 5 + Level * 5;
            CurrentMana = BaseMana + 0;
            BaseStamina = 50 + Dexterity * 5 + Level * 5;
            CurrentStamina = BaseEndurance + 0;
            BaseVitality = Convert.ToInt32(Math.Round(9.5 + BaseEndurance * 0.01));
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

        }
    }
    internal class Players
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
            //TODO: special plot selectoption that shows 1 at a time right after from?
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
            var permanentPoints = Utility.Spend("Set your attributes manually. Points left are indicated below.",
                new List<string> { "You have ", " points", " left to use" },
                new List<string> { "Strength:", "Dexterity:", "Wisdom:","0","0","0","0","0","0","0","3","0","0","0","","","","","","","","2","","" ,"","","1"},
                new List<int> { Player.Instance.Strength, Player.Instance.Dexterity, Player.Instance.Wisdom ,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}, new List<int> { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0 }, 16, arrowPosition: 14);
            Player.Instance.Strength = permanentPoints[0]; Player.Instance.Dexterity = permanentPoints[1]; Player.Instance.Wisdom = permanentPoints[2];
            //TODO: stats -> array?
            Player.Instance.RoomId = 1;
        }
    }
}