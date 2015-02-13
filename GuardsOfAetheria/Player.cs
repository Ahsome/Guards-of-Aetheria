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
        public string Name { get; set; }
        public Class PlayerClass { get; set; }

        public int Experience { get; set; }
        public int Level { get; set; }

        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Wisdom { get; set; }

        public int BaseVitality { get; set; }
        public int CurrentVitality { get; set; }

        public int BaseMana { get; set; }
        public int CurrentMana { get; set; }

        public int BaseEndurance { get; set; }
        public int CurrentEndurance { get; set; }

        public int Defence { get; set; }
        public int AttackAtt { get; set; }

        public int AccuracyAtt { get; set; }
        public int EvasionAtt { get; set; }

        public int LuckAtt { get; set; }

        public int PrimaryAtt { get; set; }
        public int SecondaryAtt { get; set; }
        public int TertiaryAtt { get; set; }

        public int PerceptionAtt { get; set; }
        public string[,] inventory { get; set; }

        public string LocationRegion { get; set; }
        public string LocationArea { get; set; }
        public string LocationBuilding { get; set; }
        public string LocationRoom { get; set; }

        public void AssignAtts()
        {
            Player.Instance.PrimaryAtt = 13;
            Player.Instance.SecondaryAtt = 10;
            Player.Instance.TertiaryAtt = 7;
            switch (Player.Instance.PlayerClass)
            {
                case Player.Class.Melee:
                    Player.Instance.Strength = Player.Instance.PrimaryAtt;
                    Player.Instance.Wisdom = Player.Instance.SecondaryAtt;
                    Player.Instance.Dexterity = Player.Instance.TertiaryAtt;
                    break;
                case Player.Class.Magic:
                    Player.Instance.Strength = Player.Instance.TertiaryAtt;
                    Player.Instance.Wisdom = Player.Instance.PrimaryAtt;
                    Player.Instance.Dexterity = Player.Instance.SecondaryAtt;
                    break;
                case Player.Class.Ranged:
                    Player.Instance.Strength = Player.Instance.SecondaryAtt;
                    Player.Instance.Wisdom = Player.Instance.TertiaryAtt;
                    Player.Instance.Dexterity = Player.Instance.PrimaryAtt;
                    break;
            }
        }
        public void UpdateAtts()
        {
            Player.Instance.BaseVitality = Player.Instance.Strength * 10;
            Player.Instance.CurrentVitality = Player.Instance.BaseVitality + 0;
            Player.Instance.BaseMana = Player.Instance.Wisdom * 10;
            Player.Instance.CurrentMana = Player.Instance.BaseMana + 0;
            Player.Instance.BaseEndurance = Player.Instance.Dexterity * 10;
            Player.Instance.CurrentEndurance = Player.Instance.BaseEndurance + 0;
            AssignAtts();
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
