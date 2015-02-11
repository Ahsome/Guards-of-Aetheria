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
            public string CombatEnemy { get; set; }
            public string CombatFighter { get; set; }
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
        public static getProps(int combatEnemy, int combatFighter)
        {
            // Gets and calculates the attack scenerio's numbers.
            
        }
        public void fight
        {
            // Does the calculations.

        }
        public void output
        {
            // Outputs the result.

        }
    }
}