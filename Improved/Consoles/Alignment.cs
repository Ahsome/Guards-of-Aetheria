using static System.Console;
namespace Improved.Consoles{
    using System;
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
            //TODO: left (for frames), space = width - 
        }*/
    public class Aligned{
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
        public static void Write(string s,object[] o=null,int left=-1,int top=-1,int width=int.MaxValue){
            width=Math.Min(width,WindowWidth);
            if(left<0||left>WindowWidth) left=0;
            if(top>=0||top<=WindowHeight) CursorTop=top;
            var lines=s.Split(new[]{'\n','\r'},StringSplitOptions.RemoveEmptyEntries);
            foreach(var l in lines) (o==null?l:string.Format(l,o)).WriteAt(Align.Calculate(l.Length,left,width),++CursorTop);
        }
        public static void Write(string s,bool wordWrap,object[] o=null,int left=-1,int top=-1,int width=int.MaxValue){
            if(!wordWrap){
                if(top>=0&&top<=WindowHeight) CursorTop=top;
                Write(s,o,left,CursorTop,width);
                return;
            }
            foreach(var l in Consoles.WordWrap(s.Replace('\n',' '))) Write(l,o,left,CursorTop,width);
        }
        public static void Write(string s,Alignment alignment,bool wordWrap=false,object[] o=null,int left=-1,int top=-1,
            int width=int.MaxValue){
            Initiate(alignment);
            Write(s,wordWrap,null,left,top,width);
        }
    }
}
