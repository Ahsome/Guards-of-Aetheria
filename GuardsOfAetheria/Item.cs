namespace GuardsOfAetheria{
    using System;
    using Improved.Consoles;
    public class Equipment:Item {}
    public enum ArmourType:byte{
        Helmet,
        Chestplate,
        Gauntlets,
        Greaves,
        Gloves,
        Boots,
        Shield
    }
    public class Weapon:Equipment{
        public Variable ArmourPenetration;
        public Variable Attack;
        public string AttackText;
        //details for weps: prefix, name, suffix, Variable DamageRange, same for armour
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
        public ArmourType Type;
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
        public Variable(int average,decimal minMultiplier,decimal maxMultiplier){
            Min=(int)(average*minMultiplier);
            Max=(int)(average*maxMultiplier);
        }
        public int Random =>new Random().Next(Min,Max+1);
    }
}
