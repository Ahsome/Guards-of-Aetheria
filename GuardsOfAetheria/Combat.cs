using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int[] Shield { get; set; }

        public void loadEnemy(string type, string variant, int slot)
        {
            // TODO: Review XML code, see how to use it.
        }

        public void loadPlayer()
        {
            Combat.instance.CurrentVitality[1] = Player.Instance.CurrentVitality;
            Combat.instance.CurrentMana[1] = Player.Instance.CurrentMana;
            Combat.instance.CurrentEndurance[1] = Player.Instance.CurrentEndurance;
        }

        public void loadPlayerParty()
        {
            // If party has nobody but the player, break.
            // If party 1 or more members, load member #1, else, break.
            // If party 2 or more members, load member #2, else, break.
            // Etc. etc. etc.
        }

        public void fight(int AttackerSlot, int DefenderSlot, int AttackType)
        {
            // TODO: Print the action line.
            Console.WriteLine(Combat.instance.Name[AttackerSlot] + "used a basic attack and dealt " + (Combat.instance.Attack[AttackerSlot] - Combat.instance.Defence[DefenderSlot]) + " damage!");
            Combat.instance.CurrentVitality[DefenderSlot] = Combat.instance.CurrentVitality[DefenderSlot] - (Combat.instance.Attack[AttackerSlot] - Combat.instance.Defence[DefenderSlot]);
        }

        public void startFight()
        {
            // See how to handle from script
        }

        public void endFight()
        {
            Player.Instance.CurrentMana = Combat.instance.CurrentMana[1];
            Player.Instance.CurrentVitality = Combat.instance.CurrentVitality[1];
        }

        public static Combat instance
        {
            get
            {
                return instance;
            }
        }
    }
}