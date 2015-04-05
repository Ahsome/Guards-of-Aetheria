using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace GuardsOfAetheria
{
    internal class MainMenu
    {
        private static readonly string AppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Guards of Aetheria");
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
            new[] {"New Game", "Load Game", "Options", "Credits", "Quit Game"}.SelectOption().Activate();
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
                { Console.SetCursorPosition(left + 2 + spacing*j, top + i); Console.Write(Options.Instance.Strings[i][j]); }
            }
            Console.SetCursorPosition(left, top); Console.Write('>');
            while (true)
            {
                var input = Console.ReadKey().Key;
                Console.SetCursorPosition(left + spacing*choice, top + number); Console.Write(' ');
                switch (input)
                {
                    case ConsoleKey.UpArrow: number--; break;
                    case ConsoleKey.DownArrow: number++; break;
                    case ConsoleKey.LeftArrow: choice--; break;
                    case ConsoleKey.RightArrow: choice++; break;
                    case ConsoleKey.Enter:
                        var doc = new XmlDocument();
                        var location = Path.Combine(AppData, "Options.option");
                        doc.Load(location);
                        XElement.Load(location);
                        for (var i = 0; i < Options.Instance.Names.Length; i++)
                        {
                            var newOption = doc.SelectSingleNode(String.Format("settings/setting[@number='{0}']", i));
                            if (newOption == null) continue;
                            if (newOption.Attributes != null)
                                newOption.Attributes[1].Value = choice.ToString();
                        }
                        doc.Save(location);
                        DisplayMainMenu();
                        return;
                        //TODO: break to calling method or something
                }
                if (number < 0) number = Options.Instance.Names.Length - 1;
                if (number > Options.Instance.Names.Length - 1) number = 0;
                if (choice < 0) choice = Options.Instance.List[number].Length - 1;
                if (choice > Options.Instance.List[number].Length - 1) choice = 0;
                Options.Instance.Current[number] = Options.Instance.List[number][choice];
                Console.SetCursorPosition(left + spacing*choice, number + 2);
                Console.Write('>');
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