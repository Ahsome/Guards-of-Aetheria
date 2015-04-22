using System;
using System.Collections.Generic;

namespace GuardsOfAetheria
{
    internal class MainMenu
    {
        //TODO: autosave plot story selected to .txt
        public static bool CharacterIsSelected;
        public static void DisplayMainMenu()
        {
            while (!CharacterIsSelected)
            {
                Console.Clear();
                Console.WriteLine("                                                                               ");
                Console.WriteLine("               ╔═╗┬ ┬┌─┐┬─┐┌┬┐┌─┐  ┌─┐┌─┐  ╔═╗┌─┐┌┬┐┬ ┬┌─┐┬─┐┬┌─┐              ");
                Console.WriteLine("               ║ ╦│ │├─┤├┬┘ ││└─┐  │ │├┤   ╠═╣├┤  │ ├─┤├┤ ├┬┘│├─┤              ");
                Console.WriteLine("               ╚═╝└─┘┴ ┴┴└──┴┘└─┘  └─┘└    ╩ ╩└─┘ ┴ ┴ ┴└─┘┴└─┴┴ ┴              ");
                Console.WriteLine("                                                                               ");
                Console.WriteLine("Welcome to the Guards of Atheria");
                Console.WriteLine("A simple game, set in the land of Aesrin");
                Console.WriteLine("What would you like to do?\n");
                Console.CursorTop = 22;
                Console.WriteLine(" - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ");
                Console.WriteLine("                      © Black-Strike Studios, 2014 - {0}                      ", DateTime.Now.Year);
                Console.CursorTop = 8;
                new List<string> { "New", "Load", "Options", "Credits", "Quit", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""}.Select().Activate();
            }
        }

        public static void ShowCredits()
        {
            Console.Clear(); Console.WriteLine("Credits:\n\nCoders:\nAhkam \"Ahsome\" Nihardeen\nTimothy \"aytimothy\" Chew\nE-Hern \"somebody1234\" Lee\n\nWriter/Designer:\nPerry \"Lafamas\" Luo\nJohnathan");
            Console.ReadKey();
        }
    }

    static class MenuExtensions
    {
        public static void Activate(this int option)
        {
            switch (option)
            {
                case 0: Players.Create(); MainMenu.CharacterIsSelected = true; break;
                case 1: // MainMenu.CharacterIsSelected = true; TODO: LoadGame();, (SaveGame(); in location 'home'/'base' ) - inventory, location, plot location, enemies defeated/locations progress
                    break;
                case 2: Options.Change(); break;
                case 3: MainMenu.ShowCredits(); break;
                case 4: Environment.Exit(0); break;
            }
        }
    }
}