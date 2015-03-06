using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace GuardsOfAetheria
{
    class MainMenu
    {
        readonly string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        //TODO: autosave plot story selected to .txt
        public void DisplayMainMenu()
        {
            var utility = new Utility();
            Console.Clear();
            if (!Directory.Exists(appdata + @"\Guards of Aetheria"))
            {
                Directory.CreateDirectory(appdata + @"\Guards of Aetheria");
            }
            if (!File.Exists(appdata + @"\Guards of Aetheria\Options.options"))
            {
                Options.Instance.InitialiseOptions();
            }
            else
            {
                Options.Instance.CurrentSettings = new Options.Settings[Options.Instance.SettingsList.Length];
                var doc = new XmlDocument();
                doc.Load(appdata + @"\Guards of Aetheria\Options.options");
                var values = doc.SelectNodes("/setting/value/"); //TODO: is it /@number? - it's working
                var i = 0;
                foreach (var value in from XmlNode node in values select utility.IntParseFast(node.ToString()))
                {
                    Options.Instance.CurrentSettings[i] = Options.Instance.SettingsList[i][value];
                    i++;
                }
            }
            Console.WriteLine("                                                                               ");
            Console.WriteLine("               ╔═╗┬ ┬┌─┐┬─┐┌┬┐┌─┐  ┌─┐┌─┐  ╔═╗┌─┐┌┬┐┬ ┬┌─┐┬─┐┬┌─┐              ");
            Console.WriteLine("               ║ ╦│ │├─┤├┬┘ ││└─┐  │ │├┤   ╠═╣├┤  │ ├─┤├┤ ├┬┘│├─┤              ");
            Console.WriteLine("               ╚═╝└─┘┴ ┴┴└──┴┘└─┘  └─┘└    ╩ ╩└─┘ ┴ ┴ ┴└─┘┴└─┴┴ ┴              ");
            Console.WriteLine("                                                                               ");
            Console.WriteLine("Welcome to the Guards of Atheria");
            Console.WriteLine("A simple game, set in the land of Aesrin");
            Console.WriteLine("What would you like to do?\n");
            Console.SetCursorPosition(0, 22);
            Console.WriteLine(" - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ");
            Console.WriteLine("                     (C) Black-Strike Studios, 2014 - {0}                     ", DateTime.Now.Year);
            Console.SetCursorPosition(0, 8);
            string[] options = { "New Game", "Load Game", "Options", "Credits", "Quit Game" };
            var menuSelected = utility.SelectOption(options);
            ActivateSelectedMenu(menuSelected);
        }

        private void ActivateSelectedMenu(int menuSelected)
        {
            var charCreation = new CharacterCreation();
            switch (menuSelected)
            {
                case 1:
                    charCreation.CreateCharacter();
                    break;
                case 2:
                    // TODO: LoadGame();, (SaveGame(); in location 'home'/'base' )
                    break;
                case 3:
                    DisplayOptions();
                    break;
                case 4:
                    DisplayCredits();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
            }
        }

        private void DisplayOptions() //TODO: make general method for options?
        {
            Console.Clear();
            Console.WriteLine("Options\n");
            for (var i = 0; i < Options.Instance.SettingNames.Length; i++)
            {
                Console.SetCursorPosition(0, 2 + i);
                Console.Write(Options.Instance.SettingNames[i]);
                for (var j = 0; j < Options.Instance.SettingsList[i].Length; j++)
                {
                    Console.SetCursorPosition(40 + 12 * j, 2 + i);
                    Console.Write(Options.Instance.SettingNameStrings[i][j]);
                }
            }
            var optionNumber = 0;
            var optionChoice = 0;
            Console.SetCursorPosition(38, 2);
            Console.Write('>');
            while (true)
            {
                var input = Console.ReadKey().Key;
                Console.SetCursorPosition(38 + 12 * optionChoice, optionNumber + 2);
                Console.Write(' ');

                switch (input)
                {
                    case ConsoleKey.UpArrow:
                        optionNumber--;
                        break;
                    case ConsoleKey.DownArrow:
                        optionNumber++;
                        break;
                    case ConsoleKey.LeftArrow:
                        optionChoice--;
                        break;
                    case ConsoleKey.RightArrow:
                        optionChoice++;
                        break;
                    case ConsoleKey.Enter:
                        var doc = new XmlDocument();
                        for (var i = 0; i < Options.Instance.SettingNames.Length; i++)
                        {
                            doc.AppendChild(doc.CreateElement(Convert.ToString(optionNumber), Convert.ToString(optionChoice)));
                        }
                        doc.Save(appdata + @"\Guards of Aetheria\Options.option");
                        DisplayMainMenu();
                        return;
                }

                if (optionNumber < 0) { optionNumber = Options.Instance.SettingNames.Length - 1; }
                if (optionNumber > Options.Instance.SettingNames.Length - 1) { optionNumber = 0; }
                if (optionChoice < 0) { optionChoice = Options.Instance.SettingsList[optionNumber].Length - 1; }
                if (optionChoice > Options.Instance.SettingsList[optionNumber].Length - 1) { optionChoice = 0; }

                Options.Instance.CurrentSettings[optionNumber] = Options.Instance.SettingsList[optionNumber][optionChoice];

                Console.SetCursorPosition(38 + 12 * optionChoice, optionNumber + 2);
                Console.Write('>');
            }
        }
        private void DisplayCredits()
        {
            Console.Clear();
            Console.WriteLine("Coders:\nAhsome\naytimothy\nsomebody1234\n\nWriter/Designer:\nLafamas");
            DisplayMainMenu();
        }
    }
}
