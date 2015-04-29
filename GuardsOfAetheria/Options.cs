using System;
using System.Collections.Generic;
using GuardsOfAetheria.Properties;
using Improved;

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

        public static void Load()
        {
            Consoles.ScrollingIsContinuous = Settings.Default.Menu_scrolling;
            Consoles.PageNumIsVisible = Settings.Default.Page_numbers;
        }

        public static void Change()
        {
            //TODO: scrolling
            Console.Clear();
            Console.WriteLine("Options\n");
            var top = Console.CursorTop; const int left = 38; const int spacing = 12; var number = 0; var oldNumber = 0;
            Console.CursorLeft = 0;
            foreach (var name in Names) Console.WriteLine(name);
            for (var i = 0; i < Names.Count; i++) for (var j = 0; j < List[i].Count; j++)
            { Strings[i][j].WriteAt(left + 2 + spacing * j, top + i); }
            var choice = List[number].IndexOf(Settings.Default[Names[number].Replace(" ", "_")]);
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
                }
                if (input == ConsoleKey.DownArrow || input == ConsoleKey.UpArrow)
                {
                    Settings.Default[Names[oldNumber].Replace(" ", "_")] = List[oldNumber][choice];
                    Maths.Mod(ref number, Names.Count);
                    choice = List[number].IndexOf(Settings.Default[Names[number].Replace(" ", "_")]);
                }
                else Maths.Mod(ref choice, List[number].Count);
                '>'.WriteAt(left + spacing * choice, top + number);
                oldNumber = number;
            }
        }
    }
}