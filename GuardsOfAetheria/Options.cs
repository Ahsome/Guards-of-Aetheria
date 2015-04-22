using System;
using System.Collections.Generic;
using GuardsOfAetheria.Properties;

namespace GuardsOfAetheria
{
    public class Options
    {
        public static List<string> Names = new List<string>
        {
            "Menu scrolling",
            "Page numbers"
        };

        public static List<List<string>> Strings = new List<List<string>>
        {
            new List<string> { "Pages", "Scroll" },
            new List<string> { "Hidden", "Visible" }
        };

        public static List<List<object>> List = new List<List<object>>
        {
            new List<object> { false, true },
            new List<object> { false, true }
        };

        public List<object> InitialValues = new List<object>
        {
            true
        };

        public static void Load()
        {
            Utility.ScrollingIsContinuous = Settings.Default.Menu_scrolling;
            Utility.PageNumIsVisible = Settings.Default.Page_numbers;
        }

        public static void Change()
        {
            Console.Clear();
            Console.WriteLine("Options\n");
            var top = Console.CursorTop; const int left = 38; const int spacing = 12; var number = 0; var choice = 0; var oldNumber = 0;
            for (var i = 0; i < Names.Count; i++)
            {
                Console.SetCursorPosition(0, top + i);
                Console.Write(Names[i]);
                for (var j = 0; j < List[i].Count; j++)
                { Console.SetCursorPosition(left + 2 + spacing * j, top + i); Console.Write(Strings[i][j]); }
            }
            choice = List[number].IndexOf(Settings.Default[Names[number].Replace(" ", "_")]);
            '>'.WriteAt(left + spacing * choice, top + number);
            while (true)
            {
                var input = Console.ReadKey().Key;
                ' '.WriteAt(left + spacing * choice, top + number);
                switch (input)
                {
                    case ConsoleKey.UpArrow: number--; break;
                    case ConsoleKey.DownArrow: number++; break;
                    case ConsoleKey.LeftArrow: choice--; break;
                    case ConsoleKey.RightArrow: choice++; break;
                    case ConsoleKey.Enter:
                        Settings.Default[Names[number].Replace(" ", "_")] = List[number][choice];
                        Settings.Default.Save();
                        Load();
                        return;
                    //TODO: break to calling method or something
                }
                if (input == ConsoleKey.UpArrow || input == ConsoleKey.DownArrow)
                {
                    Settings.Default[Names[oldNumber].Replace(" ", "_")] = List[oldNumber][choice];
                    Utility.Mod(ref number, Names.Count);
                    choice = List[number].IndexOf(Settings.Default[Names[number].Replace(" ", "_")]);
                }
                else Utility.Mod(ref choice, List[number].Count);
                '>'.WriteAt(left + spacing * choice, top + number);
                oldNumber = number;
            }
        }
    }
}