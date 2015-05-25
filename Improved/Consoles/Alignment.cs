namespace Improved.Consoles{
    using System.Linq;
    using System.Text.RegularExpressions;
    using static System.Console;
    public enum Alignment{
        Left,
        Centre,
        Right
    }
    public interface IAlignable{
        int Calculate(int len,int left,int width);
    }
    public class Lefted:IAlignable{
        public int Calculate(int len,int left,int width) =>left;
    }
    public class Centred:IAlignable{
        public int Calculate(int len,int left,int width) =>(width-len)/2;
    }
    public class Righted:IAlignable{
        public int Calculate(int len,int left,int width) =>width-len;
    }
    /*public void WriteJustified(string s, object[] o, int left = -1, int top = -1)
        {
            string.Format(s, o).WriteAt(0, CursorTop);
            //TODO: left (for frames), var spaces=l.Count(c=>c==' '); left+=word.Length; l.Split(new{' '}).WriteAt(spaces*wordswritten/totalwords
        }*/
    public static class Aligned{
        public static Alignment Alignment;
        public static IAlignable Align = new Lefted();
        public static Regex Regex=new Regex("[\n\r]");
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
            int left=0,
            int top=-1,
            int width=int.MaxValue,
            int maxLength=-1,
            object[] o=null){
            left=left<0||left>=WindowWidth?0:left;
            Consoles.REnsureBetween(ref width,1,WindowWidth-left);
            CursorTop=top<0||top>=WindowHeight?0:top;//TODO: \r\n
            Consoles.REnsureBetween(ref maxLength,0,width);
            var lines=s.Split(new[] {'\n','\r'}, System.StringSplitOptions.RemoveEmptyEntries).Select(l=>l.PadRight(maxLength)).ToArray();
            if(lines.Length==0) s.PadRight(maxLength).W(Align.Calculate(0,left,width),CursorTop);
            if(lines.Length==1) (o==null?lines[0]:string.Format(lines[0],o)).W(Align.Calculate(lines[0].Length,left,width),CursorTop);
            else foreach(var l in lines) (o==null?l:string.Format(l,o)).W(Align.Calculate(l.Length,left,width),++CursorTop);
        }
        public static void W(this string s,int left,int top){
            SetCursorPosition(left,top);
            Write(s);
        }
        public static void WriteAt(this string s,
            bool wordWrap,
            int left=0,
            int top=-1,
            int width=int.MaxValue,
            int maxLength=-1,
            object[] o=null){
            if(wordWrap) foreach(var l in Consoles.WordWrap(Regex.Replace(s,""),write:false)) WriteAt(l,left,CursorTop,width,maxLength,o);
            else WriteAt(s,left,CursorTop,width,maxLength,o);
        }
        public static void WriteAt(this string s,
            bool wordWrap,
            object[] o,
            int left=0,
            int top=-1,
            int width=int.MaxValue,
            int maxLength=-1,
            bool clear=false){
            if(clear) Clear();
            if(wordWrap) foreach(var l in Consoles.WordWrap(Regex.Replace(s,""),write:false)) WriteAt(l,left,CursorTop,width,maxLength,o);
            else WriteAt(s,left,CursorTop,width,maxLength,o);
        }
        public static void WriteAt(this string s,
            Alignment alignment,
            bool wordWrap=false,
            object[] o=null,
            int left=0,
            int top=-1,
            int width=int.MaxValue,
            int maxLength=-1,
            bool clear=false){
            Initiate(alignment);
            WriteAt(s,wordWrap,o,left,top,width,maxLength,clear);
        }
        public static void WriteAt(this object o,
            int left=0,
            int top=-1,
            int width=int.MaxValue,
            bool wordWrap=false,
            int maxLength=-1,
            bool clear=false){
            if(clear) Clear();
            WriteAt(o.ToString(),wordWrap,left,top,width,maxLength);
        }
        public static void WriteAt(this char c,int left=0,int top=-1,bool clear=false){
            if(clear) Clear();
            W(c.ToString(),left,top);
        }
    }
}
