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

        }

        public void fight(string AttackerSlot, string DefenderSlot)
        {

        }

        public void startFight()
        {
            // See how to handle from script
        }

        public void endFight()
        {
            // Write values back to character data.
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