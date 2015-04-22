using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GuardsOfAetheria
{
    public class Frame
    {
        public int TopRightX;
        public int TopRightY;
        public int Height;
        public int Width;
        public List<string> Buffer;
        //public List<List<int>> FrameTopRight = new List<List<int>>();
        //public List<List<int>> FrameDimensions = new List<List<int>>();
        
        public Frame(int topRightCornerY, int topRightCornerX, int height, int width)
        {
            TopRightX = topRightCornerX;
            TopRightY = topRightCornerY;
            Height = height;
            Width = width;
            Frames.FrameList.Add(this);
        }

        public void Write(string input, bool wordWrap = false)
        {
            int numLines;
            Buffer.AddRange(Utility.WordWrap(input, out numLines, Width, wordWrap));
            var temp = new string[Height];
            Buffer.CopyTo(temp, Buffer.Count - Height);
            Buffer = temp.ToList();
            //todo: buffer.remove
        }
        //TODO: writeat etc, copy from;
        //TODO: dev mode, devtools.cs?
    }

    public static class Frames
    {
        public static List<Frame> FrameList; 
        
        public static void ShowFrameNumbers()
        {
            for (var i = 0; i < FrameList.Count; i++) i.WriteAt(FrameList[i].TopRightX, FrameList[i].TopRightY);
        }
    }
    
    public static class Utility
    {
        public static bool ScrollingIsContinuous, ScrollIsEnabled, PageNumIsVisible;
        public static int Left, Option, Top, Page, MaxLines, PagePlus, TotalOptions, PossibleOptions, Pages, Index;
        
        //TODO: rearrange methods, readability
        public static void Initiate(int arrayLength) { Initiate(arrayLength, Console.WindowHeight - 4); }
        //TODO: footerheight

        public static void Initiate(int arrayLength, int lastLine)
        {
            Option = 0; Page = -1; PagePlus = 1; Index = 0;
            Left = Console.CursorLeft; Top = Console.CursorTop + 1; 
            MaxLines = lastLine - Top; 
            TotalOptions = arrayLength;
            PossibleOptions = Math.Min(arrayLength, MaxLines);
            if (ScrollingIsContinuous) Pages = TotalOptions;
            else Pages = 1 + arrayLength / MaxLines;
            ScrollIsEnabled = arrayLength > MaxLines;
            //TODO: prevent cheating (when i find out how)
        }

        public static void ShowPageNum() { if (Pages <= 0 || !PageNumIsVisible || !ScrollIsEnabled || ScrollingIsContinuous) return; (Page + 1).WriteAt(50, Top); ("/" + (Pages + 1)).WriteAt(52, Top); }

        public static int Mod(int a, int b) { return (a %= b) < 0 ? a + b : a; }
        
        public static void Mod(ref int a, int b) { a = (a %= b) < 0 ? a + b : a; }

        public static void WriteAt(this char c, int left, int top) { Console.SetCursorPosition(left, top); Console.Write(c); }

        public static void WriteAt(this object s, int left, int top, int limit = -1)
        {
            limit = limit < 0 ? Console.WindowWidth - left - 1 : limit;
            Console.SetCursorPosition(left, top); Console.Write(s.ToString().PadRight(limit));
        }

        //TODO: Createpartition, createtextbox, bools: spaceIsEdge, spaceIsCorner, borderIsVisible, border styles: curved, straight, double, !!!custom!!! - how, ascii fonts

        public static int Select(this List<string> options, bool menuIsVisible = false)
        {
            //TODO: option to jump to start for continuous scrolling?, prevent cheating (when i find out how)
            Initiate(options.Count);
            var maxLength = options.Max(s => s.Length);
            //TODO: adjust padright when GUI created
            if (menuIsVisible) "[M]enu".WriteAt(10, 23);
            while (true)
            {
                '>'.WriteAt(Left, Index + Top);
                //TODO: change appearance of arrow? >/-
                var input = default(ConsoleKey);
                while (true)
                {
                    ' '.WriteAt(Left, Index + Top);
                    switch (input)
                    {
                        case ConsoleKey.UpArrow: Option--; Index--; break;
                        case ConsoleKey.DownArrow: Option++; Index ++;break;
                        case ConsoleKey.M: if (menuIsVisible) PlayerMenu(); break;
                        //TODO: key array, text array, left array, top array - write text[i] at left[i] + top[i], if input is key[i] then run !!!method[i]!!! - or return xor with 11111111_2, then process
                        case ConsoleKey.Enter: return Option;
                        case default(ConsoleKey): break;
                        default: continue;
                    }
                    Mod(ref Option, TotalOptions);
                    if (ScrollIsEnabled)
                    {
                        if (Index < 0) PagePlus = -1;
                        else if (Index > PossibleOptions) PagePlus = 1;
                        if (ScrollingIsContinuous) Index = Math.Min(Math.Max(Index, 0), PossibleOptions);
                    }
                    else Index = Option;
                    if (PagePlus != 0)
                    {
                        Page += PagePlus;
                        Mod(ref Page, Pages);
                        ShowPageNum();
                        for (var i = 0; i < PossibleOptions; i++)
                            (ScrollingIsContinuous ? options[(Page + i) % TotalOptions] : options[i + (MaxLines * Page)])
                                .WriteAt(Left + 2, Top + i, maxLength);
                    }
                    PagePlus = 0;
                    '>'.WriteAt(Left, Index + Top);
                    input = Console.ReadKey(true).Key;
                }
            }
        }

        public static void PlayerMenu()
        {
            Console.Clear(); Console.WriteLine("Inventory");
            //SelectOption(Player.Instance.InventoryName);
            //TODO: Add more options, centre text
        }
        //TODO: move to inventory.cs

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

        public static int ToInt(this string value) { return value.Aggregate(0, (current, t) => 10 * current + (t - 48)); }

        public static string RemoveLast(this string input) { return input.Remove(input.Length - 1); }

        public static string RemoveLast(this string input, int numberToRemove) { return input.Remove(input.Length - numberToRemove); }

        public static List<string> WordWrap(string paragraph, out int numberOfLines, int maxX = -1, bool wordWrap = false)
        {
            //scrolling, maybe
            if (maxX < 0 || maxX > Console.WindowWidth) maxX = Console.WindowWidth;
            paragraph = new Regex(@" {2,}").Replace(paragraph.Trim(), @" ");
            var left = Console.CursorLeft; var top = Console.CursorTop; numberOfLines = 0; var lines = new List<string>();
            for (; paragraph.Length > 0; numberOfLines++)
            {
                lines.Add(paragraph.Substring(0, Math.Min(maxX - left, paragraph.Length)));
                if (wordWrap && paragraph.Length > Console.WindowWidth - left)
                {
                    var length = lines[numberOfLines].LastIndexOf(" ", StringComparison.Ordinal);
                    if (length > 0) lines[numberOfLines] = lines[numberOfLines].Remove(length);
                }
                paragraph = paragraph.Substring(Math.Min(lines[numberOfLines].Length + 1, paragraph.Length));
                lines[numberOfLines].WriteAt(left, top + numberOfLines);
            } return lines;
        }

        public static List<int> NewList(int count, int value)
        {
            //TODO: make it a list<t>
            var list = new List<int>(count);
            for (var i = 0; i < count; i++) list.Add(value);
            return list;
        }

        public static List<int> Spend(string pretext, List<string> text, List<string> names, List<int> items, List<int> cost, int currency, List<int> sellValue = null, string singular = null, string textzero = null, int arrowPosition = -1)
        {
            Console.Clear();
            if (names.Count != items.Count || items.Count != cost.Count || (sellValue != null && cost.Count != sellValue.Count)) throw new Exception("List length mismatch");
            //TODO: un-error-ify - how will the other lists be stored, organise, make more readable
            var newItems = NewList(items.Count, 0);
            Initiate(names.Count);
            var maximumNameLength = names.Max(n => n.Length);
            var sellIsEnabled = sellValue != null;
            if (arrowPosition < 0 || arrowPosition >= Console.WindowWidth) throw new Exception("Arrow position out of bounds");
            int startLine;
            WordWrap(pretext, out startLine);
            var maximumCurrencyLength = currency.ToString().Length + text[0].Length + text[1].Length + text[2].Length;
            if (singular == null) singular = text[1].RemoveLast();
            else maximumCurrencyLength += Math.Max(0, singular.Length - text[1].Length);
            if (textzero != null) maximumCurrencyLength = Math.Max(maximumCurrencyLength, textzero.Length);
            var line = startLine + 1; var input = default(ConsoleKey); var totalItems = items.ToList(); var amountIsChanged = false;
            ShowPageNum();
            while (true)
            {
                switch (input)
                {
                    case ConsoleKey.UpArrow: Option--; Index--; break;
                    case ConsoleKey.DownArrow: Option++; Index++; break;
                    case ConsoleKey.LeftArrow:
                        if (newItems[Option] > 0) { newItems[Option]--; currency += cost[Option]; amountIsChanged = true; }
                        else if (sellIsEnabled && items[Option] + newItems[Option] > 0) { newItems[Option]--; currency += sellValue[Option]; amountIsChanged = true; }
                        break;
                    case ConsoleKey.RightArrow:
                        if (currency - cost[Option] >= 0) { newItems[Option]++; currency -= cost[Option]; amountIsChanged = true; }
                        else if (sellIsEnabled && newItems[Option] < 0) { if (currency - sellValue[Option] < 0) break; newItems[Option]++; currency -= sellValue[Option]; amountIsChanged = true; }
                        break;
                    case ConsoleKey.Enter: return totalItems;
                }
                if (amountIsChanged)
                {
                    totalItems[Option] = items[Option] + newItems[Option];
                    totalItems[Option].WriteAt(maximumNameLength + 2, line, totalItems[Option].ToString().Length + 1);
                    string s;
                    switch (currency)
                    {
                        case 1: s = currency + singular; break;
                        case 0: if (textzero != null) s = textzero; else s = "no" + text[1]; break;
                        default: s = currency + text[1]; break;
                    }
                    if (currency != 0 || textzero == null) (text[0] + s + text[2]).WriteAt(0, startLine + MaxLines + 2, maximumCurrencyLength);
                }
                Mod(ref Option, TotalOptions);
                if (ScrollIsEnabled)
                {
                    if (Index < 0) PagePlus = -1;
                    else if (Index >= PossibleOptions) PagePlus = 1;
                    if (PagePlus != 0)
                    {
                        Page += PagePlus;
                        Mod(ref Page, Pages);
                        ShowPageNum();
                        if (ScrollingIsContinuous) Index = Math.Min(PossibleOptions - 1, Math.Max(0, Index));
                        else
                        {
                            PossibleOptions = Math.Min(TotalOptions - MaxLines * Page, MaxLines);
                            if (PagePlus == 1) Index = 0;
                            if (PagePlus == -1) Index = PossibleOptions - 1;
                        }
                        var maximumItemNumberLength = totalItems.Max().ToString().Length;
                        for (var i = 0; i < PossibleOptions; i++)
                        {
                            (ScrollingIsContinuous ? names[(Page + i) % names.Count] : names[i + (MaxLines * Page)])
                                .WriteAt(2, Top + i + 1, maximumNameLength);
                            (ScrollingIsContinuous ? totalItems[(Page + i) % totalItems.Count] : totalItems[i + (MaxLines * Page)])
                                .WriteAt(maximumNameLength + 3, Top + i + 1, maximumItemNumberLength);
                        }
                        for (var i = PossibleOptions; i < MaxLines; i++) "".WriteAt(2, Top + i + 1, 1 + maximumNameLength + maximumItemNumberLength);
                    }
                }
                if (input == ConsoleKey.UpArrow || input == ConsoleKey.DownArrow || input == default(ConsoleKey))
                { ' '.WriteAt(Left, line); '>'.WriteAt(Left, line = Index + startLine + 1); }
                input = Console.ReadKey(true).Key;
                amountIsChanged = false;
                PagePlus = 0;
            }
        }
    }

    internal class Quicksort
    {
        public static int Partition(List<int> numbers, int left, int right)
        { //TODO: list<t>-ify
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
