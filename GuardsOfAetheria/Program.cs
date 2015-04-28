using System;
using System.IO;

namespace GuardsOfAetheria
{
    internal class MainProgram
    {
        //private static readonly string AppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Guards of Aetheria");
        private static void Main() { MainAsync(); }

        private static async void MainAsync()
        {
            // Fake Class: Spaghetti Monster ;)
            var task = Database.DatabaseConnection.OpenAsync();
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
            //TODO: for fullscreen option? Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight); screen size options, allow/disallow autoresize option, gentyricise, improve readability
            Console.Title = "Guards of Aetheria";
            Console.CursorVisible = false;
            //if (!Directory.Exists(AppData)) Directory.CreateDirectory(AppData);
            Options.Load();
            MainMenu.DisplayMainMenu();
            await task;
            Database.DatabaseConnection.Close();
            while (true) Movement.ShowLocation();
            //Took way too long to make this ;)
        }
    }
}
