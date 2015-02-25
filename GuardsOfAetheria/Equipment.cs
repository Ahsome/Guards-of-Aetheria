using System;

namespace GuardsOfAetheria
{
    class General
    {
        readonly public string[][] Prefixes = { //split into categories?
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
        readonly public string[][][] Weapons = {
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
        public void WeaponGen(General.Material mat)
        {
            var general = new General();
            var utility = new Utility();
            var rand = new Random();
            var weaponClass = rand.Next(0, 3);
            var weaponType = rand.Next(0, Weapons[weaponClass].Length);
            var weapon = rand.Next(0, Weapons[weaponClass][weaponType].Length);
            var weaponName = Weapons[weaponClass][weaponType][weapon];
            var prefixRarity = Convert.ToInt16(Math.Round(Math.Log10(rand.Next(0, 10001))));
            var prefixNumber = rand.Next(0, general.Prefixes[prefixRarity].Length / 2);
            var prefixFinal = prefixNumber * 2 + rand.Next(0, 2);
            var prefix = general.Prefixes[prefixRarity][prefixFinal];
            Console.WriteLine("You found a {0} {1}.", prefix, weaponName);
            var firstEmptySlot = -1;
            for (var i = 1; i < 51; i++)
            {
                if(Player.Instance.Inventory[1][i][1] == 0)
                {
                    firstEmptySlot = i;
                }
            }
            if (firstEmptySlot == -1)
            {
                Console.WriteLine("Your inventory is full. You:");
                string[] options = { "replace another item with this one", "discard this item" };
                int menuSelected = utility.SelectOption(options);
                switch (menuSelected)
                {
                    case 1: //Console.Clear(); ?
                        utility.SelectOption(Player.Instance.InventoryNameAll[]);
                        //InventorySelect(); (use InventoryView();)
                        break;
                    case 2: //Do nothing - the item is discarded, the inventory is unchanged :)
                        break;
                }
            }
            else
            {
                Player.Instance.Inventory[1][firstEmptySlot] = new[]
            {
                weaponClass, weaponType, weapon, prefixRarity, prefixFinal, 0, 0
            };
                Player.Instance.InventoryName[1][firstEmptySlot] = general.Prefixes[prefixRarity][prefixFinal] +
                                                              Weapons[weaponClass][weaponType][weapon];
            }
        }
    }

    internal class Armour
    {
        private readonly string[] armours =
        {
            "Chain",
            "Plate"
        };

        private readonly string[] parts =
        {
            "Helmet",
            "Chestplate",
            "Gloves",
            "Leggings",
            "Boots"
        };

        private readonly int[] partSpaces = 
        {
            1, 4, 1, 2, 1
        };
        public void ArmourGen(General.Material mat)
        {
            var general = new General();
            var utility = new Utility();
            var rand = new Random();
            //var armourClass = rand.Next(0, 3);
            //var armour = rand.Next(0, armours[armourClass].Length);
            //var armourName = armours[armour];
            var armourType = rand.Next(0, 2);
            var armourTypeName = armours[armourType];
            var armourPart = rand.Next(0, 5);
            var armourPartName = parts[armourPart];
            var armourSpace = partSpaces[armourPart];
            var prefixRarity = Convert.ToInt16(Math.Round(Math.Log10(rand.Next(0, 10001))));
            var prefixNumber = rand.Next(0, general.Prefixes[prefixRarity].Length / 2);
            var prefixFinal = prefixNumber*2 + rand.Next(0, 2);
            var prefix = general.Prefixes[prefixRarity][prefixFinal];
            Console.WriteLine("You found a {0} {1} {2}.", prefix, armourTypeName, armourPartName);
            var firstEmptySlot = -1;
            var spaceLeft = utility.SpaceLeft();
            for (var i = 1; i < 51; i++)
            {
                if (Player.Instance.Inventory[2][i][1] == 0)
                {
                    if (firstEmptySlot == -1)
                    {
                        firstEmptySlot = i;
                        break;
                    }
                    if (firstEmptySlot != -1) { break; }
                }
                if (firstEmptySlot != -1) { break; }
            }
            if (spaceLeft == 0)
            {
                Console.WriteLine("Your inventory is full. You:");
                string[] options = {"replace another item with this one", "discard this item"};
                int menuSelected = utility.SelectOption(options);
                switch (menuSelected)
                {
                    case 1:
                        //InventorySelect(); (use InventoryView();)
                        break;
                    case 2: //Do nothing :)
                        break;
                }
            }
            else
            {
                Player.Instance.Inventory[2][firstEmptySlot] = new[]
            {
                armourType, armourPart, prefixRarity, prefixFinal, 0, 0, armourSpace
            };
            }
        }
    }
}
