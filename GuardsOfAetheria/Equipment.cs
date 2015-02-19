using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
    class Weapon
    {
        string[][][] Weapons = new string[][][]
    {
        new string[][]
        {
            new string[]
            {
                "Broadsword",
                "Shortsword"
            },
            new string[]
            {
                "Axe"
            },
            new string[]
            {
                "Club",
                "Mace",
                "Flail"
            },
            new string[]
            {
                "Spear",
                "Halberd?",
            }
        },
        new string[][]
        {
            new string[]
            {
                "Javelin"
            },
            new string[]
            {
                "Bow"
            }
        },
        new string[][]
        {
            new string[]
            {
                "Wand",
                "Staff",
                "Tome"
            }
        }
    };
        string[] Prefixes = new string[]
        {
            "Featherweight (+speed, -damage)",
            "Light (+speed, -damage)",
            "Heavy (-speed, +damage)",
            "Leadweight? (-speed, +damage)",
        };
        string[] Craftmanship = new string[]
        {
            "Use as prefixes?",
            "Terrible? (-all)",
            "Shoddy (-all)",
            "Bad? (-all)",
            "Good? (+all)",
            "Excellent (+all)",
            "Superb (+all)"
        };
        public enum Material
        {
            Copper,
            Iron,
            Steel
        }
        public void WeaponGen(Material mat)
        {
            Random rand = new Random();
            int weaponClass = rand.Next(0, 3);
            int weaponType = rand.Next(0, Weapons[weaponClass].Length);
            int weapon = rand.Next(0, Weapons[weaponClass][weaponType].Length);
            int firstEmptySlot = -1;
            firstEmptySlot = Array.IndexOf(Player.Instance.inventory, "");
            if (firstEmptySlot == -1)
            {
                Console.WriteLine("Your inventory is full\n> Replace\n  Sell ({0} gold)");
            }
        }
    }
    class Armour
    {
        string[] Armours = new string[]
        {
            "Chain",
            "Plate"
        };
        public enum Material
        {
            Leather,
            Copper,
            Iron,
            Steel
        }
        public void ArmourGen(Material mat)
        {
            Random rand = new Random();
            int armourClass = rand.Next(0, 3);
            int armour = rand.Next(0, Armours[armourClass].Length);
            int firstEmptySlot = -1;
            firstEmptySlot = Array.IndexOf(Player.Instance.inventory, "");
            if (firstEmptySlot == -1)
            {
                Console.WriteLine("Your inventory is full\n> Replace\n  Sell ({0} gold)");
            }
        }
    }
}
