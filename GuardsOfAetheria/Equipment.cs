/* using System;
using System.Collections.Generic;
using Improved;

namespace GuardsOfAetheria
{
    //TODO: move to item.cs, db-ify, complete refactor, enum to string, most efficient way - eqiupment, consumable, material, 
    public class General
    {
        public enum Alloy : byte { Brass }
        public enum Material : byte { Copper, Iron, Steel }
        public enum MaterialMod : byte { Silvered, Gilded }
        public enum Rarity : byte { Common, Uncommon, Rare, Epic, Fabled, Mythical, Legendary, Supreme, Unique }

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

        public readonly List<List<string>> Prefixes = new List<List<string>>
        {
            //split into categories?
            new List<string>
            {
                "Light (+speed, -damage)",
                "Heavy (-speed, +damage)",
                "Bad? (-all)",
                "Good? (+all)"
            },
            new List<string>
            {
                "Featherweight (+speed, -damage)",
                "Leadweight? (-speed, +damage)",
                "Shoddy (-all)",
                "Excellent (+all)"
            },
            new List<string>
            {
                "Terrible? (-all)",
                "Superb (+all)"
            }
        };
    }

    public class Weapon
    {
        public Classes WeaponClass;

        public enum Classes : byte { Melee, Ranged, Magic}
        public enum Types : byte { Sword, Axe, Club, Polearm, Throwing, Bow, Stick }
        public enum Melee : byte { Sword, Axe, Club, Polearm }
        public enum Ranged : byte { Throwing, Bow }
        public enum Magic : byte { Stick }

        public readonly List<List<List<string>>> Weapons = new List<List<List<string>>>
        {
            new List<List<string>> //Melee
            {
                new List<string> //Sword
                {
                    "Broadsword",
                    "Shortsword",
                    "Dagger"
                },
                new List<string> //Axe
                {
                    "Axe"
                },
                new List<string> //Club
                {
                    "Club",
                    "Mace",
                    "Flail"
                },
                new List<string> //Polearm
                {
                    "Spear",
                    "Halberd?"
                }
            },
            new List<List<string>> //Ranged
            {
                new List<string> //Throwing
                {
                    "Javelin"
                },
                new List<string> //Bow
                {
                    "Bow"
                }
            },
            new List<List<string>> //Magic
            {
                new List<string> //Stick
                {
                    "Wand",
                    "Staff",
                    "Hand"
                }
            }
        };

        public readonly List<List<List<List<int>>>> WeaponStats = new List<List<List<List<int>>>>
        {
            new List<List<List<int>>> //Melee
            {
                new List<List<int>> //Swords
                {
                    new List<int> //Broadsword
                    {
                        1, //Damage to end
                        1, //Crit damage to vit
                        1 //Armour penetration
                    },
                    new List<int> //Shortsword
                    {
                        1
                    },
                    new List<int> //Dagger
                    {
                        1
                    }
                },
                new List<List<int>> //Axes
                {
                    new List<int> //Axe
                    {
                        1
                    }
                },
                new List<List<int>> //Clubs
                {
                    new List<int> //Club
                    {
                        1
                    },
                    new List<int> //Mace
                    {
                        1
                    },
                    new List<int> //Flail
                    {
                        1
                    }
                },
                new List<List<int>> //Polearms
                {
                    new List<int> //Spear
                    {
                        1
                    },
                    new List<int> //Halberd?
                    {
                        1
                    }
                }
            },
            new List<List<List<int>>> //Ranged
            {
                new List<List<int>> //Throwing
                {
                    new List<int> //Javelin
                    {
                        1
                    }
                },
                new List<List<int>> //Bows
                {
                    new List<int> //Bow
                    {
                        1
                    }
                }
            },
            new List<List<List<int>>> //Magic
            {
                new List<List<int>>
                {
                    new List<int> //Wand
                    {
                        1
                    },
                    new List<int> //Staff
                    {
                        1
                    },
                    new List<int> //Hand
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
            var weaponType = rand.Next(0, Weapons[weaponClass].Count);
            var weapon = rand.Next(0, Weapons[weaponClass][weaponType].Count);
            var weaponName = Weapons[weaponClass][weaponType][weapon];
            var prefixRarity = Convert.ToInt16(Math.Round(Math.Log10(rand.Next(0, 10001))));
            var prefixNumber = rand.Next(0, general.Prefixes[prefixRarity].Count / 2);
            var prefixFinal = prefixNumber * 2 + rand.Next(0, 2);
            var prefix = general.Prefixes[prefixRarity][prefixFinal];
            Console.WriteLine("You found a {0} {1}.", prefix, weaponName);
            var firstEmptySlot = -1;
            var spaceLeft = Player.SpaceLeft();
            for (var i = 0; i < 50 && firstEmptySlot == -1; i++)
                if (Player.Instance.Inventory[i][1] == 0 && Player.Instance.Inventory[i][0] == 1) firstEmptySlot = i;
            //TODO: select where to put it
            if (spaceLeft < 1)
            {
                Console.WriteLine("Your inventory is full. You:");
                switch (new List<string> { "replace another item with this one", "discard this item" }.Select())
                {
                    case 1: //Console.Clear();?
                        Player.Instance.InventoryName.Select();
                        break;
                    case 2: //Do nothing
                        break;
                }
            }
            else
            {
                Player.Instance.Inventory[firstEmptySlot] = new List<int>
                {
                    1,
                    weaponClass,
                    weaponType,
                    weapon,
                    prefixRarity,
                    prefixFinal,
                    0,
                    0
                };
                Player.Instance.InventoryName[firstEmptySlot] = general.Prefixes[prefixRarity][prefixFinal] +
                                                                Weapons[weaponClass][weaponType][weapon];
            }
        }
    }
    //TODO: use same RNG
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
            //var armour = rand.Next(0, armours[armourClass].Count);
            //var armourName = armours[armour];
            var armourType = rand.Next(0, 2);
            var armourTypeName = armours[armourType];
            var armourPart = rand.Next(0, 5);
            var armourPartName = parts[armourPart];
            var armourSpace = partSpaces[armourPart];
            var prefixRarity = Convert.ToInt16(Math.Round(Math.Log10(rand.Next(0, 10001))));
            var prefixNumber = rand.Next(0, general.Prefixes[prefixRarity].Count / 2);
            var prefixFinal = prefixNumber * 2 + rand.Next(0, 2);
            var prefix = general.Prefixes[prefixRarity][prefixFinal];
            Console.WriteLine("You found a {0} {1} {2}.", prefix, armourTypeName, armourPartName);
            var firstEmptySlot = -1;
            var spaceLeft = Player.SpaceLeft();
            for (var i = 0; i < 50 && firstEmptySlot == -1; i++)
                if (Player.Instance.Inventory[i][1] == 0 && Player.Instance.Inventory[i][0] == 2) firstEmptySlot = i;
            if (spaceLeft < armourSpace)
            {
                Console.WriteLine("Your inventory is full. You:");
                switch (new List<string> { "replace another item with this one", "discard this item" }.Select())
                {
                    case 1: //Console.Clear(); ?
                        Player.Instance.InventoryName.Select();
                        break;
                    case 2: //Do nothing - the item is discarded, the inventory is unchanged :)
                        break;
                }
            }
            else
                Player.Instance.Inventory[firstEmptySlot] = new List<int>
                {
                    2,
                    armourType,
                    armourPart,
                    prefixRarity,
                    prefixFinal,
                    0,
                    0,
                    armourSpace
                };
        }
    }
} */