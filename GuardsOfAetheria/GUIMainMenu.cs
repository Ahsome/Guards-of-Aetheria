using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardsOfAetheria
{
    class GUIMainMenu
    {
        public void DisplayMainMenu()
        {

            Console.Clear();
            Console.WriteLine("Welcome to the Guards of Atheria");
            Console.WriteLine("A simple game, set in the land of Aesrin");
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("> New Game\n  Load Game\n  Options\n  Credits\n  Quit Game");

            Utility utility = new Utility();
            int menuSelected = utility.SelectOption(3, 4);
            ActivateSelectedMenu(menuSelected);
 
        }

        private void ActivateSelectedMenu(int menuSelected)
        {
            switch (menuSelected)
            {
                case 1:
                    var characterCreation = new CharacterCreation();
                    characterCreation.CreateCharacter();
                    break;
                case 2:
                //LoadGame
                case 3:
                    DisplayOptions();
                    break;
                case 4:
                    DisplayCredits();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
            }
        }

        private void DisplayOptions()
        {
            Console.Clear();
            //Options
            return;
        }
        private void DisplayCredits()
        {
            Console.Clear();
            //Credits
            return;
        }
    }
}
