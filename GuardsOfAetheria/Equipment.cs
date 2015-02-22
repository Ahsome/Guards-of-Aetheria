using System;

namespace GuardsOfAetheria
{
    class General
    {
        public enum Rarity
        {
            Common,
            Uncommon,
            Rare,
            Epic,
            Legendary,
            Supreme
        }
        public enum Material
        {
            Copper,
            Iron,
            Steel
        }
        public enum MaterialMods
        {
            Silvered,
            Gilded
        }
        public enum Alloys
        {
            //Brass?
        }
    }
    class Weapon
    {
        readonly string[][][] _weapons = {
        new[]
        {
            new[]
            {
                "Broadsword",
                "Shortsword"
            },
            new[]
            {
                "Axe"
            },
            new[]
            {
                "Club",
                "Mace",
                "Flail"
            },
            new[]
            {
                "Spear",
                "Halberd?"
            }
        },
        new[]
        {
            new[]
            {
                "Javelin"
            },
            new[]
            {
                "Bow"
            }
        },
        new[]
        {
            new[]
            {
                "Tomes",
                "?",
                "?"
            }
        }
    };
        string[] _prefixes = {
            "Featherweight (+speed, -damage)",
            "Light (+speed, -damage)",
            "Heavy (-speed, +damage)",
            "Leadweight? (-speed, +damage)"
        };
        string[] _craftmanship = {
            "Use as prefixes?",
            "Terrible? (-all)",
            "Shoddy (-all)",
            "Bad? (-all)",
            "Good? (+all)",
            "Excellent (+all)",
            "Superb (+all)"
        };
        public void WeaponGen(General.Material mat)
        {
            Random rand = new Random();
            int weaponClass = rand.Next(0, 3);
            int weaponType = rand.Next(0, _weapons[weaponClass].Length);
            int weapon = rand.Next(0, _weapons[weaponClass][weaponType].Length);
            int firstEmptySlot = -1;
            firstEmptySlot = Array.IndexOf(Player.Instance.Inventory, "");
            if (firstEmptySlot == -1)
            {
                Console.WriteLine("Your inventory is full\n> Replace\n  Sell ({0} gold)");
            }
        }
    }
    class Armour
    {
        readonly string[] _armours = {
            "Chain",
            "Plate"
        };
        public void ArmourGen(General.Material mat)
        {
            Random rand = new Random();
            int armourClass = rand.Next(0, 3);
            int armour = rand.Next(0, _armours[armourClass].Length);
            int firstEmptySlot = -1;
            firstEmptySlot = Array.IndexOf(Player.Instance.Inventory, "");
            if (firstEmptySlot == -1)
            {
                Console.WriteLine("Your inventory is full\n> Replace\n  Sell ({0} gold)");
            }
        }
    }
}
