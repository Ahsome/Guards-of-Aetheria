using System;

namespace GuardsOfAetheria
{
    internal class Combat
    {
        private static readonly Combat OrigInstance = new Combat();
        private readonly Utility utility = new Utility();

        private Combat() {}
        static Combat() {}

        public int[] CurrentVitality { get; set; }
        public int[] CurrentMana { get; set; }
        public int[] CurrentEndurance { get; set; }
        public int[] CurrentStamina { get; set; }
        public string[] Name { get; set; }
        public string[][] WepNames { get; set; }
        //[n] entity; [0] #1 [1] #2
        public int[][] Attack { get; set; }
        public int[] Defence { get; set; }
        //[n] entity; [0] min [1] max; [0] #1 [1] #2
        public int[][][] AtkPercent { get; set; } //TODO: calculate before, or check for file edits?
        //[n] entity;[0] min [1] max; [0] #1 [1] #2
        public int[][][] ArmourPenetrationRange { get; set; }
        //[n] entity;[0] min [1] max
        public int[][] ArmourToughnessRange { get; set; }

        public static Combat Instance { get { return OrigInstance; } }

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
            Instance.CurrentVitality[0] = Player.Instance.CurrentVitality;
            Instance.CurrentMana[0] = Player.Instance.CurrentMana;
            Instance.CurrentEndurance[0] = Player.Instance.CurrentEndurance;
            Instance.Name[0] = "You";

            utility.CalculateWeaponStats();
                //TODO: check cheating

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
            string[] options = {WepNames[attacker][0], WepNames[attacker][1]}; //TODO: change
            var weaponNumber = utility.SelectOption(options);
            var totalPenetration =
                Convert.ToInt32(
                    Math.Round(ArmourPenetrationRange[attacker][0][weaponNumber] +
                               penetrationNumber*
                               (ArmourPenetrationRange[attacker][1][weaponNumber] -
                                ArmourPenetrationRange[attacker][0][weaponNumber])/10000));
            var totalArmour =
                Convert.ToInt32(
                    Math.Round(ArmourToughnessRange[defender][0] +
                               armourNumber*
                               (ArmourToughnessRange[defender][1] -
                                ArmourToughnessRange[defender][0])/10000));
            var penetrationDamage = totalPenetration - totalArmour;
            var totalDamage =
                Convert.ToInt32(
                    Math.Round(AtkPercent[attacker][0][weaponNumber] +
                               damageNumber*
                               (AtkPercent[attacker][1][weaponNumber] - AtkPercent[attacker][0][weaponNumber])/
                               10000));
            var damageDealt = Math.Max(1,
                (Attack[attacker][weaponNumber] + penetrationDamage)*totalDamage - Defence[defender]);
            if (penetrationDamage < 0)
            {
                damageDealt = 1;
                Console.WriteLine(Name[attacker] + "struck a glancing blow!");
            }
            else
            {
                Console.WriteLine(Name[attacker] + "used a basic attack!");
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
            var enemyToAttack= new string[6];
            for (var i = 6; i < 13; i++) if (!String.IsNullOrEmpty(Name[i])) enemyToAttack[i] = Name[i];
            // var partySelected = utility.SelectOption(enemyToAttack);
            // Fight();
            //TODO: loop until player/enemy dies
        }

        public void EndFight()
        {
            Player.Instance.CurrentMana = Instance.CurrentMana[0];
            Player.Instance.CurrentVitality = Instance.CurrentVitality[0];
            Player.Instance.CurrentEndurance = Instance.CurrentEndurance[0];
            Player.Instance.Experience += 1; //TODO: read data from xml
            utility.UpdateExp();
            // TODO: regen while out of fight?
        }
    }
}