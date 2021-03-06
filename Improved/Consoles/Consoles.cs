﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Improved.Consoles
{
    public class KeyMenu
    {
        public string Text;
        public int Left;
        public int Top;
        public ConsoleKey Key;

        public KeyMenu(string text, int left, int top, ConsoleKey key)
        {
            Text = text;
            Left = left;
            Top = top;
            Key = key;
        }
    }
    
    public interface IScrollable
    {
        void RecalculateIndex();
        List<int> CalculateIndices();
    }

    public interface IAlignable
    {
        void Write(string s, object[] o);
    }
    
    public static class Consoles
    {
        public static bool ScrollingIsContinuous, ScrollIsEnabled, PageNumIsVisible, Continue;
        public static int Left, Top, Option, Index, Page, MaxLines, PagePlus, TotalOptions, PossibleOptions, Pages;
        public static ConsoleKey Input;
        public static IScrollable Scroll;

        public enum Alignment { Left, Centre, Right, Justified }

        public static Alignment CurrentAlignment;

        public class Continuous : IScrollable
        {
            public void RecalculateIndex() { REnsureBetween(ref Index, 0, PossibleOptions - 1); }
            public List<int> CalculateIndices() { var indices = new List<int>(); for (var i = 0; i < PossibleOptions; i++) indices.Add((Page + i) % TotalOptions); return indices; }
        }

        public class PageByPage : IScrollable
        {
            public void RecalculateIndex()
            {
                PossibleOptions = Math.Min(TotalOptions - MaxLines * Page, MaxLines);
                switch (PagePlus) { case 1: Index = 0; break; case -1: Index = PossibleOptions - 1; break; }
            }
            public List<int> CalculateIndices() { var indices = new List<int>(); for (var i = 0; i < PossibleOptions; i++) indices.Add(i + (MaxLines * Page)); return indices; }
        }

        public class LeftAligned : IAlignable
        {
            public void Write(string s, object[] o)
            {
                String.Format(s, o).WriteAt(0, Console.CursorTop);
                //TODO: left (for frames)
            }
        }

        //TODO: rearrange methods, readability, replace footerheight with frame, frameify spend and select
        public static void Initiate(int arrayLength) { Initiate(arrayLength, Console.WindowHeight - 3); }

        public static void Initiate(int arrayLength, int lastLine)
        {
            if (ScrollingIsContinuous) Scroll = new Continuous(); else Scroll = new PageByPage();
            Option = 1; Page = -1; PagePlus = 1; Index = 1; Input = ConsoleKey.UpArrow;
            Left = Console.CursorLeft; Top = Console.CursorTop + 1;
            MaxLines = lastLine - Top;
            TotalOptions = arrayLength;
            PossibleOptions = Math.Min(MaxLines, TotalOptions);
            Pages = ScrollingIsContinuous ? TotalOptions : 1 + TotalOptions/MaxLines;
            ScrollIsEnabled = TotalOptions > MaxLines;
        }

        //TODO: text position - left, centre, right, justified

        public static void ShowPageNum(int left = 50, int top = -1) { if (top < 0 || top > Console.WindowHeight) top = Top;
            if (Pages <= 0 || !PageNumIsVisible || !ScrollIsEnabled || ScrollingIsContinuous) return;
            ((Page + 1) + "/" + (Pages + 1)).WriteAt(left + 2, top);
            //TODO: page + 1 > 10
        }
        /// <summary>
        /// Removes the last character in a string.
        /// </summary>
        /// <param name="input">The string to remove the last character from</param>
        /// <returns>The modified string</returns>
        public static string RemoveLast(this string input) { return input.Remove(input.Length - 1); }
        /// <summary>
        /// Removes the last characters in a string.
        /// </summary>
        /// <param name="input">The string to remove characters from</param>
        /// <param name="numberToRemove">The number of characters to remove</param>
        /// <returns>The modified string</returns>
        public static string RemoveLast(this string input, int numberToRemove) { return input.Remove(input.Length - numberToRemove); }
        //TODO: rename params
        /// <summary>
        /// Writes a string vertically, one character on each line.
        /// </summary>
        /// <param name="s">The string to write</param>
        public static void WriteVertical(this string s) { s.WriteVertical(Console.CursorLeft, Console.CursorTop); }
        /// <summary>
        /// Writes a string vertically, one character on each line, at a specified position.
        /// </summary>
        /// <param name="s">The string to write</param>
        /// <param name="left">The column to write the string at</param>
        /// <param name="top">The row to write the string at</param>
        public static void WriteVertical(this string s, int left, int top) { for (var i = 1; i <= s.Length; i++) WriteAt(s[i - 1], left, top + i, 0); }
        /// <summary>
        /// Writes a character at a specified position, padded to the right of the window.
        /// </summary>
        /// <param name="c">The character to write</param>
        /// <param name="left">The column to write the string at</param>
        /// <param name="top">The row to write the string at</param>
        public static void WriteAt(this char c, int left, int top) { Console.SetCursorPosition(left, top); Console.Write(c); }
        /// <summary>
        /// Writes a character at a specified position, padded to a maximum length.
        /// </summary>
        /// <param name="o">The object to write</param>
        /// <param name="left">The column to write the string at</param>
        /// <param name="top">The row to write the string at</param>
        /// <param name="limit">The amount of the padding</param>
        public static void WriteAt(this object o, int left, int top, int limit = -1)
        {
            limit = (limit < 0 || limit >= Console.WindowWidth) ? Console.WindowWidth - left - 1 : limit;
            Console.SetCursorPosition(left, top); Console.Write(o.ToString().PadRight(limit));
        }

        public static void REnsureBetween(ref int n, int min, int max) { n =  n.EnsureBetween(min, max); }

        public static int EnsureBetween(this int n, int min, int max) { return Math.Max(min, Math.Min(max, n)); }

        public static void WriteBorder(this string s, int width = int.MaxValue)
        {
            REnsureBetween(ref width, 1, Console.WindowWidth - 1);
            for (var i = 0; i <= width; i += 1) Console.Write(s[i % s.Length]);
        }

        //TODO BLOCK: bools: spaceIsEdge, spaceIsCorner, borderIsVisible, ascii fonts
        public static int Choose(this string[] options, KeyMenu[] keyMenus = null)
        {
            //option to jump to start for continuous scrolling? key list etc
            if (!Continue) Initiate(options.Length);
            else Continue = false;
            //Continue =!;, =; <- is this possible in c#
            var maxLength = options.Max(s => s.Length);
            //adjust padright when GUI created
            //if (menuIsVisible) "[M]enu".WriteAt(10, 23);
            while (true)
            {
                '>'.WriteAt(Left, Index + Top);
                //change appearance of arrow? >/-
                while (true)
                {
                    var oldIndex = Index;
                    switch (Input)
                    {
                        case ConsoleKey.UpArrow: Option--; Index--; break;
                        case ConsoleKey.DownArrow: Option++; Index++; break;
                        //case ConsoleKey.M: if (menuIsVisible) PlayerMenu(); break;
                        //then process - but how? use in Spend() as well - savebuffer() loadbuffer() (pinvoke, or Frame.Buffer?)? async + await? set positions - PositionSystem - spacing, vertical spacing
                        case ConsoleKey.Enter: return Option;
                        case default(ConsoleKey): break;
                        default:
                            int index;
                            if (keyMenus == null) { Input = Console.ReadKey(true).Key; continue; }
                            if ((index = keyMenus.ToList().FindIndex(k => k.Key == Input)) != -1) { Continue = true; return 0-index; }//-index?
                            //TODO: is default value -1?
                            break;
                    }
                    Maths.RMod(ref Option, TotalOptions);
                    if (ScrollIsEnabled) { if (Index < 0) PagePlus = -1; else if (Index >= PossibleOptions) PagePlus = 1; }
                    else Index = Option;
                    if (PagePlus != 0)
                    {
                        Page += PagePlus;
                        Maths.RMod(ref Page, Pages);
                        ShowPageNum();
                        Scroll.RecalculateIndex();
                        var indices = Scroll.CalculateIndices();
                        Console.SetCursorPosition(40, 15);
                        var i = 0;
                        for (; i < PossibleOptions; i++) options[indices[i]].WriteAt(Left + 2, Top + i, maxLength);
                        for (; i < MaxLines; i++) "".WriteAt(Left + 2, Top + i, maxLength);
                        PagePlus = 0;
                    }
                    ' '.WriteAt(Left, oldIndex + Top); '>'.WriteAt(Left, Index + Top);
                    Input = Console.ReadKey(true).Key;
                }
            }
        }

        public static List<string> WordWrap(string paragraph, int width = Int32.MaxValue, bool wordWrap = true)
        { int numberOfLines; return WordWrap(paragraph, out numberOfLines, width, wordWrap); }

        /// <summary>
        /// Moves words that would otherwise be cut off at the end of a line to the next line
        /// </summary>
        /// <param name="paragraph">The string to wrap</param>
        /// <param name="numberOfLines">The number of lines the string takes up</param>
        /// <param name="width">The width of the frame</param>
        /// <param name="wordWrap">If false, wraps by letters instead</param>
        /// <returns>The paragraph split into a list of lines</returns>
        public static List<string> WordWrap(string paragraph, out int numberOfLines, int width = Int32.MaxValue, bool wordWrap = true)
        {
            var left = Console.CursorLeft; var top = Console.CursorTop; numberOfLines = 0; var lines = new List<string>();
            width = Math.Min(width, Console.WindowWidth - left);
            paragraph = new Regex(" {2,}").Replace(paragraph.Trim(), " ");
            for (; paragraph.Length > 0; numberOfLines++)
            {
                lines.Add(paragraph.Substring(0, Math.Min(width - left, paragraph.Length)));
                int length;
                if (wordWrap && paragraph.Length > Console.WindowWidth - left && (length = lines[numberOfLines].LastIndexOf(' ')) > 0)
                    lines[numberOfLines] = lines[numberOfLines].Remove(length);
                paragraph = paragraph.Substring(Math.Min(lines[numberOfLines].Length + 1, paragraph.Length));
                lines[numberOfLines].WriteAt(left, top + numberOfLines);
            } return lines;
        }

        /// <summary>
        /// Creates a new list filled with a value.
        /// </summary>
        /// <typeparam name="T">The type of list and value</typeparam>
        /// <param name="count">The number of items in the list</param>
        /// <param name="value">The value to fill the list with</param>
        /// <returns>The new list</returns>
        public static List<T> NewList<T>(int count, T value) { var list = new List<T>(); for (var i = 0; i < count; i++) list.Add(value); return list; }

        //TODO: move spend() to rpg section (out of consolus), same project as item

        /// <summary>
        /// Lets the user spend currency to obtain items.
        /// </summary>
        /// <param name="text">The text, after the spend screen, that tells the user the amount of currency left, in the format [0] = "You have ", [1] = " currency", [2] = "left"</param>
        /// <param name="options">The names of the items</param>
        /// <param name="items">The number of items</param>
        /// <param name="cost">The cost of each item</param>
        /// <param name="currency">The currency the user has</param>
        /// <param name="currencyLeft">The remaining currency of the user</param>
        /// <param name="sellValue">The value of the items when sold</param>
        /// <param name="singular">The text to display instead of " currency" when one unit is left</param>
        /// <param name="textzero">The full text to display when the user has no currency left</param>
        /// <param name="arrowPosition">The position from left of the pointer indicating the item being modified</param>
        /// <returns>The total number of each item</returns>
        public static List<int> Spend(List<string> text, List<string> options, List<int> items, List<int> cost, int currency, out int currencyLeft, List<int> sellValue = null, string singular = null, string textzero = null, int arrowPosition = -1)
        {
            Console.Clear();
            if (options.Count != items.Count || items.Count != cost.Count || (sellValue != null && cost.Count != sellValue.Count)) throw new Exception("List length mismatch"); //replace with list<item>
            //TODO: un-error-ify - how will the other lists be stored, organise, make more readable, continue from spend(), extract common methods, add ISelectable
            var newItems = NewList(TotalOptions, 0);
            Initiate(options.Count);
            var maximumNameLength = options.Max(n => n.Length);
            var sellIsEnabled = sellValue != null;
            if (arrowPosition < 0 || arrowPosition >= Console.WindowWidth) throw new Exception("Arrow position out of bounds");
            var maximumCurrencyLength = currency.ToString().Length + text[0].Length + text[1].Length + text[2].Length;
            if (singular != null) maximumCurrencyLength += Math.Max(0, singular.Length - text[1].Length);
            singular = singular ?? text[1].RemoveLast();
            if (textzero != null) maximumCurrencyLength = Math.Max(maximumCurrencyLength, textzero.Length);
            var line = Top + 1; var totalItems = items.ToList(); var amountIsChanged = false;
            ShowPageNum();
            while (true)
            {
                switch (Input)
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
                    case ConsoleKey.Enter: currencyLeft = currency; return totalItems;
                    default: Input = Console.ReadKey(true).Key; continue;
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
                    if (currency != 0 || textzero == null) (text[0] + s + text[2]).WriteAt(0, Top + MaxLines + 2, maximumCurrencyLength);
                }
                Maths.RMod(ref Option, TotalOptions);
                if (ScrollIsEnabled) //TODO: draw thing on first run
                {
                    if (Index < 0) PagePlus = -1;
                    else if (Index >= PossibleOptions) PagePlus = 1;
                    if (PagePlus != 0)
                    {
                        Page += PagePlus;
                        Maths.RMod(ref Page, Pages);
                        ShowPageNum();
                        Scroll.RecalculateIndex();
                        var maximumItemNumberLength = totalItems.Max().ToString().Length;
                        var indices = Scroll.CalculateIndices();
                        var i = 0;
                        var top = Top;
                        for (; i < PossibleOptions; i++)
                        {
                            options[indices[i]].WriteAt(2, ++top, maximumNameLength);
                            totalItems[indices[i]].WriteAt(maximumNameLength + 3, top, maximumItemNumberLength);
                        }
                        for (; i < MaxLines; i++) "".WriteAt(2, ++top, 1 + maximumNameLength + maximumItemNumberLength);
                    }
                }
                if (Input == ConsoleKey.UpArrow || Input == ConsoleKey.DownArrow)
                { ' '.WriteAt(Left, line); '>'.WriteAt(Left, line = Index + Top + 1); }
                Input = Console.ReadKey(true).Key;
                amountIsChanged = false;
                PagePlus = 0;
            }
        }
    }
}
