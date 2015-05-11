using System.Collections.Generic;
using static System.Console;

namespace Improved.Consoles {
    public enum Style:byte { Normal, Bold, Double, Curved }

    public class Frame {
        public int TopLeftX;
        public int TopLeftY;
        public int Height;
        public int Width;
        public int Priority;
        public List<string> Buffer;

        public Frame(int topLeftCornerY,int topLeftCornerX,int height,int width) {
            TopLeftX=topLeftCornerX;
            TopLeftY=topLeftCornerY;
            Height=height;
            Width=width;
        }
        //TODO: frame order, add to frameList

        public char[][] StyleParts =
        {
            new[] {'─', '│', '┌', '┐', '└', '┘'},
            new[] {'━', '┃', '┏', '┓', '┗', '┛'},
            new[] {'═', '║', '╔', '╗', '╚', '╝'},
            new[] {'─', '│', '╭', '╮', '╯', '╰'}
        };

        //TODO: equipment to enum, Overlap(style1, side1, style2, side2), Console.Beep for alerts, sound option

        public void ShowBorder(Style s) {
            var num = (int)s;
            var str1 = new string(StyleParts[num][0],Width);
            var str2 = new string(StyleParts[num][1],Height);
            str1.WriteAt(TopLeftX,TopLeftY,Width);
            str1.WriteAt(TopLeftX,TopLeftY+Height-1,Width);
            str2.WriteVertical(TopLeftX,TopLeftY);
            str2.WriteVertical(TopLeftX+Width-1,TopLeftY);
            //TODO: make border outside frame, not at edge, pinvoke so cursor doesnt move
            for(var i = 0;i<4;i++) StyleParts[num][2+i].WriteAt(TopLeftX-1+i%2*Width,TopLeftY-1+i/2*Height);
        }
        //TODO: mixed styles - bottom, top, left, right, 

        /// <summary>
        /// Writes a string to a frame
        /// </summary>
        /// <param name="s">The string to write</param>
        /// <param name="wordWrap">Whether to wrap words instead of letters</param>
        public void Write(string s,bool wordWrap = false) {
            int numLines;
            Buffer.AddRange(Consoles.WordWrap(s,out numLines,Width,wordWrap));
            Buffer.RemoveRange(0,numLines);
        }

        public void Write(string s,object[] o,int left = 0,int top = 0) {
            Aligned.Write(s,o,TopLeftX,TopLeftY,Width);
        }

        public void Write(string s,object[] o,bool wordWrap,int left = -1,int top = -1,int width = int.MaxValue) {
            Aligned.Write(s,wordWrap,o,TopLeftX,TopLeftY,Width);
        }
        //TODO: modify buffer

        public void WriteBorder(string s,int top) {
            var line = new char[Width];
            for(var i = 0;i<line.Length;i+=1) line[i]=s[i%s.Length];
            WriteLine(new string(line));
        }

        public void WriteAt(object o,int left,int top,int limit = -1) {
            o.WriteAt(left,top,limit.EnsureBetween(0,Width));
        }
        //TODO: copy from, dev mode, devtools.cs? for errors and stuff
    }

    public static class Frames {
        public static List<Frame> FrameList;

        public static void ShowFrameNumbers() {
            var i = 0; foreach(var f in FrameList) { i.WriteAt(f.TopLeftX,f.TopLeftY); i++; }
        }
    }

}