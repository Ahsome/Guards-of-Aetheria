using System;

namespace GuardsOfAetheria
{
    internal class MainProgram
    {
        private static void Main()
        {
            // Fake Class: Spaghetti Monster ;)

            Console.CursorVisible = false;
            Player.Instance.LocationRegion = "TestRegion";
            Player.Instance.LocationArea = "TestArea";
            Player.Instance.LocationBuilding = "Outside";
            Player.Instance.LocationRoom = "TutorialRoom";

            var mainMenu = new MainMenu();
            mainMenu.DisplayMainMenu();

            var movement = new Movement();

            while (true)
            {
                movement.ShowLocation();
                Console.SetCursorPosition(0, 0);
                Console.Clear();
            }
            //Took way too long to make this ;)
        }
    }
}
