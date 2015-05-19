using static System.Console;
namespace GuardsOfAetheria{
    internal class Program{
        //private static readonly string AppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Guards of Aetheria");
        //TODO: read oledb to class
        private static void Main() {MainAsync();}
        private static async void MainAsync(){
            // Fake Class: Spaghetti Monster ;)
            var task=Database.Connection.OpenAsync();
            #region Initiate Display
            BufferHeight=WindowHeight=LargestWindowHeight-5;
            BufferWidth=WindowWidth=LargestWindowWidth-5;
            Title="Guards of Aetheria";
            CursorVisible=false;
            //TODO: for fullscreen option? screen size options, allow/disallow autoresize option (how?), genericise, improve readability, shorten, move strings to json (faster than db)
            #endregion
            //if (!Directory.Exists(AppData)) Directory.CreateDirectory(AppData);
            Options.Load();
            MainMenu.Display();
            await task;
            Database.Connection.Close();
            while(true) Movement.ShowLocation();
            //Took way too long to make this ;)
            // ReSharper disable once FunctionNeverReturns
        }
    }
}
