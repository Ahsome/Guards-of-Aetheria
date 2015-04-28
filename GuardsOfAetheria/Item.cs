using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace GuardsOfAetheria
{
    public class Item
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
            Console.Clear();
            //TODO: info screen
            var lists = new Dictionary<string, List<string>>
            {
                {"Materials", new List<string>()}
            };
            var strings = new Dictionary<string, object>
            {
                {"Material Name", ""}
            };
            Database.GetData(new OleDbCommand(String.Format("SELECT [Material Name] FROM Materials"), Database.DatabaseConnection), strings, lists);
            Database.GetData(new OleDbCommand(String.Format("SELECT * FROM Materials WHERE [Material Name] = {0}", Player.Instance.RoomId), Database.DatabaseConnection), strings, lists);
            //TODO: max gems = material id - 1? or material id/2?
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
        
    }

    public class Gem : Small
    {
        //read froom db, like weapons, atts.split, dictionary, in GemPouch
    }

    public class Material : Item
    {
        
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
