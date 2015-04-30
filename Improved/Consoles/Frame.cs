using System;
using System.Collections.Generic;

namespace Improved.Consoles
{
    public class Frame
    {
        public int TopLeftX;
        public int TopLeftY;
        public int Height;
        public int Width;
        public int Priority;
        public List<string> Buffer;

        public Frame(int topLeftCornerY, int topLeftCornerX, int height, int width)
        {
            TopLeftX = topLeftCornerX;
            TopLeftY = topLeftCornerY;
            Height = height;
            Width = width;
        }
        //TODO: frame order, add to frameList

        public enum Style : byte { Normal, Bold, Double, Curved }

        public List<List<char>> StyleParts = new List<List<char>>
        {
            new List<char> {'─', '│', '┌', '┐', '└', '┘'},
            new List<char> {'━', '┃', '┏', '┓', '┗', '┛'},
            new List<char> {'═', '║', '╔', '╗', '╚', '╝'},
            new List<char> {'─', '│', '╭', '╮', '╯', '╰'}
        };

        //TODO: equipment to enum, Overlap(style1, side1, style2, side2), Console.Beep for alerts, sound option

        public void ShowBorder(Style s)
        {
            var num = (int)s;
            var str1 = new string(StyleParts[num][0], Width - 2);
            var str2 = new string(StyleParts[num][1], Height - 2);
            str1.WriteAt(TopLeftX + 1, TopLeftY, Width - 2);
            str1.WriteAt(TopLeftX + 1, TopLeftY + Height - 1, Width - 2);
            str2.WriteVertical(TopLeftX, TopLeftY);
            str2.WriteVertical(TopLeftX + Width - 1, TopLeftY);
            //TODO: fix, pinvoke so cursor doesnt move
            for (var i = 0; i < 4; i++) StyleParts[num][2 + i].WriteAt(TopLeftX - 1 + i % 2 * Width, TopLeftY - 1 + i / 2 * Height);
        }
        //TODO: mixed styles - bottom, top, left, right, 

        /// <summary>
        /// Writes a string to a frame
        /// </summary>
        /// <param name="input">The string to write</param>
        /// <param name="wordWrap">Whether to wrap words instead of letters</param>
        public void Write(string input, bool wordWrap = false)
        {
            int numLines;
            Buffer.AddRange(Consoles.WordWrap(input, out numLines, Width, wordWrap));
            Buffer.RemoveRange(0, numLines);
        }

        public void WriteBorder(string s, int top)
        {
            var line = new char[Width];
            for (var i = 0; i < line.Length; i += 1) line[i] = s[i % s.Length];
            Console.WriteLine(new string(line));
        }

        public void WriteAt(object o, int left, int top, int limit = -1) { o.WriteAt(left, top, limit.EnsureBetween(0, Width)); }
        //TODO: copy from, dev mode, devtools.cs? for errors and stuff
    }

    public static class Frames
    {
        public static List<Frame> FrameList;

        public static void ShowFrameNumbers() { var i = 0; foreach (var f in FrameList) { i.WriteAt(f.TopLeftX, f.TopLeftY); i++; } }
    }

}