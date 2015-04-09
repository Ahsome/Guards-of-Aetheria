using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace GuardsOfAetheria
{
    internal class MainProgram
    {
        //TODO: improve readability
        //TODO: create character + save, Console.Title for location etc
        private static readonly string AppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Guards of Aetheria");
        private static void Main()
        {
            // Fake Class: Spaghetti Monster ;)
            Console.CursorVisible = false;
            //TODO: 
            Player.Instance.RoomId = 1;
            //TODO: change when locations are finalised, 
            if (!Directory.Exists(AppData)) Directory.CreateDirectory(AppData);
            var options = new Options();
            options.LoadOptions();
            MainMenu.DisplayMainMenu();
            var movement = new Movement();
            while (true) movement.ShowLocation();
            //Took way too long to make this ;)
        }
    }
}
