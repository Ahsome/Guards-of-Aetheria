namespace GuardsOfAetheria{
    using static System.Console;
    internal class Program{
        private static void Main() {MainAsync();}
        private static async void MainAsync(){
            var task=Database.Connection.OpenAsync();
            #region Initiate Display
            BufferHeight=WindowHeight=LargestWindowHeight-5;
            BufferWidth=WindowWidth=LargestWindowWidth-5;
            Title="Guards of Aetheria";
            CursorVisible=false;
            //TODO: for fullscreen option? screen size options, allow/disallow autoresize option (how?), genericise, improve readability, shorten, move strings to json (faster than db)
            #endregion
            Options.Load();
            MainMenu.Display();
            await task;
            Database.Connection.Close();
            while(true) Movement.ShowLocation();
            // ReSharper disable once FunctionNeverReturns
        }
    }
}
