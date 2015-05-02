using System;
using System.Collections.Generic;
using static System.Console;

namespace GuardsOfAetheria
{
    public abstract class Item
    {
        public string Name;
        public int Index;
        public int Amount;
        public int Cost;
        public int Value;
    }

    public class Equipment : Item
    {

    }

    public class Weapon : Equipment
    {
        public Variable Attack;
        public string AttackText;
        public Variable ArmourPenetration;

        //details for weps: prefix, name, suffix, avg damage, damage%
        //same for armour
        
        public void GetWeaponData()
        {
            Clear();
            //TODO: info screen
            var arrays = new Dictionary<string, string[]>
            {
                {"Materials", new[]{""}}
            };
            var strings = new Dictionary<string, object>
            {
                {"Material Name", ""}
            };
            Database.GetData("SELECT [Material Name] FROM Materials", strings, arrays);
            Database.GetData($"SELECT * FROM Materials WHERE [Material Name] = {Player.Instance.RoomId}", strings, arrays);
            //TODO: max gems = rarity id - 1? or rarity id/2?
        } //In weaponrack/armourrack - (cart)
        //CreateWeapon() (custom) for starting?
    }

    public class Armour : Equipment
    {
        public Variable Defense;
        public Variable ArmourToughness;
    }

    public class Small : Item //rename
    {
        public int MaximumStack;
    }

    public class Gem : Small
    {
        //read from db, atts.split, dictionary, in GemPouch
    }

    public class Material : Item
    {
        public int MaximumStack;
    }

    public class Variable
    {
        public int Min;
        public int Max;

        public Variable(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public int Random() { return new Random().Next(Min, Max + 1); }
    }
}
