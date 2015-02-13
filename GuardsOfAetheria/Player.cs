using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardsOfAetheria
{
    class Player
    {
        // General Character Stats
        public string PlayerName { get; set; }
        public string PlayerClass { get; set; }

        // Character Attributes
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Wisdom { get; set; }

        // Strength-related Stuffs
        private int BaseHealth { get; set; }
        private int BuffHealth { get; set; }
        private int BuffHealthPerCent { get; set; }
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        private int BaseStamina { get; set; }
        private int BuffStaminia { get; set; }
        private int BuffStaminiaPerCent { get; set; }
        public int MaxStaminia { get; set; }
        public int CurrentStaminia { get; set; }

        // Agility-related stuff
        // None. They're all mostly calculated.

        // Wisdom-related stuff
        private int BaseMana { get; set; }
        private int BuffMana { get; set; }
        private int BuffManaPerCent { get; set; }
        public int MaxMana { get; set; }
        public int CurrentMana { get; set; }
        private int BaseLuck { get; set; }
        public int Luck { get; set; }

        // Stuff that has calcuation in it.
        public int Attack { get; set; }
        public int Defence { get; set; } // Physical Def.
        public int Shield { get; set; } // Magic Def.
        public int Evasion { get; set; }
        private int EvasionBuff { get; set; }
        private int EvasionBuffPerCent { get; set; }
        public int Speed { get; set; }
        private int SpeedBuff { get; set; }
        private int SpeedBuffPerCent { get; set; }
        public int Vision { get; set; }
        public int ArmorPenetration { get; set; }

        // Locational Data
        public string Location { get; set; }

        public void UpdateAttributes()
        {

        }
        public void AssignAtts()
        {
            switch (Player.Instance.PlayerClass)
            {
                if (Player.Instance.PlayerClass = "Knight") {
                    Player.Instance.Strength; = Player.Instance.Strength + 5;
                    Player.Instance.Wisdom; = Player.Instance.Wisdom + 1;
                    Player.Instance.Agility; = Player.Instance.Agility + 3;
                }
                else if (Player.Instace.PlayerClass = "Ranger") {
                    Player.Instance.Strength; = Player.Instance.Strength + 1;
                    Player.Instance.Wisdom; = Player.Instance.Wisdom + 3;
                    Player.Instance.Agility; = Player.Instance.Agility + 5;
                }
                else if (Player.Instance.PlayerClass = "Wizard") {
                    Player.Instance.Strength; = Player.Instance.Strength + 3;
                    Player.Instance.Wisdom; = Player.Instance.Wisdom + 5;
                    Player.Instance.Agility; = Player.Instance.Agility + 1;
                }
                else {
                    // Complain.
                    return;
                };
            }
        }

        private static readonly Player instance = new Player();

        static Player()
        {
        }

        private Player()
        {
        }

        public static Player Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
