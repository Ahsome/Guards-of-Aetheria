using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardsOfAetheria
{
    class Player
    {
        public enum playerClass
        {
            Melee, Magic, Ranged
        }
        public string PlayerName { get; set; }
        public playerClass PlayerClass { get; set; }

        public int StrengthAtt { get; set; }
        public int DexterityAtt { get; set; }
        public int WisdomAtt { get; set; }

        public int VitalityAtt { get; set; }
        //Rename Mana to something better
        public int ManaAtt { get; set; }
        public int EnduranceAtt { get; set; }

        public int DefenceAtt { get; set; }
    }
}
