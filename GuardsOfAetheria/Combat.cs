using static GuardsOfAetheria.Toolbox;
namespace GuardsOfAetheria{
    using System;
    using System.Collections.Generic;
    using Improved.Consoles;
    internal class Combat{
        public List<Entity> Enemies;
        //CHEAT: everything
        public List<Entity> Party;
        public void LoadEntities(){
            //TODO: Location db [Scenarios] -> scenario db -> enemy db, add enemies/party, load+save party to json
            Party.Add(Bag.Player());
        }
        public void Fight(Entity attacker,Entity defender){
            //TODO: melee/magic/ranged effects, armour chance to hit - based on size?
            var armour=new Random().Next(8);//select - chance to hit
            var weapon=Consoles.Choose(attacker.Weapons[0].Name,attacker.Weapons[1].Name);
            var totalPenetration=attacker.Weapons[weapon].ArmourPenetration.Random();
            var totalArmour=defender.Armour[armour].ArmourToughness.Random();
            var totalDamage=attacker.Weapons[weapon].Attack.Random();
            var penetrationDamage=totalPenetration-totalArmour;
            var damageDealt=Math.Max(1,
                (attacker.Weapons[weapon].Attack.Random()+penetrationDamage)*totalDamage-
                    defender.Armour[armour].Defense.Random());
            if(penetrationDamage<0) Console.WriteLine(attacker.Name+"struck a glancing blow!");
            else Console.WriteLine(attacker.Name+"used a basic attack!");
            //TODO: never show numerical stats - text instead?
            defender.Bars.Vitality.Current-=damageDealt;
            //TODO: change attack/defence mechanics? change text, implement miss/dodge based on acc/eva
        }
        public void StartFight(){
            //TODO:select enemy to attack, test, select party targets/party auto, auto mode
            var enemyToAttack=Enemies[Consoles.Choose(Enemies.ConvertAll(e=>e.Name).ToArray())];
            Fight(Bag.Player(),enemyToAttack);
            //TODO: (party+)enemy AI? simple or not? do the reverse, loop until player/enemy dies
        }
        public void EndFight(){
            Bag.Player().Experience+=1;//TODO: read data from oledb (after creating enemy db), regen while out of fight, or insta-regen?
            Bag.Player().TryLevelUp();
        }
    }
}
