using static System.Console;
namespace Improved.Consoles{
    using System.Collections.Generic;
    public enum Style:byte{
        Normal,
        Bold,
        Double,
        Curved
    }
    public class Frame{
        public List<string> Buffer;
        public int Height;
        public int Priority;
        //TODO: frame order, add to frameList
        public char[][] StyleParts={
            new[]{'─','│','┌','┐','└','┘'},
            new[]{'━','┃','┏','┓','┗','┛'},
            new[]{'═','║','╔','╗','╚','╝'},
            new[]{'─','│','╭','╮','╯','╰'}
        };
        public int TopLeftX;
        public int TopLeftY;
        public int Width;
        public Frame(int topLeftCornerY,int topLeftCornerX,int height,int width){
            TopLeftX=topLeftCornerX;
            TopLeftY=topLeftCornerY;
            Height=height;
            Width=width;
        }
        //TODO: equipment to enum, Overlap(style1, side1, style2, side2), Console.Beep for alerts, sound option
        public void ShowBorder(Style s){
            var num=(int)s;
            var str1=new string(StyleParts[num][0],Width);
            var str2=new string(StyleParts[num][1],Height);
            str1.WriteAt(TopLeftX,TopLeftY,Width);
            str1.WriteAt(TopLeftX,TopLeftY+Height-1,Width);
            str2.WriteVertical(TopLeftX,TopLeftY);
            str2.WriteVertical(TopLeftX+Width-1,TopLeftY);
            //TODO: make border outside frame, not at edge, pinvoke so cursor doesnt move
            for(var i=0;i<4;i++) StyleParts[num][2+i].WriteAt(TopLeftX-1+i%2*Width,TopLeftY-1+i/2*Height);
        }
        //TODO: mixed styles - bottom, top, left, right, 
        /// <summary>
        ///     Writes a string to a frame
        /// </summary>
        /// <param name="s">The string to write</param>
        /// <param name="wordWrap">Whether to wrap words instead of letters</param>
        public void Write(string s,bool wordWrap=false){
            var lines=Consoles.WordWrap(s,Width,wordWrap);
            Buffer.AddRange(lines);
            Buffer.RemoveRange(0,lines.Count);
        }
        public void Write(string s,object[] o,int left=0,int top=0) {s.WriteAt(TopLeftX,TopLeftY,Width,o:o);}
        public void Write(string s,object[] o,bool wordWrap,int left=-1,int top=-1,int width=int.MaxValue){
            s.WriteAt(wordWrap,TopLeftX,TopLeftY,Width,o:o);
        }//TODO: modify buffer instead
        public void WriteBorder(string s,int top){
            var line=new char[Width];
            for(var i=0;i<line.Length;i+=1) line[i]=s[i%s.Length];
            WriteLine(new string(line));
        }
        public void Write(object o,int left,int top,int limit=-1) {o.WriteAt(left,top,limit.EnsureBetween(0,Width));}
    }
    public static class Frames{
        public static List<Frame> FrameList;
        public static void ShowFrameNumbers(){
            for(var i=0;i<FrameList.Count;i++) i.WriteAt(FrameList[i].TopLeftX,FrameList[i].TopLeftY);
        }
    }
}
