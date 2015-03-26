using System;
using System.Collections.Generic;
using System.Linq;

namespace GuardsOfAetheria
{
    internal class Utility
    {
        public int SelectOption(string[] options, bool showMenu = false)
        {
            var menuSelected = 1;
            var pageScroll = -1;
            if (Options.Instance.CurrentSettings[0] == Options.Settings.Pages) pageScroll = 0;
            if (Options.Instance.CurrentSettings[0] == Options.Settings.Scroll) pageScroll = 1;
            var startLine = Console.CursorTop;
            var numberOfLines = 22 - startLine;
            var pages = (options.Length - (options.Length % numberOfLines)) / numberOfLines;
            var pageNumber = 1;
            var scroll = options.Length - 1 > numberOfLines;
            var possibleOptions = options.Length - (numberOfLines*(pageNumber - 1));
            if (numberOfLines < possibleOptions) possibleOptions = numberOfLines;
            for (var i = 0; i < possibleOptions; i++)
            {
                Console.SetCursorPosition(2, startLine + i + 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(2, startLine + i + 1);
                Console.Write(options[i]);
            }
            if (pages > 1 && pageScroll == 0)
            {
                Console.SetCursorPosition(50, startLine);
                Console.Write(pageNumber);
                Console.SetCursorPosition(52, startLine);
                Console.Write("/{0}", pages);
            }
            if (showMenu)
            {
                Console.SetCursorPosition(10, 23);
                Console.Write("[M]enu");
            }
            var lastPage = false;
            var nextPage = false;
            while (true)
            {
                Console.SetCursorPosition(0, menuSelected + startLine);
                Console.Write('>');
                //TODO: change appearance of arrow? >/-
                while (true)
                {
                    var input = Console.ReadKey(true).Key;
                    Console.SetCursorPosition(0, menuSelected + startLine);
                    Console.Write(' ');
                    switch (input)
                    {
                        case ConsoleKey.UpArrow:
                            menuSelected--;
                            break;
                        case ConsoleKey.DownArrow:
                            menuSelected++;
                            break;
                        case ConsoleKey.M:
                            if (showMenu) PlayerMenu();
                            break;
                        case ConsoleKey.Enter:
                            return (menuSelected + (numberOfLines * (pageNumber - 1)));
                    }
                    if (scroll)
                    {
                        if (menuSelected < 1) lastPage = true;
                        if (menuSelected > possibleOptions) nextPage = true;
                    } else {
                        if (menuSelected < 1) menuSelected = possibleOptions;
                        if (menuSelected > possibleOptions) menuSelected = 1;
                    }
                    if (lastPage || nextPage)
                    {
                        if (pageScroll == 0)
                        {
                            possibleOptions = options.Length - (numberOfLines*(pageNumber - 1));
                            if (numberOfLines < possibleOptions) possibleOptions = numberOfLines;
                            if (lastPage)
                            {
                                menuSelected = possibleOptions;
                                pageNumber--;
                            }
                            if (nextPage)
                            {
                                menuSelected = 1;
                                pageNumber++;
                            }
                            if (pageNumber < 1) pageNumber = pages;
                            if (pageNumber > pages) pageNumber = 1;
                            if (pages > 1)
                            {
                                Console.SetCursorPosition(50, startLine);
                                Console.Write("{0}/{1}", pageNumber, pages);
                            }
                        }
                        if (pageScroll == 1)
                        {
                            if (pageNumber < 1) pageNumber = options.Length;
                            if (pageNumber > options.Length) pageNumber = 1;
                            if (lastPage)
                            {
                                menuSelected = 1;
                                pageNumber--;
                                lastPage = false;
                            }
                            if (nextPage)
                            {
                                menuSelected = possibleOptions;
                                pageNumber++;
                                nextPage = false;
                            }
                        }
                        for (var i = 0; i < possibleOptions; i++)
                        {
                            Console.SetCursorPosition(2, startLine + i + 1);
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(2, startLine + i + 1);
                            if (pageScroll == 0) Console.Write(options[i + (numberOfLines*(pageNumber - 1))]);
                            if (pageScroll != 1) continue;
                            var currentNumber = pageNumber + i;
                            if (currentNumber - 1 > options.Length) currentNumber = currentNumber - options.Length;
                            Console.Write(options[currentNumber]);
                        }
                    }
                    Console.SetCursorPosition(0, menuSelected + startLine);
                    Console.Write('>');
                }
            }
        }

        public void PlayerMenu()
        {
            Console.Clear();
            Console.WriteLine("Inventory");
            SelectOption(Player.Instance.InventoryNameAll);
            //TODO: Add more options
        }

        public void CalculateWeaponStats()
        {
            //TODO: everything 
        }

        public void UpdateExp()
        {
            int expNeeded = Convert.ToInt16(Math.Pow(1.05, Player.Instance.Level)*1000);
            if (Player.Instance.Experience < expNeeded) return;
            Player.Instance.Level++;
            Player.Instance.Experience = Player.Instance.Experience - expNeeded;
        }

        public static void PrioritiseInventoryItems()
        {
            Player.Instance.InventoryOld = Player.Instance.Inventory;
            //TODO: integer, 10^5? * most important etc, quicksort
        }

        public static int SpaceLeft()
        {
            var spaceLeft = Player.Instance.InventorySpace;
            for (var i = 1; i < 51; i++)
            {
                if (Player.Instance.Inventory[1][i][1] != 0) spaceLeft--;
                if (Player.Instance.Inventory[2][i][1] != 0) spaceLeft -= Player.Instance.Inventory[1][i][7];
                if (Player.Instance.Inventory[3][i][1] != 0) spaceLeft--;
                if (Player.Instance.Inventory[4][i][1] != 0) spaceLeft--;
            }
            return spaceLeft;
        }

        public int IntParseFast(string value) { return value.Aggregate(0, (current, t) => 10 * current + (t - 48)); } //from http://www.dotnetperls.com/int-parse

        public void WordWrap(string paragraph) //Remember to SetCursorPosition() before this
        {
            var words = paragraph.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            var left = Console.CursorLeft;
            var lines = new string[23];
            var j = 0;
            var maxChars = 79 - left;
            for (var i = 0; i < 23; i++)
            {
                var chars = 0;
                lines[i] = "";
                while (true) //TODO: optimise
                {
                    chars += words[j].Length + 1;
                    if (chars < maxChars) lines[i] += words[j] + " ";
                    else break;
                    j++;
                    if (j > words.Length - 1) break;
                }
                Console.SetCursorPosition(left, Console.CursorTop);
                if (!String.IsNullOrEmpty(lines[i])) Console.WriteLine(lines[i]);
                if (j > words.Length - 1) break;
            }
        }

        public int[] Spend(string[] text, string[] names, int[] currentItems, int[] newItems, int[] cost, int currency,
            int arrowPosition, int[] value = null)
        {
            Console.Clear();
            var menuSelected = 1;
            SpendGraphics(text, names, currency, currentItems, arrowPosition);
            Console.SetCursorPosition(arrowPosition, 2); //TODO: Change 2 to var later (after word wrap)
            Console.Write('>');
            while (true)
            {
                var totalItems = new int[currentItems.Length];
                var input = Console.ReadKey().Key;
                Console.SetCursorPosition(arrowPosition, menuSelected + 1); //change here too
                Console.Write(' ');

                switch (input)
                {
                    case ConsoleKey.UpArrow:
                        menuSelected--;
                        break;
                    case ConsoleKey.DownArrow:
                        menuSelected++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (newItems[menuSelected - 1] > 0)
                        {
                            newItems[menuSelected - 1]--;
                            currency += cost[menuSelected - 1];
                        }
                        else if (value != null && currentItems[menuSelected - 1] + newItems[menuSelected - 1] > 0)
                        {
                            newItems[menuSelected - 1]--;
                            currency += value[menuSelected - 1];
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (currency > 0)
                        {
                            newItems[menuSelected - 1]++;
                            currency -= cost[menuSelected - 1];
                        }
                        else if (value != null && newItems[menuSelected - 1] < 0)
                        {
                            newItems[menuSelected - 1]++;
                            currency -= value[menuSelected - 1];
                        }
                        break;
                    case ConsoleKey.Enter:
                        for (var i = 0; i < currentItems.Length; i++) currentItems[i] += newItems[i];
                        return currentItems;
                }
                if (input == ConsoleKey.RightArrow || input == ConsoleKey.LeftArrow)
                {
                    for (var i = 0; i < currentItems.Length; i++) totalItems[i] = currentItems[i] + newItems[i];
                    SpendGraphics(text, names, currency, totalItems, arrowPosition);
                }

                if (menuSelected < 1) menuSelected = currentItems.Length;
                else if (menuSelected > currentItems.Length) menuSelected = 1;
                Console.SetCursorPosition(arrowPosition, menuSelected + 1); //and here
                Console.Write('>');
            }
        }

        //TODO: possibly optimise by putting it in spend()
        private void SpendGraphics(string[] text, string[] names, int currency, int[] items, int arrowPosition)
        {
            Console.Clear(); //TODO: see if this needs to be removed when shops are implemented
            WordWrap(text[0]);
            for (var i = 0; i < items.Length; i++)
            {
                Console.SetCursorPosition(0, Console.CursorTop + 1);
                Console.Write(names[i]);
                Console.SetCursorPosition(arrowPosition + 2, Console.CursorTop);
                Console.Write(items[i]);
            }
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            Console.WriteLine("{0} {1} {2}", text[1], currency, text[2]);
        }
    }

    internal class Quicksort //From http://www.softwareandfinance.com/CSharp/QuickSort_Recursive.html
    {
        public static int Partition(int[] numbers, int left, int right)
        {
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

        public static void QuickSortRecursive(int[] arr, int left, int right)
        {
            while (true)
            {
                // For Recursion
                if (left >= right) return;
                var pivot = Partition(arr, left, right);

                if (pivot > 1) QuickSortRecursive(arr, left, pivot - 1);

                if (pivot + 1 < right)
                {
                    left = pivot + 1;
                    continue;
                }
                break;
            }
        }
    }
}