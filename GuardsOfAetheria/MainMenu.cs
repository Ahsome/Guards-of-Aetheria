using System;

namespace GuardsOfAetheria
{
    class MainMenu
    {
        public void DisplayMainMenu()
        {

            Console.Clear();
            Options.Instance.InitialiseOptions(); // TODO: Check if options are initialised
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
            Console.WriteLine("                     (C) Black-Strike Studios, 2014 - {0}                     ",DateTime.Now.Year);
            Utility utility = new Utility();
            Console.SetCursorPosition(0, 8);
            string[] options = { "New Game", "Load Game", "Options", "Credits", "Quit Game" };
            var menuSelected = utility.SelectOption(options, true);
            ActivateSelectedMenu(menuSelected);

        }

        private void ActivateSelectedMenu(int menuSelected)
        {
            switch (menuSelected)
            {
                case 1:
                    var characterCreation = new CharacterCreation();
                    characterCreation.CreateCharacter();
                    break;
                case 2:
                    // TODO: LoadGame();, SaveGame(); ( in location 'home'/'base' )
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

        private void DisplayOptions() // TODO: Autosave options in program files/mac equivalent/linux equivlent
        {
            Console.Clear();
            Console.WriteLine("Options\n"); // TODO: Make it bigger
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
                Console.SetCursorPosition(38 + optionChoice * 8, optionNumber + 2);
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
            //Credits
            Console.WriteLine("Coders:\nAhsome\naytimothy\nsomebody1234\n\nWriter:\nLafamas");
            DisplayMainMenu();
        }
    }
}
