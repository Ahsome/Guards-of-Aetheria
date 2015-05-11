using System;
using Improved.Consoles;

namespace GuardsOfAetheria {
    public class Equipment:Item {

    }

    public class Helmet:Equipment {

    }

    public class Chestplate:Equipment {

    }

    public class Gauntlets:Equipment {

    }

    public class Greaves:Equipment {

    }

    public class Gloves:Equipment {

    }

    public class Boots:Equipment {

    }


    public class Weapon:Equipment {
        public Variable Attack;
        public string AttackText;
        public Variable ArmourPenetration;

        //details for weps: prefix, name, suffix, avg damage, damage%
        //same for armour

        public void GetWeaponData() {
            //TODO: info screen
            //var data = "SELECT [Material Name] FROM Materials".GetData(new[] {"Materials"}, new[] {"Material Name"});
            //var data2 = $"SELECT * FROM Materials WHERE [Material Name] = {Bag.Instance.Player().RoomId}".GetData(new[] { "Materials" }, null);
            //TODO: max gems = rarity id - 1? or rarity id/2?
        } //In weaponrack/armourrack - (cart)
        //CreateWeapon() (custom) for starting?
    }

    public class Armour:Equipment {
        public Variable Defense;
        public Variable ArmourToughness;
    }

    public class Small:Item //rename
    {
        public int MaximumStack;
    }

    public class Gem:Small {
        //read from db, atts.split, dictionary, in GemPouch
    }

    public class Material:Item {
        public int MaximumStack;
    }

    public class Variable {
        public int Min;
        public int Max;

        public Variable(int min,int max) {
            Min=min;
            Max=max;
        }

        public int Random() { return new Random().Next(Min,Max+1); }
    }
}
