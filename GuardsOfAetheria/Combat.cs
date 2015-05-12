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
            //TODO: Location db [Scenarios] -> scenario db -> enemy db, add enemies/party
            Party.Add(Bag.Player());
        }
        public void Fight(Entity attacker,Entity defender){
            //TODO BLOCK: melee/magic/ranged effects, armour chance to hit - based on size?
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
            //instead of hp: you have several minor cuts and bruises, you have a fatal wound
            defender.Bars.Vitality.Current-=damageDealt;
            //change attack/defence mechanics, change text, implement misses (accuracy stat)/dodge (evasion stat)
        }
        public void StartFight(){
            //select enemy to attack - do the same for party? or party autoattack? test below
            var enemyToAttack=Enemies[Consoles.Choose(Enemies.ConvertAll(e=>e.Name).ToArray())];
            Fight(Bag.Player(),enemyToAttack);
            //party + enemy AI - consider strengths, hp left etc, do the reverse, loop until player/enemy dies
        }
        public void EndFight(){
            Bag.Player().Experience+=1;//read data from oledb (after creating enemy db)
            Bag.Player().TryLevelUp();
            //regen while out of fight?
        }
    }
}
