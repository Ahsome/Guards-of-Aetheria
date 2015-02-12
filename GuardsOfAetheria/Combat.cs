using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardsOfAetheria
{
    class Combat
    {
            // Gets the player and enemy.
            // TODO: Give an ID assigning system for identification in fights.
            public int CombatEnemy { get; set; }
            public int CombatFighter { get; set; }
            private int figherHealth { get; set; }
            private int enemyHealth { get; set; }
            private int fighterStrength { get; set; }
            private int enemyStrength { get; set; }
            private int figherArmor { get; set;}
            private int enemyStrength { get; set; }
        public void combat() 
        {
            getProps(combatEnemy, combatFighter);
            fight();
            outputFight();
        }
        private void getProps(int combatEnemy, int combatFighter)
        {
            // Gets and calculates the attack scenerio's numbers.
            fighterHealth = 0;
            enemyHealth = 0;
            fighterStrength = 0;
        }
        private void fight
        {
            // Does the calculations.

        }
        private void output
        {
            // Outputs the result.

        }
    }
}