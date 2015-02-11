using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardsOfAetheria
{
    class Player
    {
        public enum Class
        {
            Melee,
            Magic,
            Ranged
        }
        public string PlayerName { get; set; }
        public Class PlayerClass { get; set; }

        public int Experience { get; set; }
        public int Level { get; set; }

        public int StrengthAtt { get; set; }
        public int DexterityAtt { get; set; }
        public int WisdomAtt { get; set; }

        public int BaseVitality { get; set; }
        public int CurrentVitality { get; set; }
        //Rename Mana to something better
        public int BaseMana { get; set; }
        public int CurrentMana { get; set; }
        public int BaseEndurance { get; set; }
        public int CurrentEndurance { get; set; }

        public int DefenceAtt { get; set; }
        public int AttackAtt { get; set; }

        public int AccuracyAtt { get; set; }
        public int EvasionAtt { get; set; }

        public int LuckAtt { get; set; }

        public int PrimaryAtt { get; set; }
        public int SecondaryAtt { get; set; }
        public int TertiaryAtt { get; set; }

        public int PerceptionAtt { get; set; }
        public string[,] inventory { get; set; }

        public string Location { get; set; }

        public void UpdateAttributes()
        {
            //DO THIS DAMNIT
            //Note, similar to AssignAtts, but different
        }
        public void AssignAtts()
        {
            switch (Player.Instance.PlayerClass)
            {
                case Player.Class.Melee:
                    Player.Instance.PrimaryAtt = Player.Instance.StrengthAtt;
                    Player.Instance.SecondaryAtt = Player.Instance.WisdomAtt;
                    Player.Instance.TertiaryAtt = Player.Instance.DexterityAtt;
                    break;
                case Player.Class.Magic:
                    Player.Instance.PrimaryAtt = Player.Instance.WisdomAtt;
                    Player.Instance.SecondaryAtt = Player.Instance.DexterityAtt;
                    Player.Instance.TertiaryAtt = Player.Instance.StrengthAtt;
                    break;
                case Player.Class.Ranged:
                    Player.Instance.PrimaryAtt = Player.Instance.StrengthAtt;
                    Player.Instance.SecondaryAtt = Player.Instance.WisdomAtt;
                    Player.Instance.TertiaryAtt = Player.Instance.DexterityAtt;
                    break;
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
