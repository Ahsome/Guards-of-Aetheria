using System;

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
            #region Initiate Display
            Console.BufferHeight = Console.WindowHeight = Console.LargestWindowHeight - 5;
            Console.BufferWidth = Console.WindowWidth = Console.LargestWindowWidth - 5;
            Console.Title = "Guards of Aetheria";
            Console.CursorVisible = false;
            //TODO: for fullscreen option? screen size options, allow/disallow autoresize option (how?), genericise, improve readability, shorten
            #endregion
            //if (!Directory.Exists(AppData)) Directory.CreateDirectory(AppData);
            Options.Load();
            MainMenu.Display();
            await task;
            Database.DatabaseConnection.Close();
            //TODO: infinite timeout
            while (true) Movement.ShowLocation();
            //Took way too long to make this ;)
        }
    }
}
