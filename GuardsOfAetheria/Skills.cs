using static GuardsOfAetheria.Toolbox;
namespace GuardsOfAetheria{
    internal class Skills{
        public void GetSkills()//TODO: how to unlock other class skills? amount of a certain att?
        {
            switch(Bag.Player().Class)//TODO: skill list, change skill name every level up
            {
                case Class.Melee:
                    break;
                case Class.Ranged:
                    break;
                case Class.Magic:
                    break;
            }
        }
    }
}
