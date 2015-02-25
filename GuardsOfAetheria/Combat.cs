using System;

namespace GuardsOfAetheria
{
    class Combat
    {
        public int[] CurrentVitality { get; set; }
        public int[] CurrentMana { get; set; }
        public int[] CurrentEndurance { get; set; }

        public string[] Name { get; set; }
        public int[] Attack { get; set; }
        public int[] Defence { get; set; }
        
        // Slot 0 = Player
        // Slot 1-6 = Player's Party (up to 6)
        // Slot 7+ = Enemies (as many as you want)

        public void LoadEnemy(string type, string variant, int slot)
        {
            // TODO: Review XML code, see how to use it.
        }

        public void LoadPlayer()
        {
            Instance.CurrentVitality[0] = Player.Instance.CurrentVitality;
            Instance.CurrentMana[0] = Player.Instance.CurrentMana;
            Instance.CurrentEndurance[0] = Player.Instance.CurrentEndurance;
        }

        public void LoadPlayerParty()
        {
            // If party has nobody but the player, break.
            // If party 1 or more members, load member #1, else, break.
            // If party 2 or more members, load member #2, else, break.
            // Etc. etc. etc.
            // Array + for loop?
        }

        public void Fight(int attackerSlot, int defenderSlot, int attackType)
        {
            // TODO: Print the action line.
            Console.WriteLine(Name[attackerSlot] + "used a basic attack and dealt " + (Attack[attackerSlot] - Defence[defenderSlot]) + " damage!");
            CurrentVitality[defenderSlot] = CurrentVitality[defenderSlot] - (Attack[attackerSlot] - Defence[defenderSlot]);
        }

        public void StartFight()
        {
            // See how to handle from script
        }

        public void EndFight()
        {
            Player.Instance.CurrentMana = Instance.CurrentMana[0];
            Player.Instance.CurrentVitality = Instance.CurrentVitality[0];
            // Include Endurance? Regen while out of fight?
        }

        public static Combat Instance
        {
            get
            {
                return Instance;
            }
        }
    }
}
