using System;
using System.Collections.Generic;
using Improved.Consoles;

namespace GuardsOfAetheria
{
    internal class Combat
    {
        //CHEAT: everything
        public List<Entity> Party;
        public List<Entity> Enemies; 

        public void LoadEntities()
        {
            //TODO: Location db [Scenario List] -> scenario db -> enemy db
            Party.Add(Player.Instance);
            for (var i = 1; i < 7; i++)
            {
                //party
                Console.WriteLine();
            }
            for (var i = 7; i < 13; i++)
            {
                //enemies
                Console.WriteLine();
            }
        }

        public void Fight(Entity attacker, Entity defender)
        {
            //TODO BLOCK: melee/magic/ranged effects, armour chance to hit - based on size?
            var armour = new Random().Next(8); //select - chance to hit
            var weapon = new[] { attacker.Weapons[0].Name, attacker.Weapons[1].Name }.Choose();
            var totalPenetration = attacker.Weapons[weapon].ArmourPenetration.Random();
            var totalArmour = defender.Armour[armour].ArmourToughness.Random();
            var totalDamage = attacker.Weapons[weapon].Attack.Random();
            var penetrationDamage = totalPenetration - totalArmour;
            var damageDealt = Math.Max(1,
                (attacker.Weapons[weapon].Attack.Random() + penetrationDamage) * totalDamage - defender.Armour[armour].Defense.Random());
            if (penetrationDamage < 0) Console.WriteLine(attacker.Name + "struck a glancing blow!");
            else Console.WriteLine(attacker.Name + "used a basic attack!");
            //instead of hp: you have several minor cuts and bruises, you have a fatal wound
            defender.Bars.Vitality.Current -= damageDealt;
            //change attack/defence mechanics, change text, implement misses (accuracy stat)/dodge (evasion stat)
        }

        public void StartFight()
        {
            //TODO BLOCK: select enemy to attack - do the same for party? or party autoattack?
            //test
            var enemyToAttack = Enemies[Enemies.ConvertAll(T => T.Name).ToArray().Choose()];
            Fight(Player.Instance, enemyToAttack);
            //party + enemy AI - consider strengths, hp left etc
            //do the reverse
            //loop until player/enemy dies
        }

        public void EndFight()
        {
            Player.Instance.Experience += 1; //read data from oledb (after creating enemy db)
            Player.Instance.TryLevelUp();
            //regen while out of fight?
        }
    }
}
