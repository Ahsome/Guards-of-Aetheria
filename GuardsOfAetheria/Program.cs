using System;
using System.IO;

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
            Console.BufferHeight = Console.WindowHeight;
            //TODO: optimise, comment where cheating may occur
            //TODO: for fullscreen option? Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            //TODO: screen size options, allow/disallow autoresize option
            Console.Title = "Guards of Aetheria";
            Console.CursorVisible = false;
            //TODO: change when locations are finalised
            if (!Directory.Exists(AppData)) Directory.CreateDirectory(AppData);
            Options.Load();
            MainMenu.DisplayMainMenu();
            var movement = new Movement();
            while (true) movement.ShowLocation();
            //Took way too long to make this ;)
        }
    }
}
