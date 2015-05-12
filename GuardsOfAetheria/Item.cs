namespace GuardsOfAetheria{
    using System;
    using Improved.Consoles;
    public class Equipment:Item {}
    public class Helmet:Equipment {}
    public class Chestplate:Equipment {}
    public class Gauntlets:Equipment {}
    public class Greaves:Equipment {}
    public class Gloves:Equipment {}
    public class Boots:Equipment {}
    public class Weapon:Equipment{
        public Variable ArmourPenetration;
        public Variable Attack;
        public string AttackText;
        //details for weps: prefix, name, suffix, avg damage, damage%, same for armour
        public void GetWeaponData(){
            //TODO: info screen
            //var data = "SELECT [Material Name] FROM Materials".GetData(new[] {"Materials"}, new[] {"Material Name"});
            //var data2 = $"SELECT * FROM Materials WHERE [Material Name] = {Bag.Instance.Player().RoomId}".GetData(new[] { "Materials" }, null);
            //TODO: max gems = rarity id - 1? or rarity id/2?
        }//In weaponrack/armourrack - (cart)
    }
    public class Armour:Equipment{
        public Variable ArmourToughness;
        public Variable Defense;
    }

    //rename
    public class Small:Item{
        public int MaximumStack;
    }
    public class Gem:Small {}
    public class Material:Item{
        public int MaximumStack;
    }
    public class Variable{
        public int Max;
        public int Min;
        public Variable(int min,int max){
            Min=min;
            Max=max;
        }
        public int Random() {return new Random().Next(Min,Max+1);}
    }
}
