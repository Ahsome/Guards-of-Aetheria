using System;

namespace GuardsOfAetheria
{
    internal class Player
    {
        public enum Class : byte { Melee, Magic, Ranged }

        public enum Origin : byte { Nation, Treaty, Refugee }

        private static readonly Player OrigInstance = new Player();

        private Player() {}
        static Player() {}

        public string Name { get; set; }
        public Class PlayerClass { get; set; }
        public Origin PlayerOrigin { get; set; }
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
        public int AccuracyAtt { get; set; }
        public int EvasionAtt { get; set; }
        public int LuckAtt { get; set; }
        public int PrimaryAtt { get; set; }
        public int SecondaryAtt { get; set; }
        public int TertiaryAtt { get; set; }
        public int PerceptionAtt { get; set; }
        public string[] InventoryItems { get; set; }
        public int[] InventoryCount { get; set; }
        public string LocationRegion { get; set; }
        public string LocationArea { get; set; }
        public string LocationBuilding { get; set; }
        public string LocationRoom { get; set; }
        public int InventorySpace { get; set; }
        // weapon = 0, arm = 1, con = 2, mat = 3
        //TODO: set 0 as weaponIndex etc
        //Compartments -> weapons/armours/consumables/materials -> details
        //details for weps: prefix, name, (suffix, avg damage, damage%, gem1 location, gem2 location, gem3 location
        //same for armours
        //TODO: make compartments less accessible depending on stuff e.g. being in combat/leaving them behind (at home)
        public int[][] Inventory { get; set; }
        public string[] InventoryName { get; set; }
        public int[][] InventoryOld { get; set; }
        //Weapon, Offhand, Head, Chest, Arms, Gauntlets, Legs, Shoes
        public int[][] Equipped { get; set; }

        public static Player Instance { get { return OrigInstance; } }

        // Melee = 1, Ranged = 2, Magic = 3
        // [] = {(Class, Class, Type, Material), (Weapon, Armour, Item), (Prefix, sortNumber), (Suffix), (Tier), (Rarity), (ortNumber)}

        public void InitialiseAtts() { Instance.PrimaryAtt = 13; Instance.SecondaryAtt = 10; Instance.TertiaryAtt = 7; UpdateAtts(); }

        public void UpdateAtts()
        {
            Instance.BaseEndurance = 50 + Instance.Strength*5 + Instance.Level*5;
            Instance.CurrentEndurance = Instance.BaseVitality + 0;
            Instance.BaseMana = 50 + Instance.Wisdom*5 + Instance.Level*5;
            Instance.CurrentMana = Instance.BaseMana + 0;
            Instance.BaseStamina = 50 + Instance.Dexterity*5 + Instance.Level*5;
            Instance.CurrentStamina = Instance.BaseEndurance + 0;
            Instance.BaseVitality = Convert.ToInt32(Math.Round(9.5 + Instance.BaseEndurance*0.01));
            switch (PlayerClass)
            {
                case Class.Melee:
                    Instance.Strength = Instance.PrimaryAtt;
                    Instance.Wisdom = Instance.SecondaryAtt;
                    Instance.Dexterity = Instance.TertiaryAtt;
                    break;
                case Class.Magic:
                    Instance.Strength = Instance.TertiaryAtt;
                    Instance.Wisdom = Instance.PrimaryAtt;
                    Instance.Dexterity = Instance.SecondaryAtt;
                    break;
                case Class.Ranged:
                    Instance.Strength = Instance.SecondaryAtt;
                    Instance.Wisdom = Instance.TertiaryAtt;
                    Instance.Dexterity = Instance.PrimaryAtt;
                    break;
            }
        }

        public void AssignAtts()
        {
            switch (PlayerClass)
            {
                case Class.Melee:
                    Instance.PrimaryAtt = Instance.Strength;
                    Instance.SecondaryAtt = Instance.Wisdom;
                    Instance.TertiaryAtt = Instance.Dexterity;
                    break;
                case Class.Magic:
                    Instance.TertiaryAtt = Instance.Strength;
                    Instance.PrimaryAtt = Instance.Wisdom;
                    Instance.SecondaryAtt = Instance.Dexterity;
                    break;
                case Class.Ranged:
                    Instance.SecondaryAtt = Instance.Strength;
                    Instance.TertiaryAtt = Instance.Wisdom;
                    Instance.PrimaryAtt = Instance.Dexterity;
                    break;
            }
        }
    }
}