using System;

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
            var utility = new Utility();
            var rand = new Random();
            var weaponClass = rand.Next(0, 3);
            var weaponType = rand.Next(0, Weapons[weaponClass].Length);
            var weapon = rand.Next(0, Weapons[weaponClass][weaponType].Length);
            var weaponName = Weapons[weaponClass][weaponType][weapon];
            var prefixRarity = Convert.ToInt16(Math.Round(Math.Log10(rand.Next(0, 10001))));
            var prefixNumber = rand.Next(0, general.Prefixes[prefixRarity].Length/2);
            var prefixFinal = prefixNumber*2 + rand.Next(0, 2);
            var prefix = general.Prefixes[prefixRarity][prefixFinal];
            Console.WriteLine("You found a {0} {1}.", prefix, weaponName);
            var firstEmptySlot = -1;
            var spaceLeft = utility.SpaceLeft();
            for (var i = 1; i < 51; i++)
            {
                if (Player.Instance.Inventory[1][i][1] == 0)
                {
                    firstEmptySlot = i;
                    break;
                }
                if (firstEmptySlot != -1)
                {
                    break;
                }
            }
            if (firstEmptySlot == -1 || spaceLeft < 1)
            {
                Console.WriteLine("Your inventory is full. You:");
                string[] options = {"replace another item with this one", "discard this item"};
                var menuSelected = utility.SelectOption(options);
                switch (menuSelected)
                {
                    case 1: //Console.Clear(); ?
                        utility.SelectOption(Player.Instance.InventoryNameAll);
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
            var prefixNumber = rand.Next(0, general.Prefixes[prefixRarity].Length/2);
            var prefixFinal = prefixNumber*2 + rand.Next(0, 2);
            var prefix = general.Prefixes[prefixRarity][prefixFinal];
            Console.WriteLine("You found a {0} {1} {2}.", prefix, armourTypeName, armourPartName);
            var firstEmptySlot = -1;
            var spaceLeft = utility.SpaceLeft();
            for (var i = 0; i < 50; i++)
            {
                if (Player.Instance.Inventory[2][i][1] == 0)
                {
                    firstEmptySlot = i;
                    break;
                }
                if (firstEmptySlot != -1)
                {
                    break;
                }
            }
            if (spaceLeft == 0)
            {
                Console.WriteLine("Your inventory is full. You:");
                string[] options = {"replace another item with this one", "discard this item"};
                var menuSelected = utility.SelectOption(options);
                switch (menuSelected)
                {
                    case 1: //Console.Clear(); ?
                        utility.SelectOption(Player.Instance.InventoryNameAll);
                        break;
                    case 2: //Do nothing - the item is discarded, the inventory is unchanged :)
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