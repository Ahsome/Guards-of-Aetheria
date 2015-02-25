using System.ComponentModel;

namespace GuardsOfAetheria
{
    class Player
    {
        public enum Class
        {
            Melee,
            Magic,
            Ranged
        }
        public string Name { get; set; }
        public Class PlayerClass { get; set; }

        public int Experience { get; set; }
        public int Level { get; set; }

        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Wisdom { get; set; }

        public int BaseVitality { get; set; }
        public int CurrentVitality { get; set; }
        public int MaxVitality { get; set; } // Max. Health?

        public int BaseMana { get; set; }
        public int CurrentMana { get; set; }
        public int MaxMana { get; set; }

        public int BaseEndurance { get; set; }
        public int CurrentEndurance { get; set; }
        public int MaxEndurance { get; set; }

        public int Defence { get; set; }
        public int Attack { get; set; }
        public int Sheild { get; set; } // Magic Resist

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

        // Temp initialisation
        public string[] Settings = { "Pages" };
        // Temp end

        public int InventorySpace { get; set; }
        //Compartments -> weapons/armours/consumables/materials -> details
        //Inventory[][][] { get; set; }
        public int[][][] Inventory { get; set; }
        public string[][] InventoryName { get; set; }
        public string[] InventoryNameAll { get; set; }
        public int[][][] InventoryOld { get; set; }
        // Melee = 1, Ranged = 2, Magic = 3
        // [] = {(Class, Class, Type, Material), (Weapon, Armour, Item), (Prefix, sortNumber), (Suffix), (Tier), (Rarity), (ortNumber)}

        public void UpdateInventoryNameAll()
        {
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 50; j++)
                {
                    InventoryNameAll[50 * i + j + 1] = InventoryName[i][j];
                }
            }
        }
        public void UpdateInventoryName()
        {
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < InventoryName[i].Length; j++)
                {
                    InventoryName[i][j] = InventoryNameAll[50 * i + j + 1];
                }
            }
        }
        public void InitialiseAtts()
        {
            Instance.PrimaryAtt = 13;
            Instance.SecondaryAtt = 10;
            Instance.TertiaryAtt = 7;
            UpdateAtts();
        }
        public void UpdateAtts()
        {
            Instance.BaseVitality = Instance.Strength * 10; //Change level effect amount for divverent classes?
            Instance.CurrentVitality = Instance.BaseVitality + 0;
            Instance.BaseMana = Instance.Wisdom * 10 + Instance.Level * 5;
            Instance.CurrentMana = Instance.BaseMana + 0;
            Instance.BaseEndurance = Instance.Dexterity * 10 + Instance.Level * 5;
            Instance.CurrentEndurance = Instance.BaseEndurance + 0;
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
        

        private static readonly Player instance = new Player();

        static Player()
        {
        }

        private Player()
        {
        }

        public static Player Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
