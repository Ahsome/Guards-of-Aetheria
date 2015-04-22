namespace GuardsOfAetheria
{
    internal class Skills
    {
        public void GetSkills() //TODO: how to unlock other class skills? amount of a certain att?
        {
            switch (Player.Instance.Class) //TODO: skill list, change skill name every level up
            {
                case Player.Classes.Melee:
                    break;
                case Player.Classes.Ranged:
                    break;
                case Player.Classes.Magic:
                    break;
            }
        }

        //public void ViewSkills()
        //{
        //}
    }
}