namespace GuardsOfAetheria
{
    internal class Skills
    {
        public void GetSkills() //TODO: how to unlock other class skills? amount of a certain att?
        {
            switch (Player.Instance.PlayerClass) //TODO: skill list, change skill name every level up
            {
                case Player.Class.Melee:
                    break;
                case Player.Class.Ranged:
                    break;
                case Player.Class.Magic:
                    break;
            }
        }

        //public void ViewSkills()
        //{
        //}
    }
}