using System;
using System.Collections.Generic;

namespace GuardsOfAetheria
{
    internal class General
    {
        public enum Alloys : byte
        {
            //Brass?
        }

        public enum Material : byte
        {
            Copper,
            Iron,
            Steel
        }

        public enum MaterialMods : byte
        {
            Silvered,
            Gilded
        }

        public enum Rarity : byte
        {
            Common,
            Uncommon,
            Rare,
            Epic,
            Fabled,
            Mythical,
            Legendary,
            Supreme,
            Unique
        }

        public string[] RarityNames =
        {
            "Common",
            "Uncommon",
            "Rare",
            "Epic",
            "Fabled",
            "Mythical",
            "Legendary",
            "Supreme",
            "Unique"
        };

        public readonly string[][] Prefixes =
        {
            //split into categories?
            new[] //TODO: convert to xml?
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
    }

    internal class Weapon
    {
        public readonly string[][][] Weapons =
        {
            new[] //Melee
            {
                new[] //Sword
                {
                    "Broadsword",
                    "Shortsword",
                    "Dagger"
                },
                new[] //Axe
                {
                    "Axe"
                },
                new[] //Club
                {
                    "Club",
                    "Mace",
                    "Flail"
                },
                new[] //Polearm
                {
                    "Spear",
                    "Halberd?"
                }
            },
            new[] //Ranged
            {
                new[] //Throwing
                {
                    "Javelin"
                },
                new[] //Bow
                {
                    "Bow"
                }
            },
            new[] //Magic
            {
                new[] //Stick
                {
                    "Wand",
                    "Staff",
                    "Hand"
                }
            }
        };

        public readonly int[][][][] WeaponStats =
        {
            new[] //Melee
            {
                new[] //Swords
                {
                    new[] //Broadsword
                    {
                        1, //Damage to end
                        1, //Crit damage to vit
                        1 //Armour penetration
                    },
                    new[] //Shortsword
                    {
                        1
                    },
                    new[] //Dagger
                    {
                        1
                    }
                },
                new[] //Axes
                {
                    new[] //Axe
                    {
                        1
                    }
                },
                new[] //Clubs
                {
                    new[] //Club
                    {
                        1
                    },
                    new[] //Mace
                    {
                        1
                    },
                    new[] //Flail
                    {
                        1
                    }
                },
                new[] //Polearms
                {
                    new[] //Spear
                    {
                        1
                    },
                    new[] //Halberd?
                    {
                        1
                    }
                }
            },
            new[] //Ranged
            {
                new[] //Throwing
                {
                    new[] //Javelin
                    {
                        1
                    }
                },
                new[] //Bows
                {
                    new[] //Bow
                    {
                        1
                    }
                }
            },
            new[] //Magic
            {
                new[]
                {
                    new[] //Wand
                    {
                        1
                    },
                    new[] //Staff
                    {
                        1
                    },
                    new[] //Hand
                    {
                        1
                    }
                }
            }
        };

        public void WeaponGen(General.Material mat)
        {
            var general = new General();
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
            var spaceLeft = Utility.SpaceLeft();
            for (var i = 0; i < 50 && firstEmptySlot == -1; i++) if (Player.Instance.Inventory[i][1] == 0 && Player.Instance.Inventory[i][0] == 1) firstEmptySlot = i;
            //TODO: select where to put it
            if (spaceLeft < 1)
            {
                Console.WriteLine("Your inventory is full. You:");
                switch (new List<string> { "replace another item with this one", "discard this item" }.SelectOption())
                {
                    case 1: //Console.Clear();?
                        Player.Instance.InventoryName.SelectOption();
                        break;
                    case 2: //Do nothing
                        break;
                }
            }
            else
            {
                Player.Instance.Inventory[firstEmptySlot] = new[] { 1, weaponClass, weaponType, weapon, prefixRarity, prefixFinal, 0, 0 };
                Player.Instance.InventoryName[firstEmptySlot] = general.Prefixes[prefixRarity][prefixFinal] + Weapons[weaponClass][weaponType][weapon];
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

        private readonly int[] partSpaces = { 1, 4, 1, 2, 1 };

        public void ArmourGen(General.Material mat)
        {
            var general = new General();
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
            var prefixFinal = prefixNumber * 2 + rand.Next(0, 2);
            var prefix = general.Prefixes[prefixRarity][prefixFinal];
            Console.WriteLine("You found a {0} {1} {2}.", prefix, armourTypeName, armourPartName);
            var firstEmptySlot = -1;
            var spaceLeft = Utility.SpaceLeft();
            for (var i = 0; i < 50 && firstEmptySlot == -1; i++) if (Player.Instance.Inventory[i][1] == 0 && Player.Instance.Inventory[i][0] == 2) firstEmptySlot = i;
            if (spaceLeft < armourSpace)
            {
                Console.WriteLine("Your inventory is full. You:");
                switch (new List<string> { "replace another item with this one", "discard this item" }.SelectOption())
                {
                    case 1: //Console.Clear(); ?
                        Player.Instance.InventoryName.SelectOption();
                        break;
                    case 2: //Do nothing - the item is discarded, the inventory is unchanged :)
                        break;
                }
            }
            else
            {
                Player.Instance.Inventory[firstEmptySlot] = new[] { 2, armourType, armourPart, prefixRarity, prefixFinal, 0, 0, armourSpace };
            }
        }
    }
}