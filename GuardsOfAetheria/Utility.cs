using System;
using System.Collections.Generic;
using System.Linq;

namespace GuardsOfAetheria
{
    public static class Utility
    {
        public static int Mod(int a, int b) { return (a %= b + 1) < 0 ? a + b + 1 : a; }

        public static int SelectOption(this List<string> options, bool menuIsVisible = false)
        {
            var left = Console.CursorLeft; var option = 0; var top = Console.CursorTop + 1; var page = 0; var numLines = 22 - top;
            //TODO: option to jump to start for continuous scrolling?
            var possibleOptions = options.Count - (numLines * page) - 1;
            var pages = options.Count / numLines - 1;
            var scrollIsEnabled = options.Count > numLines;
            var scrollingIsContinuous = (bool)Options.Instance.Current[0];
            //TODO: prevent cheating (when i find out how)
            if (numLines < possibleOptions) possibleOptions = numLines;
            for (var i = 0; i < possibleOptions + 1; i++)
            {
                //TODO: adjust when GUI created
                Console.SetCursorPosition(left + 2, top + i); Console.Write(options[i].PadRight(Console.WindowWidth - 1));
            }
            if (pages > 0 && !scrollingIsContinuous)
            {
                Console.SetCursorPosition(50, top); Console.Write(page + 1);
                Console.CursorLeft = 52; Console.Write("/{0}", pages + 1);
                //TODO: un-50-ify
            }
            if (menuIsVisible) { Console.SetCursorPosition(10, 23); Console.Write("[M]enu"); }
            while (true)
            {
                Console.SetCursorPosition(left, option + top); Console.Write('>');
                //TODO: change appearance of arrow? >/-
                var pageIncrement = 0;
                while (true)
                {
                    var input = Console.ReadKey(true).Key;
                    Console.SetCursorPosition(left, option + top); Console.Write(' ');
                    switch (input)
                    {
                        case ConsoleKey.UpArrow: option--; break;
                        case ConsoleKey.DownArrow: option++; break;
                        case ConsoleKey.M: if (menuIsVisible) PlayerMenu(); break;
                        case ConsoleKey.Enter: return (option + (numLines * page));
                    }
                    if (scrollIsEnabled) { if (option < 0) pageIncrement = -1; else if (option > possibleOptions + 1) pageIncrement = 1; }
                    else Mod(option, possibleOptions);
                    if (pageIncrement != 0)
                    {
                        if (!scrollingIsContinuous)
                        {
                            possibleOptions = options.Count - (numLines * page);
                            if (numLines < possibleOptions) possibleOptions = numLines;
                            page += pageIncrement;
                            option = Mod(option, possibleOptions);
                            page = Mod(page, pages);
                            if (pages > 0)
                            {
                                Console.SetCursorPosition(50, top); Console.Write(page + 1);
                                Console.CursorLeft = 52; Console.Write("/{0}", pages + 1);
                            }
                        }
                        else
                        {
                            page = (page %= pages + 1) < 0 ? page + pages + 1 : page;
                            page += pageIncrement;
                            option = (option %= possibleOptions + 1) < 0 ? option + possibleOptions + 1 : option;
                            pageIncrement = 0;
                        }
                        for (var i = 0; i < possibleOptions + 1; i++)
                        {
                            Console.SetCursorPosition(left + 2, top + i);
                            Console.Write(scrollingIsContinuous ? options[(page + i + 1) % options.Count].PadRight(Console.WindowWidth - 1) : options[i + (numLines * page)].PadRight(Console.WindowWidth - 1));
                        }
                    }
                    Console.SetCursorPosition(left, option + top); Console.Write('>');
                }
            }
        }

        public static void PlayerMenu()
        {
            Console.Clear(); Console.WriteLine("Inventory");
            //SelectOption(Players.You.InventoryName);
            //TODO: Add more options, centre text
        }
        //TODO: move to inventory.cs

        public static void PrioritiseInventoryItems()
        {
            Players.You.InventoryOld = Players.You.Inventory;
            //TODO: label by importance, .Aggregate() by importance, Quicksort;
        }

        public static int SpaceLeft()
        {
            var spaceLeft = Players.You.InventorySpace;
            //TODO: more compartments, large compartments
            //TODO: make one-dimensional
            for (var i = 0; i < 50; i++)
            {
                if (Players.You.Inventory[i][1] == 0) continue;
                if (Players.You.Inventory[i][0] == 2) spaceLeft -= Players.You.Inventory[i][7];
                else spaceLeft--;
            }
            return spaceLeft;
        }

        public static int IntParseFast(string value) { return value.Aggregate(0, (current, t) => 10 * current + (t - 48)); }

        public static int WordWrap(string paragraph)
        { //int maxchars = 79?
            var words = paragraph.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
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

        public static List<int> Spend(List<string> text, List<string> names, List<int> items, List<int> newItems, List<int> cost, int currency, List<int> value = null)
        {
            //TODO: test scrolling
            var page = 0; var top = Console.CursorTop + 1; var pageIncrement = 0; var maxLines = 22 - top;
            var possibleOptions = names.Count - (maxLines * page) - 1;
            var pages = names.Count / maxLines - 1;
            var scrollIsEnabled = names.Count > maxLines;
            var scrollingIsContinuous = (bool) Options.Instance.Current[0];
            if (maxLines < possibleOptions) possibleOptions = maxLines;
            if (pages > 0 && !scrollingIsContinuous)
            {
                Console.SetCursorPosition(50, top); Console.Write(page + 1);
                Console.CursorLeft = 52; Console.Write("/{0}", pages + 1);
                //TODO: un-50-ify
            }
            var arrowPosition = Console.CursorLeft; Console.Clear();
            var option = 0; var numLines = WordWrap(text[0]); var maxDigits = (int)Math.Log10(items.Max() + currency) + 1; var maxCurrencyLength = (int)Math.Log10(currency) + text[1].Length + text[2].Length + text[3].Length + 2;
            if (text.Count > 4) maxCurrencyLength += Math.Max(0, text[4].Length - text[2].Length);
            if (text.Count > 5) maxCurrencyLength = Math.Max(maxCurrencyLength, text[5].Length);
            for (var i = 0; i < items.Count; i++)
            {
                Console.SetCursorPosition(0, ++Console.CursorTop); Console.Write(names[i]);
                Console.CursorLeft = arrowPosition + 2; Console.Write(items[i]);
            }
            var line = numLines + 1;
            var input = default(ConsoleKey);
            var amountIsChanged = true;
            var totalItems = items.ToList();
            while (true)
            {
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
                    for (var i = 0; i < items.Count; i++) totalItems[i] = items[i] + newItems[i];
                    Console.SetCursorPosition(arrowPosition + 2, line);
                    Console.Write(totalItems[option].ToString().PadRight(maxDigits));
                    Console.SetCursorPosition(0, numLines + items.Count + 1);
                    switch (currency)
                    {
                        //TODO: improve
                        case 1:
                            Console.WriteLine("{0}{1}{2}".PadRight(maxCurrencyLength), text[1], currency, text.Count > 5 ? text[4] : text[2] + text[3]);
                            break;
                        case 0:
                            if (text.Count > 5) Console.WriteLine(text[5]);
                            else Console.WriteLine("{0}no{1}s{2}".PadRight(maxCurrencyLength), text[1], text[2], text[3]); break;
                        default: Console.WriteLine("{0}{1}{2}s{3}".PadRight(maxCurrencyLength), text[1], currency, text[2], text[3]); break;
                    }
                }
                if (scrollIsEnabled) { if (option < 0) pageIncrement = -1; else if (option > possibleOptions + 1) pageIncrement = 1; }
                else option = Mod(option, possibleOptions);
                if (pageIncrement != 0)
                {
                    if (!scrollingIsContinuous)
                    {
                        possibleOptions = names.Count - (numLines * page);
                        if (numLines < possibleOptions) possibleOptions = numLines;
                        page += pageIncrement;
                        option = Mod(option, possibleOptions);
                        page = (page %= pages + 1) < 0 ? page + pages + 1 : page;
                        if (pages > 0)
                        {
                            Console.SetCursorPosition(50, top); Console.Write(page + 1);
                            Console.CursorLeft = 52; Console.Write("/{0}", pages + 1);
                        }
                    }
                    else
                    {
                        page = (page %= pages + 1) < 0 ? page + pages + 1 : page;
                        page += pageIncrement;
                        option = (option %= possibleOptions + 1) < 0 ? option + possibleOptions + 1 : option;
                        pageIncrement = 0;
                    }
                    for (var i = 0; i < possibleOptions + 1; i++)
                    {
                        Console.SetCursorPosition(arrowPosition + 2, top + i);
                        Console.Write(scrollingIsContinuous ? totalItems[(page + i + 1) % totalItems.Count].ToString().PadRight(Console.WindowWidth - 1) : totalItems[i + (numLines * page)].ToString().PadRight(Console.WindowWidth - 1));
                    }
                }

                if (input == ConsoleKey.UpArrow || input == ConsoleKey.DownArrow || input == default(ConsoleKey))
                {
                    Console.SetCursorPosition(arrowPosition, line); Console.Write(' ');
                    Console.SetCursorPosition(arrowPosition, line = option + numLines + 1); Console.Write('>');
                }
                input = Console.ReadKey(true).Key;
                amountIsChanged = false;
            }
        }
    }

    internal class Quicksort
    {
        public static int Partition(List<int> numbers, int left, int right)
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
        public static void Qsort(List<int> arr, int left, int right)
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
