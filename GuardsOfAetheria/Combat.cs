using System;
using System.Collections.Generic;

namespace GuardsOfAetheria
{
    internal class Combat
    {
        //CHEAT: everything
        public List<int> CurrentVitality { get; set; }
        public List<int> CurrentMana { get; set; }
        public List<int> CurrentEndurance { get; set; }
        public List<int> CurrentStamina { get; set; }
        public List<string> NameOf { get; set; }
        public List<List<string>> WepNames { get; set; }
        //[n] entity; [0] #1 [1] #2
        public List<List<int>> Attack { get; set; }
        public List<int> Defence { get; set; }
        //[n] entity; [0] min [1] max; [0] #1 [1] #2
        public List<List<List<int>>> AtkPercent { get; set; }
        //[n] entity;[0] min [1] max; [0] #1 [1] #2
        public List<List<List<int>>> ArmourPenetrationRange { get; set; }
        //[n] entity;[0] min [1] max
        public List<List<int>> ArmourToughnessRange { get; set; }
        //AtkMaxPercent[0] = 1.1 * stuff;

        // ^- slots
        // Slot 0 = Player
        // Slot 1-6 = Player's Party (up to 6)
        // Slot 7-12 = Enemies (up to 6 as well)
        // Party's stats stored here

        public void LoadEntities()
        {
            // TODO: Review XML code, see how to use it. - Tim
            //Location db -> scenario db -> enemy db
            CurrentVitality[0] = Player.Instance.CurrentVitality;
            CurrentMana[0] = Player.Instance.CurrentMana;
            CurrentEndurance[0] = Player.Instance.CurrentEndurance;
            NameOf[0] = Player.Instance.Name; //TODO: or "You"

            //TODO: Calculate wep stats, check cheating

            for (var i = 1; i < 7; i++)
            {
                // load party
                Console.Write("Prevent Resharper from deleting");
            }
            for (var i = 7; i < 13; i++)
            {
                // load enemies
                Console.Write("Prevent Resharper from deleting");
            }
        }

        public void Fight(int attacker, int defender, int attackType)
        {
            // TODO: Print the action line.
            //What is attacktype?
            var rand = new Random();
            decimal damageNumber = rand.Next(0, 10001);
            decimal penetrationNumber = rand.Next(0, 10001);
            decimal armourNumber = rand.Next(0, 10001);
            var weaponNumber = new List<string> { WepNames[attacker][0], WepNames[attacker][1] }.Select();
            var totalPenetration =
                Convert.ToInt32(
                    Math.Round(ArmourPenetrationRange[attacker][0][weaponNumber] +
                               penetrationNumber *
                               (ArmourPenetrationRange[attacker][1][weaponNumber] -
                                ArmourPenetrationRange[attacker][0][weaponNumber]) / 10000));
            var totalArmour =
                Convert.ToInt32(
                    Math.Round(ArmourToughnessRange[defender][0] +
                               armourNumber *
                               (ArmourToughnessRange[defender][1] -
                                ArmourToughnessRange[defender][0]) / 10000));
            var penetrationDamage = totalPenetration - totalArmour;
            var totalDamage =
                Convert.ToInt32(
                    Math.Round(AtkPercent[attacker][0][weaponNumber] +
                               damageNumber *
                               (AtkPercent[attacker][1][weaponNumber] - AtkPercent[attacker][0][weaponNumber]) /
                               10000));
            var damageDealt = Math.Max(1,
                (Attack[attacker][weaponNumber] + penetrationDamage) * totalDamage - Defence[defender]);
            if (penetrationDamage < 0)
            {
                damageDealt = 1;
                Console.WriteLine(NameOf[attacker] + "struck a glancing blow!");
            }
            else
            {
                Console.WriteLine(NameOf[attacker] + "used a basic attack!");
            }
            //TODO: for hp: you have several minor cuts and bruises, you have a fatal wound


            CurrentVitality[defender] = CurrentVitality[defender] - damageDealt;
            // TODO: change attack/defence mechanics, change text, implement misses (accuracy stat)/dodge (evasion stat)
        }

        public void StartFight()
        {
            // See how to handle from script - Tim
            // GUI - select enemy to attack - do the same for party? or party autoattack?
            // selectOption(options); to hide player menu where options = string[]
            var enemyToAttack = new string[6];
            for (var i = 6; i < 13; i++) if (!String.IsNullOrEmpty(NameOf[i])) enemyToAttack[i] = NameOf[i];
            // var partySelected = utility.SelectOption(enemyToAttack);
            // Fight();
            //TODO: loop until player/enemy dies
        }

        public void EndFight()
        {
            Player.Instance.CurrentMana = CurrentMana[0];
            Player.Instance.CurrentVitality = CurrentVitality[0];
            Player.Instance.CurrentEndurance = CurrentEndurance[0];
            Player.Instance.Experience += 1; //TODO: read data from xml
            var expNeeded = Convert.ToInt32(Math.Pow(1.05, Player.Instance.Level) * 1000);
            if (Player.Instance.Experience >= expNeeded) Player.Instance.Level++; Player.Instance.Experience -= expNeeded;
            // TODO: regen while out of fight?
        }
    }
}
