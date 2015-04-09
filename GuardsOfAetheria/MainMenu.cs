using System;
using System.Collections.Generic;
using System.IO;

namespace GuardsOfAetheria
{
    internal class MainMenu
    {
        //TODO: autosave plot story selected to .txt
        public static void DisplayMainMenu()
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
            new List<string> { "New Game", "Load Game", "Options", "Credits", "Quit Game" }.SelectOption().Activate();
        }

        public static void ShowOptions()
        {
            Console.Clear();
            Console.WriteLine("Options\n");
            var top = Console.CursorTop; const int left = 38; const int spacing = 12; var number = 0; var choice = 0;
            for (var i = 0; i < Options.Instance.Names.Length; i++)
            {
                Console.SetCursorPosition(0, top + i);
                Console.Write(Options.Instance.Names[i]);
                for (var j = 0; j < Options.Instance.List[i].Length; j++)
                { Console.SetCursorPosition(left + 2 + spacing * j, top + i); Console.Write(Options.Instance.Strings[i][j]); }
            }
            Console.SetCursorPosition(left, top); Console.Write('>');
            while (true)
            {
                var input = Console.ReadKey().Key;
                Console.SetCursorPosition(left + spacing * choice, top + number); Console.Write(' ');
                switch (input)
                {
                    case ConsoleKey.UpArrow: number--; break;
                    case ConsoleKey.DownArrow: number++; break;
                    case ConsoleKey.LeftArrow: choice--; break;
                    case ConsoleKey.RightArrow: choice++; break;
                    case ConsoleKey.Enter:
                        Properties.Settings.Default[Options.Instance.Names[number].Replace(" ","_")] = Options.Instance.List[number][choice];
                        Properties.Settings.Default.Save();
                        DisplayMainMenu(); return;
                    //TODO: break to calling method or something
                }
                if (input == ConsoleKey.UpArrow || input == ConsoleKey.DownArrow) Properties.Settings.Default[Options.Instance.Names[number]] = choice;
                if (number < 0) number = Options.Instance.Names.Length - 1;
                if (number > Options.Instance.Names.Length - 1) number = 0;
                if (choice < 0) choice = Options.Instance.List[number].Length - 1;
                if (choice > Options.Instance.List[number].Length - 1) choice = 0;
                Options.Instance.Current[number] = Options.Instance.List[number][choice];
                Console.SetCursorPosition(left + spacing * choice, top + number); Console.Write('>');
            }
        }

        public static void ShowCredits()
        {
            Console.Clear(); Console.WriteLine("Credits:\n\nCoders:\nAhkam \"Ahsome\" Nihardeen\nTimothy \"aytimothy\" Chew\nE-Hern \"somebody1234\" Lee\n\nWriter/Designer:\nPerry \"Lafamas\" Luo\nJohnathan");
            Console.ReadKey();
            DisplayMainMenu();
        }
    }

    static class MenuExtensions
    {
        public static void Activate(this int option)
        {
            var charCreation = new CharacterCreation();
            switch (option)
            {
                case 1:
                    charCreation.CreateCharacter();
                    break;
                case 2:
                    // TODO: LoadGame();, (SaveGame(); in location 'home'/'base' ) - inventory, location, plot location, enemies defeated/locations progress
                    break;
                case 3:
                    MainMenu.ShowOptions();
                    break;
                case 4:
                    MainMenu.ShowCredits();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}