using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class Utility
    {
        using System;
using System.Linq;
namespace GuardsOfAetheria
{
    internal class Utility
    {
        //TODO: extension methods
        public int SelectOption(string[] options, bool showMenu = false)
        {
            var menuSelected = 1; bool pScroll; var startLine = Console.CursorTop; var pageNumber = 1;
            var numberOfLines = 22 - startLine;
            var pages = (options.Length - (options.Length % numberOfLines)) / numberOfLines;
            var scroll = options.Length - 1 > numberOfLines;
            var possibleOptions = options.Length - (numberOfLines * (pageNumber - 1));
            switch (Options.Instance.CurrentSettings[0])
            {
                case Options.Settings.Pages: pScroll = false; break;
                case Options.Settings.Scroll: pScroll = true; break;
                default: throw new Exception("Someone tampered with the settings >:(");
            }
            //TODO: detect cheatengine
            if (numberOfLines < possibleOptions) possibleOptions = numberOfLines;
            for (var i = 0; i < possibleOptions; i++)
            {
                Console.SetCursorPosition(2, startLine + i + 1); Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(2, startLine + i + 1); Console.Write(options[i]);
            }
            if (pages > 1 && !pScroll)
            {
                Console.SetCursorPosition(50, startLine); Console.Write(pageNumber);
                Console.CursorLeft = 52; Console.Write("/{0}", pages);
            }
            if (showMenu)
            {
                Console.SetCursorPosition(10, 23); Console.Write("[M]enu");
            }
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
                        case ConsoleKey.M: if (showMenu) PlayerMenu(); break;
                        case ConsoleKey.Enter: return (menuSelected + (numberOfLines * (pageNumber - 1)));
                    }
                    if (scroll)
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
                        if (!pScroll)
                        {
                            possibleOptions = options.Length - (numberOfLines * (pageNumber - 1));
                            if (numberOfLines < possibleOptions) possibleOptions = numberOfLines;
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
                            Console.Write(!pScroll ? options[i + (numberOfLines*(pageNumber - 1))] : options[(pageNumber + i)%options.Length]);
                        }
                    }
                    Console.SetCursorPosition(0, menuSelected + startLine); Console.Write('>');
                }
            }
        }

        public void PlayerMenu()
        {
            Console.Clear(); Console.WriteLine("Inventory");
            SelectOption(Player.Instance.InventoryNameAll);
            //TODO: Add more options
        }

        public void CalculateWeaponStats()
        {
            //TODO: everything 
        }

        public void UpdateExp()
        {
            var expNeeded = Convert.ToInt32(Math.Pow(1.05, Player.Instance.Level) * 1000);
            if (Player.Instance.Experience >= expNeeded)
                Player.Instance.Level++; Player.Instance.Experience -= expNeeded;
        }

        public static void PrioritiseInventoryItems()
        {
            Player.Instance.InventoryOld = Player.Instance.Inventory;
            //TODO: label by importance, .Aggregate() by importance, Quicksort;
        }

        public int SpaceLeft()
        {
            var spaceLeft = Player.Instance.InventorySpace;
            //TODO: more compartments, large compartments
            for (var i = 0; i < 50; i++)
            {
                if (Player.Instance.Inventory[1][i][1] != 0) spaceLeft--;
                if (Player.Instance.Inventory[2][i][1] != 0) spaceLeft -= Player.Instance.Inventory[1][i][7];
                if (Player.Instance.Inventory[3][i][1] != 0) spaceLeft--;
                if (Player.Instance.Inventory[4][i][1] != 0) spaceLeft--;
            } return spaceLeft;
        }

        public int IntParseFast(string value) { return value.Aggregate(0, (current, t) => 10 * current + (t - 48)); } //from http://www.dotnetperls.com/int-parse

        public int WordWrap(string paragraph)
        { //int maxchars = 79?
            var words = paragraph.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            var left = Console.CursorLeft; var lines = new string[23]; var j = 0; var maxChars = 79 - left; var numLines = 0;
            for (var i = 0; i < 23 && j < words.Length; i++)
            {
                var chars = 0; lines[i] = "";
                while (j < words.Length)
                { //TODO: optimise
                    chars += words[j].Length + 1;
                    if (chars < maxChars) lines[i] += words[j] + " "; else break; j++;
                }
                Console.SetCursorPosition(left, Console.CursorTop);
                lines[i].Remove(lines[i].Length - 1);
                if (!String.IsNullOrEmpty(lines[i])) Console.WriteLine(lines[i]); else numLines++;
            } return numLines;
        }

        public int[] Spend(string[] text, string[] names, int[] items, int[] newItems, int[] cost, int currency, int[] value = null)
        {
            var arrowPosition = Console.CursorLeft; Console.Clear();
            var option = 0; var numLines = WordWrap(text[0]);
            for (var i = 0; i < items.Length; i++)
            {
                Console.SetCursorPosition(0, Console.CursorTop + 1);
                Console.Write(names[i]);
                Console.SetCursorPosition(arrowPosition + 2, Console.CursorTop);
                Console.Write(items[i]);
            }
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            Console.WriteLine("{0} {1} {2}", text[1], currency, text[2]);
            var left = numLines + 2;
            Console.SetCursorPosition(arrowPosition, numLines + 2);
            Console.Write('>');
            while (true)
            {
                var totalItems = new int[items.Length];
                var input = Console.ReadKey().Key;
                Console.SetCursorPosition(arrowPosition, left);
                Console.Write(' ');
                switch (input)
                {
                    case ConsoleKey.UpArrow: option--; break;
                    case ConsoleKey.DownArrow: option++; break;
                    case ConsoleKey.LeftArrow:
                        if (newItems[option] > 0)
                        {
                            newItems[option]--;
                            currency += cost[option];
                        }
                        else if (value != null && items[option] + newItems[option] > 0)
                        {
                            newItems[option]--;
                            currency += value[option];
                        } break;
                    case ConsoleKey.RightArrow:
                        if (currency > 0)
                        {
                            newItems[option]++;
                            currency -= cost[option];
                        }
                        else if (value != null && newItems[option] < 0)
                        {
                            newItems[option]++;
                            currency -= value[option];
                        } break;
                    case ConsoleKey.Enter: return totalItems;
                }
                left = option + numLines + 2;
                if (input == ConsoleKey.RightArrow || input == ConsoleKey.LeftArrow)
                {
                    for (var i = 0; i < items.Length; i++) totalItems[i] = items[i] + newItems[i];
                    Console.SetCursorPosition(arrowPosition + 2, left);
                    Console.Write(totalItems[option]);
                }
                if (option < 0) option = items.Length - 1;
                else if (option > items.Length - 1) option = 0;
                Console.SetCursorPosition(arrowPosition, left);
                Console.Write('>');
            }
        }
    }
    internal class Quicksort
    { //From http://www.softwareandfinance.com/CSharp/QuickSort_Recursive.html
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
    }
}
