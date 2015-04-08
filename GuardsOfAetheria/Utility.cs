using System;
using System.Linq;

namespace GuardsOfAetheria
{
    public static class Utility
    {
        public static int SelectOption(this string[] options, bool menuIsVisible = false)
        {
            var menuSelected = 1; bool scrollingIsContinuous; var startLine = Console.CursorTop; var pageNumber = 1;
            //TODO: option to jump to start for continuous scrolling?
            var numLines = 22 - startLine;
            var pages = options.Length / numLines;
            var scrollIsEnabled = options.Length > numLines;
            var possibleOptions = options.Length - (numLines * (pageNumber - 1));
            switch (Options.Instance.Current[0])
            {
                case Options.Settings.Pages: scrollingIsContinuous = false; break;
                case Options.Settings.Scroll: scrollingIsContinuous = true; break;
                default: throw new Exception("Someone tampered with the settings >:(");
                //TODO: is exception necessary? FixStuff project - options, game?
            }
            //TODO: detect cheatengine
            if (numLines < possibleOptions) possibleOptions = numLines;
            for (var i = 0; i < possibleOptions; i++)
            {
                //TODO: adjust when GUI created
                Console.SetCursorPosition(2, startLine + i + 1); Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(2, startLine + i + 1); Console.Write(options[i]);
            }
            if (pages > 1 && !scrollingIsContinuous)
            {
                Console.SetCursorPosition(50, startLine); Console.Write(pageNumber);
                Console.CursorLeft = 52; Console.Write("/{0}", pages);
            }
            if (menuIsVisible) { Console.SetCursorPosition(10, 23); Console.Write("[M]enu"); }
            while (true)
            {
                Console.SetCursorPosition(0, menuSelected + startLine); Console.Write('>');
                //TODO: change appearance of arrow? >/-
                var pageIncrement = 0;
                while (true)
                {
                    var input = Console.ReadKey(true).Key;
                    Console.SetCursorPosition(0, menuSelected + startLine); Console.Write(' ');
                    switch (input)
                    {
                        case ConsoleKey.UpArrow: menuSelected--; break;
                        case ConsoleKey.DownArrow: menuSelected++; break;
                        case ConsoleKey.M: if (menuIsVisible) PlayerMenu(); break;
                        case ConsoleKey.Enter: return (menuSelected + (numLines * (pageNumber - 1)));
                    }
                    if (scrollIsEnabled)
                    {
                        if (menuSelected < 1) pageIncrement--;
                        else if (menuSelected > possibleOptions) pageIncrement++;
                    }
                    else
                    {
                        if (menuSelected < 1) menuSelected = possibleOptions;
                        else if (menuSelected > possibleOptions) menuSelected = 1;
                        //TODO: modulo?
                    }
                    if (pageIncrement != 0)
                    {
                        if (!scrollingIsContinuous)
                        {
                            possibleOptions = options.Length - (numLines * (pageNumber - 1));
                            if (numLines < possibleOptions) possibleOptions = numLines;
                            switch (pageIncrement)
                            {
                                case -1: menuSelected = possibleOptions; pageNumber--; break;
                                case 1: menuSelected = 1; pageNumber++; break;
                            }
                            if (pageNumber < 1) pageNumber = pages;
                            else if (pageNumber > pages) pageNumber = 1;
                            if (pages > 1)
                            {
                                Console.SetCursorPosition(50, startLine); Console.Write(pageNumber);
                                Console.CursorLeft = 52; Console.Write("/{0}", pages);
                            }
                        }
                        else
                        {
                            if (pageNumber < 1) pageNumber = options.Length;
                            else if (pageNumber > options.Length) pageNumber = 1;
                            switch (pageIncrement)
                            {
                                case -1: menuSelected = 1; pageNumber--; break;
                                case 1: menuSelected = possibleOptions; pageNumber++; break;
                            }
                            pageIncrement = 0;
                        }
                        for (var i = 0; i < possibleOptions; i++)
                        {
                            Console.SetCursorPosition(2, startLine + i + 1); Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(2, startLine + i + 1);
                            Console.Write(!scrollingIsContinuous ? options[i + (numLines*(pageNumber - 1))] : options[(pageNumber + i)%options.Length]);
                        }
                    }
                    Console.SetCursorPosition(0, menuSelected + startLine); Console.Write('>');
                }
            }
        }

        public static void PlayerMenu()
        {
            Console.Clear(); Console.WriteLine("Inventory");
            SelectOption(Player.Instance.InventoryName);
            //TODO: Add more options
        }
        //TODO: move to inventory;
        public static void PrioritiseInventoryItems()
        {
            Player.Instance.InventoryOld = Player.Instance.Inventory;
            //TODO: label by importance, .Aggregate() by importance, Quicksort;
        }

        public static int SpaceLeft()
        {
            var spaceLeft = Player.Instance.InventorySpace;
            //TODO: more compartments, large compartments
            //TODO: make one-dimensional
            for (var i = 0; i < 50; i++)
            {
                if (Player.Instance.Inventory[i][1] == 0) continue;
                if (Player.Instance.Inventory[i][0] == 2) spaceLeft -= Player.Instance.Inventory[i][7];
                else spaceLeft--;
            }
            return spaceLeft;
        }

        public static int IntParseFast(string value) { return value.Aggregate(0, (current, t) => 10 * current + (t - 48)); } //from http://www.dotnetperls.com/int-parse

        public static int WordWrap(string paragraph)
        { //int maxchars = 79?
            var words = paragraph.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            var left = Console.CursorLeft; var lines = new string[23]; var j = 0; var maxChars = 79 - left; var numLines = 0;
            for (var i = 0; i < 23 && j < words.Length; i++)
            {
                var chars = 0; lines[i] = "";
                while (j < words.Length)
                { //TODO: optimise
                    chars += words[j].Length + 1; if (chars >= maxChars) break;
                    lines[i] += words[j] + " "; j++;
                }
                Console.SetCursorPosition(left, Console.CursorTop);
                lines[i].Remove(lines[i].Length - 1);
                if (String.IsNullOrEmpty(lines[i])) continue;
                Console.WriteLine(lines[i]); numLines++;
                //TODO: stringbuilder?
            } return numLines;
        }

        public static int[] Spend(string[] text, string[] names, int[] items, int[] newItems, int[] cost, int currency, int[] value = null)
        {
            var arrowPosition = Console.CursorLeft; Console.Clear();
            var option = 0; var numLines = WordWrap(text[0]); var maxDigits = (int) Math.Log10(items.Max() + currency) + 1; var maxCurrencyLength = (int) Math.Log10(currency) + text[1].Length + Math.Max(text[2].Length, text[3].Length) + 1;
            if (text.Length > 4) maxCurrencyLength = Math.Max(maxCurrencyLength, text[4].Length);
            for (var i = 0; i < items.Length; i++)
            {
                Console.SetCursorPosition(0, ++Console.CursorTop);
                Console.Write(names[i]);
                Console.CursorLeft = arrowPosition + 2;
                Console.Write(items[i]);
            }
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            Console.WriteLine("{0}{1}{2}", text[1], currency, text[2]);
            var line = numLines + 1;
            Console.SetCursorPosition(arrowPosition, line);
            Console.Write('>');
            while (true)
            {
                var totalItems = new int[items.Length];
                var input = Console.ReadKey().Key;
                var amountIsChanged = false;
                switch (input)
                {
                    case ConsoleKey.UpArrow: option--; break;
                    case ConsoleKey.DownArrow: option++; break;
                    case ConsoleKey.LeftArrow:
                        if (newItems[option] > 0) { newItems[option]--; currency += cost[option]; amountIsChanged = true; }
                        else if (value != null && items[option] + newItems[option] > 0) { newItems[option]--; currency += value[option]; amountIsChanged = true; } break;
                    case ConsoleKey.RightArrow:
                        if (currency > 0) { newItems[option]++; currency -= cost[option]; amountIsChanged = true; }
                        else if (value != null && newItems[option] < 0) { newItems[option]++; currency -= value[option]; amountIsChanged = true; } break;
                    case ConsoleKey.Enter: return totalItems;
                }
                if (amountIsChanged)
                {
                    for (var i = 0; i < items.Length; i++) totalItems[i] = items[i] + newItems[i];
                    Console.SetCursorPosition(arrowPosition + 2, line);
                    Console.Write(new string(' ', maxDigits));
                    Console.SetCursorPosition(arrowPosition + 2, line);
                    Console.Write(totalItems[option]);
                    Console.SetCursorPosition(0, numLines + items.Length + 1);
                    Console.Write(new string(' ', maxCurrencyLength));
                    Console.SetCursorPosition(0, numLines + items.Length + 1);
                    switch (currency)
                    {
                        case 1: Console.WriteLine("{0}{1}{2}", text[1], currency, text[3]); break;
                        case 0:
                            if (text.Length > 4) Console.WriteLine(text[4]);
                            else Console.WriteLine("{0}no{1}", text[1], text[2]); break;
                        default: Console.WriteLine("{0}{1}{2}", text[1], currency, text[2]); break;
                    }
                    //TODO: optimise - less repetition
                }
                if (option < 0) option = items.Length - 1;
                else if (option > items.Length - 1) option = 0;
                if (input != ConsoleKey.UpArrow && input != ConsoleKey.DownArrow) continue;
                Console.SetCursorPosition(arrowPosition, line); Console.Write(' ');
                line = option + numLines + 1;
                Console.SetCursorPosition(arrowPosition, line); Console.Write('>');
            }
        }
    }
    internal class Quicksort
    { //From http://www.softwareandfinance.com/CSharp/QuickSort_Recursive.html
        public static int Partition(int[] numbers, int left, int right)
        { //TODO: adapt to mmultidimensional arrays
            var pivot = numbers[left];
            while (true)
            {
                while (numbers[left] < pivot) left++;
                while (numbers[right] > pivot) right--;
                if (left < right)
                {
                    var temp = numbers[right];
                    numbers[right] = numbers[left];
                    numbers[left] = temp;
                }
                else return right;
            }
        }
        public static void Qsort(int[] arr, int left, int right)
        {
            while (true)
            {
                if (left >= right) return;
                var pivot = Partition(arr, left, right);
                if (pivot > 1) Qsort(arr, left, pivot - 1);
                if (pivot + 1 < right) { left = pivot + 1; continue; } break;
            }
        }
    }
}
