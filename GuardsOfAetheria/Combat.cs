using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardsOfAetheria
{
    class Combat
    {
        class Battlefield
        {
            // Array lots 1-6 = Player, 7+ = Enemy
            public int[] CurrentVitality;
            public int[] CurrentMana;
            public int[] CurrentEndurance;

            public string[] Name;
            public int[] Attack;
            public int[] Defence;
            public int[] Shield;
        }

        public void loadEnemy(string type, string variant, int slot)
        {
            // TODO: Review XML code, see how to use it.
        }

        public void loadPlayer()
        {
            
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
    }
}