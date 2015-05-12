using static Improved.Consoles.Consoles;
using static System.ConsoleColor;
using static GuardsOfAetheria.Toolbox;
namespace GuardsOfAetheria{
    using System;
    using System.Collections.Generic;
    using Improved;
    using Improved.Consoles;
    public class EquipmentSet{
        public Boots Boots;
        public Chestplate Chestplate;
        public Gauntlets Gauntlets;
        public Gloves Gloves;
        public Greaves Greaves;
        public Helmet Helmet;
        public Weapon Weapon;
    }
    public class Bar:ProgressBar{
        public int Unbuffed;
        public Bar() {}
        public Bar(int unbuffed) {Unbuffed=Current=Maximum=unbuffed;}
        public Bar(int unbuffed,int current,int maximum){
            Unbuffed=unbuffed;
            Current=current;
            Maximum=maximum;
        }
    }
    public class Bars{
        public Bar Endurance=new Bar{InitialColour=DarkYellow,NewColour=Yellow};
        public Bar Mana=new Bar{InitialColour=DarkBlue,NewColour=Blue};
        public Bar Stamina=new Bar{InitialColour=DarkGreen,NewColour=DarkGreen};
        public Bar Vitality=new Bar{InitialColour=DarkRed,NewColour=Red};
    }
    public enum Class:byte{
        Melee,
        Magic,
        Ranged
    }
    public class Attributes{
        public int Strength{get;set;}
        public int Dexterity{get;set;}
        public int Wisdom{get;set;}
        public int Primary{get;set;}
        public int Secondary{get;set;}
        public int Tertiary{get;set;}
        public int Points{get;set;}
    }
    public class Entity{
        public List<Armour> Armour;
        public Attributes Attributes=new Attributes();
        public Bars Bars=new Bars();
        public Class Class;
        public string Name;
        //restrict to 2? or will there be octopus enemies?
        public List<Weapon> Weapons;
        public void Assign(){
            switch(Class){
                case Class.Melee:
                    Attributes.Primary=Attributes.Strength;
                    Attributes.Secondary=Attributes.Wisdom;
                    Attributes.Tertiary=Attributes.Dexterity;
                    break;
                case Class.Magic:
                    Attributes.Tertiary=Attributes.Strength;
                    Attributes.Primary=Attributes.Wisdom;
                    Attributes.Secondary=Attributes.Dexterity;
                    break;
                case Class.Ranged:
                    Attributes.Secondary=Attributes.Strength;
                    Attributes.Tertiary=Attributes.Wisdom;
                    Attributes.Primary=Attributes.Dexterity;
                    break;
            }
        }
        public void Update(){
            switch(Class){
                case Class.Melee:
                    Attributes.Strength=Attributes.Primary;
                    Attributes.Wisdom=Attributes.Secondary;
                    Attributes.Dexterity=Attributes.Tertiary;
                    break;
                case Class.Magic:
                    Attributes.Strength=Attributes.Tertiary;
                    Attributes.Wisdom=Attributes.Primary;
                    Attributes.Dexterity=Attributes.Secondary;
                    break;
                case Class.Ranged:
                    Attributes.Strength=Attributes.Secondary;
                    Attributes.Wisdom=Attributes.Tertiary;
                    Attributes.Dexterity=Attributes.Primary;
                    break;
            }
        }
    }
    public class Stats{
        public int Attack{get;set;}
        public int Defence{get;set;}
        public int Shield{get;set;}// Magic Resist
        public int Accuracy{get;set;}
        public int Evasion{get;set;}
        public int Luck{get;set;}
        public int Perception{get;set;}
    }
    public enum Origin:byte{
        Nation,
        Treaty,
        Refugee
    }
    public enum Type:byte{
        Weapon,
        Armour,
        Comsumable,
        Material
    }
    public class Player:Entity{
        public Origin Origin{get;set;}
        public int Experience{get;set;}
        public int Level{get;set;}
        public int RoomId{get;set;}
        public int PlotId{get;set;}
        public int InventorySpace{get;set;}
        //TODO BLOCK: Inventory stuff
        //Compartments, large compartments
        //subtract inventoryspace once item is bought/sold
        //make compartments less accessible depending on stuff e.g. being in combat/leaving them behind (at home)
        public List<Item> Inventory{get;set;}
        public EquipmentSet Equipped{get;set;}
        public void UpdateBars(){
            Bars.Endurance.Unbuffed=50+Attributes.Strength*5+Level*5;
            Bars.Mana.Unbuffed=50+Attributes.Wisdom*5+Level*5;
            Bars.Stamina.Unbuffed=50+Attributes.Dexterity*5+Level*5;
            Bars.Vitality.Unbuffed=(int)(10+Bars.Endurance.Unbuffed*0.01);//TODO: buffed stats
        }
        public void Equip(int invIndex){
            var item=Inventory[invIndex];//TODO: test
            if(item is Helmet) Equipped.Helmet=item as Helmet;
            else if(item is Chestplate) Equipped.Chestplate=item as Chestplate;
            else if(item is Gauntlets) Equipped.Gauntlets=item as Gauntlets;
            else if(item is Greaves) Equipped.Greaves=item as Greaves;
            else if(item is Gloves) Equipped.Gloves=item as Gloves;
            else if(item is Boots) Equipped.Boots=item as Boots;
            else return;
            Inventory[invIndex]=default(Item);
        }
        public static void SortInventory(){
            throw new NotImplementedException();
            //label by importance, .Aggregate() by importance, Quicksort;
        }
        public void TryLevelUp(){
            var expNeeded=(int)Math.Pow(1.05,Level)*1000;
            if(Experience>=expNeeded) Level++;
            Experience-=expNeeded;
        }
        public void ShowMenu(){
            throw new NotImplementedException();
            //"Inventory".CWrite();
            //(from i in Inv select i.Name).ToArray().Choose();
            //Add more options, centre text, SelectContinue
        }
    }
    internal class CharacterCreation{
        public static void Create(){
            //TODO: more extensive character creation, notify player
            "Your name is ".CWrite();
            Bag.Player().Name=CustomIo.ReadLine(Lists.Dict(Lists.Kvp(1,Str.NonLetters)));
            "You come from\n".CWrite();
            Bag.Player().Origin=
                (Origin)
                    Choose("an average house in the safe provinces, loyal to the king",
                        "an average house in a war-torn province, loyal to your lord",//TODO: find correct title
                        "a refugee tent in a war-torn province, loyal to nobody");
            #region Choose Class
            "You are\n".CWrite();
            var options=new string[3];
            switch(Bag.Player().Origin){
                case Origin.Nation:
                    options=new[]{
                        "a skilled warrior, able to knock back a training dummy 10 metres with one blow",
                        "a skilled archer, able to hit a training dummy's heart from 100 metres away",
                        "a skilled mage, able to burn training dummies to a crisp in 10 seconds flat"
                    };
                    break;
                case Origin.Treaty:
                    options=new[]{
                        "a warrior, able to knock back an invader 10 metres with one blow",
                        "an archer, able to hit an invader's heart from 100 metres away",
                        "a mage, able to burn invaders to a crisp in 10 seconds flat"
                    };
                    break;
                //TODO: rename invaders, and king maybe, and move to json - var dict = Dict(new[]{""}); Json.Load(out dict);
                case Origin.Refugee:
                    options=new[]{
                        "a born warrior, able to knock a sack of potatoes 10 metres back with one blow",
                        "a born archer, able to hit a bullseye from 100 metres away",
                        "a born mage, able to burn a tree in 10 seconds flat"
                    };
                    break;
            }
            Bag.Player().Class=(Class)Choose(options);
            #endregion
            //TODO: AssignStartingEquipment();
            Bag.Player().Attributes.Primary=13;
            Bag.Player().Attributes.Secondary=10;
            Bag.Player().Attributes.Tertiary=7;
            Bag.Player().Update();
            int attPoints;
            WordWrap("Set your attributes manually. Points left are indicated below.");
            var permanentPoints=Spend(new[]{"You have "," points"," left to use"},
                new[]{
                    new Item("Strength",Bag.Player().Attributes.Strength,1),
                    new Item("Dexterity",Bag.Player().Attributes.Dexterity,1),
                    new Item("Wisdom",Bag.Player().Attributes.Wisdom,1)
                },16,out attPoints);
            Bag.Player().Attributes.Strength=permanentPoints[0].Amount;
            Bag.Player().Attributes.Dexterity=permanentPoints[1].Amount;
            Bag.Player().Attributes.Wisdom=permanentPoints[2].Amount;
            Bag.Player().Attributes.Points=attPoints;
            Bag.Player().RoomId=1;
        }
    }
}
