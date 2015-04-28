using System;
using System.Collections.Generic;
using Improved;

namespace GuardsOfAetheria
{
    internal class Combat
    {
        //CHEAT: everything
        public List<Entity> Entities;

        public void LoadEntities()
        {
            //TODO: Location db [Scenario List] -> scenario db -> enemy db
            Entities.Add(Player.Instance);
            for (var i = 1; i < 7; i++)
            {
                //party
                Console.Write("Prevent Resharper from deleting");
            }
            for (var i = 7; i < 13; i++)
            {
                //enemies
                Console.Write("Prevent Resharper from deleting");
            }
        }

        public void Fight(Entity attacker, Entity defender)
        {
            //TODO BLOCK: melee/magic/ranged effects, armour chance to hit - based on size?
            var armour = new Random().Next(0,8);
            var weapon = new List<string> { attacker.Weapons[0].Name, attacker.Weapons[1].Name }.Select();
            var totalPenetration = (attacker.Weapons[weapon].ArmourPenetration.Random());
            var totalArmour = defender.Armour[armour].ArmourToughness.Random();
            var totalDamage = attacker.Weapons[weapon].Attack.Random();
            var penetrationDamage = totalPenetration - totalArmour;
            var damageDealt = Math.Max(1,
                (attacker.Weapons[weapon].Attack.Random() + penetrationDamage) * totalDamage - defender.Armour[armour].Defense.Random());
            if (penetrationDamage < 0) Console.WriteLine(attacker.Name + "struck a glancing blow!");
            else Console.WriteLine(attacker.Name + "used a basic attack!");
            //instead of hp: you have several minor cuts and bruises, you have a fatal wound
            defender.Vitality.Current -= damageDealt;
            //change attack/defence mechanics, change text, implement misses (accuracy stat)/dodge (evasion stat)
        }

        public void StartFight()
        {
            //TODO BLOCK: select enemy to attack - do the same for party? or party autoattack?
            var enemyToAttack = new List<string>();
            for (var i = 6; i < 13 && Entities[i] != null; i++) enemyToAttack.Add(Entities[i].Name);
            // .where?
            var partySelected = enemyToAttack.Select(); //wait, partyselected?
            //Fight();
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
