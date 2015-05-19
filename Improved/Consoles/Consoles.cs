using static System.Console;
using static System.ConsoleKey;
namespace Improved.Consoles{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    public class CustomIo{//TODO: fix > 255 char stuff
        public static string ReadLine(Dictionary<int,string> maxPerWord,
            bool lettersNeeded=true,
            int minLength=1,
            int maxLength=255){//minperword? min/max length?
            var x=CursorLeft;
            var y=CursorTop;
            var s="";
            while(true){
                var k=ReadKey(true).KeyChar;
                if(!char.IsLetter(k)&&k!='\b'&&Str.Separators.All(c=>c!=k)&&maxPerWord.Values.All(v=>v.All(c=>c!=k))) continue;
                var lastSpace=Math.Max(0,Str.Separators.Max(sep=>s.LastIndexOf(sep)));
                var lastWord=s.Substring(lastSpace);
                if(maxPerWord.Any(max=>lastWord.Count(c=>max.Value.Any(m=>m==c))>=max.Key&&max.Value.Any(c=>c==k))) continue;
                if(k=='\b'&&s.Length>0){
                    (s=s.RemoveLast()).WriteAt(x,y,s.Length+1);
                    if(CursorLeft==0){
                        CursorTop--;
                        CursorLeft=WindowWidth;
                    } else CursorLeft--;
                    continue;
                }
                if(lettersNeeded&&Str.Separators.Any(c=>c==k)&&!lastWord.Any(char.IsLetter)) continue;
                Write(k);
                switch(k){
                    case '\r':
                    case '\n':
                        if(s.Length==s.Length.EnsureBetween(minLength,maxLength)) return s;
                        break;
                    default:
                        s+=k;
                        break;
                }
            }
        }
    }
    public class KeyMenu{
        public ConsoleKey Key;
        public int Left;
        public string Text;
        public int Top;
        public KeyMenu(string text,int left,int top,ConsoleKey key){
            Text=text;
            Left=left;
            Top=top;
            Key=key;
        }
    }
    public class Item{
        public int Amount;
        public int Cost;
        public int Index;
        public string Name;
        public int Value;
        public Item() {}
        public Item(string name,int amount,int cost,int value=-1,int index=-1){
            Name=name;
            Amount=amount;
            Cost=cost;
            Value=value;
            Index=index;
        }
    }
    public interface IScrollable{
        void RecalculateIndex();
        List<int> CalculateIndices();
    }
    public static class Consoles{
        public static bool ScrollingIsContinuous,ScrollIsEnabled,PageNumIsVisible,Continue;
        public static int Left,Top,Option,Index,Page,MaxLines,PagePlus,TotalOptions,PossibleOptions,Pages;
        public static ConsoleKey Input;
        public static IScrollable Scroll;
        public static List<int> Indices;
        //TODO: rearrange methods, readability, replace footerheight with frame, frameify spend and select
        public static void Initiate(int arrayLength) {Initiate(arrayLength,WindowHeight-3);}
        public static void Initiate(int arrayLength,int lastLine){
            if(ScrollingIsContinuous) Scroll=new Continuous();
            else Scroll=new PageByPage();
            Option=1;
            Page=-1;
            PagePlus=1;
            Index=1;
            Input=UpArrow;
            Left=CursorLeft;
            Top=CursorTop+1;
            MaxLines=lastLine-Top;
            TotalOptions=arrayLength;
            PossibleOptions=Math.Min(MaxLines,TotalOptions);
            Pages=ScrollingIsContinuous?TotalOptions:1+TotalOptions/MaxLines;
            ScrollIsEnabled=TotalOptions>MaxLines;
        }
        /// <summary>
        ///     Removes the last characters in a string.
        /// </summary>
        /// <param name="input">The string to remove characters from</param>
        /// <param name="numberToRemove">The number of characters to remove</param>
        /// <returns>The modified string</returns>
        public static string RemoveLast(this string input,int numberToRemove=1) =>input.Remove(input.Length-numberToRemove);
        //TODO: rename params
        /// <summary>
        ///     Writes a string vertically, one character on each line, at a specified position.
        /// </summary>
        /// <param name="s">The string to write</param>
        /// <param name="left">The column to write the string at</param>
        /// <param name="top">The row to write the string at</param>
        public static void WriteVertical(this string s,int left=-1,int top=-1){
            left=left==-1?CursorLeft:left;
            top=top==-1?CursorTop:top;
            foreach(var c in s) c.WriteAt(left,top++);
        }//TODO: ++top?
        public static void REnsureBetween(ref int n,int min,int max) {n=n.EnsureBetween(min,max);}
        public static int EnsureBetween(this int n,int min,int max) =>Math.Max(min,Math.Min(max,n));
        public static void WriteBorder(this string s,
            int width=int.MaxValue,
            Alignment a=Alignment.Centre,
            int left=0,
            int top=-1){
            REnsureBetween(ref width,1,WindowWidth-1);
            string.Join("",Enumerable.Repeat(s,1+width/s.Length))
                .Substring(0,WindowWidth-width)
                .WriteAt(a,left:left,top:top,width:width);
        }
        public static List<T> Range<T>(this int count,Func<int,T> f,int min=0)
            =>Enumerable.Range(min,count).Select(f).ToList();
        public static int Choose(params string[] options) =>Choose(null,options);
        //TODO BLOCK: bools: spaceIsEdge, spaceIsCorner, borderIsVisible, ascii fonts, index = option % possibleoptions
        public static int Choose(KeyMenu[] keyMenus,params string[] options){
            //option to jump to start for continuous scrolling?
            if(!Continue) Initiate(options.Length);
            else Continue=false;
            //TODO: continue
            //Continue =!;, =; <- is this possible in c#
            var maxLength=options.Max(s=>s.Length);
            while(true)//change appearance of arrow? >/-
                while(true){
                    var oldIndex=Index;
                    switch(Input){
                        case UpArrow:
                            Option--;
                            Index--;
                            break;
                        case DownArrow:
                            Option++;
                            Index++;
                            break;
                        //use in Spend() as well - savebuffer() loadbuffer(), async + await?, set positions - PositionSystem - spacing, vertical spacing, frame-ify everything
                        case Enter:
                            return Option;
                        case default(ConsoleKey):
                            break;
                        default:
                            int index;
                            if(keyMenus==null){
                                Input=ReadKey(true).Key;
                                continue;
                            }
                            //TODO: de-null check
                            if((index=keyMenus.ToList().FindIndex(k=>k.Key==Input))!=-1){
                                Continue=true;
                                return -index;
                            }
                            //TODO: is default value -1?
                            continue;
                    }
                    Maths.RMod(ref Option,TotalOptions);
                    if(ScrollIsEnabled){
                        if(Index<0) PagePlus=-1;
                        else if(Index>=PossibleOptions) PagePlus=1;
                    } else Index=Option;
                    if(PagePlus!=0){
                        TurnPage();
                        var i=0;
                        for(;i<PossibleOptions;i++) options[Indices[i]].WriteAt(Left+2,Top+i,maxLength);
                        for(;i<MaxLines;i++) "".WriteAt(Left+2,Top+i,maxLength);
                        PagePlus=0;//TODO: pass arrays as parameters to turnpage?
                    }
                    ' '.WriteAt(Left,oldIndex+Top);
                    '>'.WriteAt(Left,Index+Top);
                    Input=ReadKey(true).Key;
                }
        }
        public static List<string> WordWrap(string paragraph,
            int width=int.MaxValue,
            bool wordWrap=true,
            bool trim=true,
            bool write=true){
            var left=CursorLeft;
            var top=CursorTop;
            var n=0;
            var lines=new List<string>();
            width=Math.Min(width,WindowWidth-left);
            if(trim) paragraph=new Regex($"{Str.Separators}{{2,}}").Replace(paragraph.Trim()," ");
            for(;paragraph.Length>0;n++){
                lines.Add(paragraph.Substring(0,Math.Min(width-left,paragraph.Length)));
                var length=lines[n].LastIndexOf(' ');
                if(wordWrap&&paragraph.Length>WindowWidth-left&&length>0) lines[n]=lines[n].Remove(length);
                paragraph=paragraph.Substring(Math.Min(lines[n].Length+1,paragraph.Length));
                if(write) lines[n].WriteAt(left,top+n);
            }
            return lines;
        }
        public static int Length(this object o) {return o.ToString().Length;}
        public static List<T> NewList<T>(int count,T value) {return Enumerable.Repeat(value,count).ToList();}
        public static void TurnPage(){
            Page+=PagePlus;
            Maths.RMod(ref Page,Pages);
            if(Pages>0&&PageNumIsVisible&&ScrollIsEnabled&&!ScrollingIsContinuous) $"{Page+1}/{Pages+1}".WriteAt(Left+2,Top);
            //TODO: page + 1 > 10
            Scroll.RecalculateIndex();
            Indices=Scroll.CalculateIndices();
        }
        /// <summary>
        ///     Lets the user spend currency to obtain items.
        /// </summary>
        /// <param name="text">Current currency in the format "You have "," currency"," left"</param>
        /// <param name="items">The list of items</param>
        /// <param name="currency">The currency the user has</param>
        /// <param name="currencyLeft">The final currency of the user</param>
        /// <param name="singular">The singular form in place of " currency"</param>
        /// <param name="textZero">The full text to display when the user has no currency left</param>
        /// <returns>The final number of each item</returns>
        public static Item[] Spend(string[] text,
            Item[] items,
            int currency,
            out int currencyLeft,
            string singular=null,
            string textZero=null){
            //TODO: continue(); extract common methods, add ISelectable maybe, this is really clunky, currency text placement
            Initiate(items.Length);
            var maxNameLength=items.Max(n=>n.Name.Length);
            var sellIsEnabled=items.All(v=>v.Value>=0);//TODO: currency.Length(), maximum currency after selling 
            var maxCurrencyLength=
                Math.Max(
                    currency.Length()+text[0].Length+text[1].Length+text[2].Length+
                        Math.Max(0,(singular=singular??text[1].RemoveLast()).Length-text[1].Length),
                    textZero?.Length??0);
            var line=Top+1;
            var totalItems=items.ToArray();
            var amtIsChanged=false;
            while(true){
                switch(Input){
                    case UpArrow:
                        Option--;
                        Index--;
                        break;
                    case DownArrow:
                        Option++;
                        Index++;
                        break;
                    case LeftArrow:
                        if(totalItems[Option].Amount-items[Option].Amount>0){
                            totalItems[Option].Amount--;
                            currency+=items[Option].Cost;
                            amtIsChanged=true;
                        } else if(sellIsEnabled&&totalItems[Option].Amount>0){
                            totalItems[Option].Amount--;
                            currency+=items[Option].Value;
                            amtIsChanged=true;
                        }
                        break;
                    case RightArrow:
                        if(currency-items[Option].Cost>=0){
                            totalItems[Option].Amount++;
                            currency-=items[Option].Cost;
                            amtIsChanged=true;
                        } else if(sellIsEnabled&&currency-items[Option].Value>=0&&
                            totalItems[Option].Amount-items[Option].Amount<0){
                            totalItems[Option].Amount++;
                            currency-=items[Option].Value;
                            amtIsChanged=true;
                        }
                        break;
                    //TODO: test
                    case Enter:
                        currencyLeft=currency;
                        return totalItems;
                    default:
                        Input=ReadKey(true).Key;
                        continue;
                }
                if(amtIsChanged){
                    totalItems[Option].Amount.WriteAt(maxNameLength+2,line,totalItems[Option].Length()+1);
                    var s=textZero;
                    if(currency!=0||textZero==null) s=text[0]+(currency==0?"no":currency.ToString())+(currency==1?singular:text[1])+text[2];
                    //TODO: simplify
                    s.WriteAt(0,Top+MaxLines+2,maxCurrencyLength);
                }
                Maths.RMod(ref Option,TotalOptions);
                if(ScrollIsEnabled)//TODO: draw on first run if scrollisdisabled, rangesign in maths
                    if((PagePlus=Index<0?-1:Index>=PossibleOptions?1:0)!=0){//TODO: change for choose
                        TurnPage();
                        var maxValueLength=totalItems.Max(t=>t.Value).Length();
                        var top=Top;
                        foreach(var i in Indices){
                            items[i].Name.WriteAt(2,++top,maxNameLength);
                            totalItems[i].Amount.WriteAt(maxNameLength+3,top,maxValueLength);
                        }
                        for(var i=Indices.Count;i<MaxLines;i++) "".WriteAt(2,++top,1+maxNameLength+maxValueLength);
                    }
                if(Input==UpArrow||Input==DownArrow){
                    ' '.WriteAt(Left,line);
                    '>'.WriteAt(Left,line=Index+Top+1);
                }
                Input=ReadKey(true).Key;
                amtIsChanged=false;
                PagePlus=0;
            }
        }
        public class Continuous:IScrollable{
            public void RecalculateIndex() {REnsureBetween(ref Index,0,PossibleOptions-1);}
            public List<int> CalculateIndices() {return PossibleOptions.Range(i=>(Page+i)%TotalOptions);}
        }
        public class PageByPage:IScrollable{
            public void RecalculateIndex(){
                PossibleOptions=Math.Min(TotalOptions-MaxLines*Page,MaxLines);
                Index=PagePlus==1?0:PagePlus==-1?PossibleOptions-1:Index;
            }
            public List<int> CalculateIndices() {return PossibleOptions.Range(i=>i+(MaxLines*Page));}
        }
    }
}
