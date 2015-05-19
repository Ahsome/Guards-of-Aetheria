using static System.Console;
namespace Improved.Consoles{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    public enum Alignment{
        Left,
        Centre,
        Right
    }
    public interface IAlignable{
        int Calculate(int len,int left,int width);
    }
    public class Lefted:IAlignable{
        public int Calculate(int len,int left,int width) {return left;}
    }
    public class Centred:IAlignable{
        public int Calculate(int len,int left,int width) {return (width-len)/2;}
    }
    public class Righted:IAlignable{
        public int Calculate(int len,int left,int width) {return width-len;}
    }
    /*public void WriteJustified(string s, object[] o, int left = -1, int top = -1)
        {
            string.Format(s, o).WriteAt(0, CursorTop);
            //TODO: left (for frames), var spaces=l.Count(c=>c==' '); left+=word.Length; l.Split(new{' '}).WriteAt(spaces*wordswritten/totalwords
        }*/
    public static class Aligned{
        public static Alignment Alignment;
        public static IAlignable Align;
        public static void Initiate() {Initiate(Alignment);}
        public static void Initiate(Alignment alignment){
            switch(alignment){
                case Alignment.Left:
                    Align=new Lefted();
                    break;
                case Alignment.Centre:
                    Align=new Centred();
                    break;
                case Alignment.Right:
                    Align=new Righted();
                    break;
            }
        }
        public static void WriteAt(this string s,
            int left=-1,
            int top=-1,
            int width=int.MaxValue,
            int maxLength=-1,
            object[] o=null){
            CursorLeft=left<0||left>=WindowWidth?WindowWidth:left;
            Consoles.REnsureBetween(ref width,1,WindowWidth-left);
            CursorTop=top<0||top>=WindowHeight?WindowHeight:top;//TODO: \r\n
            Consoles.REnsureBetween(ref maxLength,1,width);
            foreach(var l in
                s.Split(new[]{'\n','\r'},StringSplitOptions.RemoveEmptyEntries).Select(l=>l.PadRight(maxLength))) (o==null?l:string.Format(l,o)).W(Align.Calculate(l.Length,left,width),++CursorTop);
        }
        public static void W(this string s,int left,int top){
            SetCursorPosition(left,top);
            Write(s);
        }
        public static void WriteAt(this string s,
            bool wordWrap,
            int left=-1,
            int top=-1,
            int width=int.MaxValue,
            int maxLength=-1,
            object[] o=null){
            var regex=new Regex("[\n\r]");
            if(wordWrap) foreach(var l in Consoles.WordWrap(regex.Replace(s,""),write:false)) WriteAt(l,left,CursorTop,width,maxLength,o);
            else WriteAt(s,left,CursorTop,width,maxLength,o);
        }
        public static void CWrite(this string s,
            bool wordWrap,
            int left=-1,
            int top=-1,
            int width=int.MaxValue,
            int maxLength=-1,
            object[] o=null){
            var regex=new Regex("[\n\r]");
            Clear();
            if(wordWrap) foreach(var l in Consoles.WordWrap(regex.Replace(s,""),write:false)) WriteAt(l,left,CursorTop,width,maxLength,o);
            else WriteAt(s,left,CursorTop,width,maxLength,o);
        }
        public static void WriteAt(this string s,
            Alignment alignment,
            bool wordWrap=false,
            object[] o=null,
            int left=-1,
            int top=-1,
            int width=int.MaxValue,
            int maxLength=-1){
            Initiate(alignment);
            WriteAt(s,wordWrap,left,top,width,maxLength,o);
        }
        public static void CWrite(this string s,
            Alignment alignment,
            bool wordWrap=false,
            object[] o=null,
            int left=-1,
            int top=-1,
            int width=int.MaxValue,
            int maxLength=-1){
            Initiate(alignment);
            Clear();
            WriteAt(s,wordWrap,left,top,width,maxLength,o);
        }
        public static void WriteAt(this object o,
            int left=-1,
            int top=-1,
            int width=int.MaxValue,
            bool wordWrap=false,
            int maxLength=-1){
            WriteAt(o.ToString(),wordWrap,left,top,width,maxLength);
        }
        public static void CWrite(this object o,
            int left=-1,
            int top=-1,
            int width=int.MaxValue,
            bool wordWrap=false,
            int maxLength=-1){
            Clear();
            WriteAt(o.ToString(),wordWrap,left,top,width,maxLength);
        }
        public static void WriteAt(this char c,int left=-1,int top=-1) {W(c.ToString(),left,top);}
        public static void CWrite(this char c,int left=-1,int top=-1){
            Clear();
            W(c.ToString(),left,top);
        }
    }
}
