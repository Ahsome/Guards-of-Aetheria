using System;

namespace GuardsOfAetheria
{
    internal class MainProgram
    {
        private static void Main()
        {
            /* Think about these:
             * Damage = MainAtt * random.Next(-lvl,lvl+1) + DamageModifier;
             * Fake Class: Spaghetti Monster ;)
             */

            Console.CursorVisible = false;
            Player.Instance.LocationRegion = "TestRegion";
            Player.Instance.LocationArea = "TestArea";
            Player.Instance.LocationBuilding = "Outside";
            Player.Instance.LocationRoom = "TutorialRoom";

            var mainMenu = new MainMenu();
            mainMenu.DisplayMainMenu();

            while (true)
            {
                var movement = new Movement();
                movement.ShowLocation();
                Console.SetCursorPosition(0, 0);
                Console.Clear();
            }
            //Took way too long to make this ;)
            //TODO: environemnt.exit in inventory options?
        }
    }
}
