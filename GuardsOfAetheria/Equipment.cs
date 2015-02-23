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
            Fabled,
            Mythical,
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
        readonly string[] weaponClassName = {
            "Melee",
            "Ranged",
            "Magic"
        };
        readonly string[][] weaponTypeName = {
        new[]
        {
            "Sword",
            "Axe",
            "Club",
            "Polearm"
        },
        new []
        {
            "Javelin",
            "Bow",
        },
        new[]
        {
            "Catalyst"
        }
        };
        readonly string[][][] weapons = {
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
        readonly string[][] prefixes = { //split?
            new[]
            {
            "Light (+speed, -damage)",
            "Heavy (-speed, +damage)",
            
            "Bad? (-all)",
            "Good? (+all)"
            },
            new[]
            {
                "Featherweight (+speed, -damage)",
                "Leadweight? (-speed, +damage)",
                "Shoddy (-all)",
                "Excellent (+all)"
            },
            new[]
            {
                "Terrible? (-all)",
                "Superb (+all)"
            }
        };
        public void WeaponGen(General.Material mat)
        {
            var rand = new Random();
            var weaponClass = rand.Next(0, 3);
            var weaponType = rand.Next(0, weapons[weaponClass].Length);
            var weapon = rand.Next(0, weapons[weaponClass][weaponType].Length);
            var weaponName = weapons[weaponClass][weaponType][weapon];
            var prefixRarity = Convert.ToInt16(Math.Round(Math.Log10(rand.Next(0, 10001))));
            var prefixGoodOrBad = rand.Next(0, 2);
            var prefixNumber = rand.Next(0, prefixes[prefixRarity].Length / 2);
            var prefix = prefixes[prefixRarity][prefixNumber * 2 + prefixGoodOrBad];
            Console.WriteLine("You found a {0} {1}.", prefix, weaponName);
            var firstEmptySlot = Array.IndexOf(Player.Instance.Weapons, 0);
            if (firstEmptySlot == -1)
            {
                Console.WriteLine("Your inventory is full\n> Replace\n  Discard");
            }
            Player.Instance.Weapons[firstEmptySlot] = new[]
            {
                weaponClass, weaponType, weapon, prefixRarity, prefixNumber * 2 + prefixGoodOrBad, 0, 0
            };
        }
    }
    class Armour
    {
        readonly string[] armours = {
            "Chain",
            "Plate"
        };
        public void ArmourGen(General.Material mat)
        {
            Random rand = new Random();
            int armourClass = rand.Next(0, 3);
            int armour = rand.Next(0, armours[armourClass].Length);
            int firstEmptySlot = Array.IndexOf(Player.Instance.Armours, 0);
            if (firstEmptySlot == -1)
            {
                Console.WriteLine("Your inventory is full\n> Replace\n  Sell ({0} gold)");
            }
        }
    }
}
