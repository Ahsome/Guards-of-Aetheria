using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardsOfAetheria
{
    class Movement
    {
        Utility utility = new Utility();
        public void ShowLocation()
        {
            Console.Clear();
            switch (Player.Instance.Location)
            {
                case "TutorialArea":
                    Console.WriteLine("Welcome to the game, {0}. Test the movement system. Good luck with that", Player.Instance.PlayerName);
                    Console.WriteLine("Your given options are located below");
                    Console.WriteLine("Use your arrow keys to move, press ENTER to select");
                    break;
                case "Corridor":
                    Console.WriteLine("Welcome to the corridor, {0}", Player.Instance.PlayerName);
                    break;
            }
            LocationOption();
        }

        public int LocationOption()
        {
            switch (Player.Instance.Location)
            {
                case "TutorialArea":
                    Console.SetCursorPosition(0, 6);
                    Console.WriteLine("> Corridor\n  Random House (NOT WORKING)\n  A subway (NOT WORKING)\n  Heaven (NOT DEAD ENOUGH)");
                    int option = utility.SelectOption(5, 3);
                    if (option == 1)
                    {
                        SetLocation("Corridor");
                    }
                    else
                    {
                        LocationOption();
                    }
                    break;
                case "Corridor":
                    Console.WriteLine("Sorry I haven't bothered to give any options, {0}", Player.Instance.PlayerName);
                    Console.ReadKey();
                    break;
            }
            return 0;
        }

        private void SetLocation(string newArea)
        {
            Player.Instance.Location = newArea;
        }
    }
}
