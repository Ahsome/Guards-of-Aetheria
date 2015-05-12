using static System.Console;
using static System.ConsoleKey;
namespace Improved.Consoles{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Text.RegularExpressions;
    public class CustomIo{//TODO: fix > 255 char stuff
        public static string ReadLine(Dictionary<int,string> maxPerWord,bool lettersNeeded=true,int minLength=1,
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
    public static class Lists{
        public static List<T> List<T>(params T[] i) {return i.ToList();}
        public static Dictionary<TKey,TValue> Dict<TKey,TValue>(params KeyValuePair<TKey,TValue>[] i){
            return i.ToDictionary(k=>k.Key,k=>k.Value);
        }
        public static Dictionary<TKey,TValue> ToDict<TKey,TValue>(this TKey[] l,Func<TKey,TValue> f){
            return l.ToDictionary(k=>k,f);
        }
        public static LinkedList<T> LList<T>(params T[] i) {return new LinkedList<T>(i);}
        public static LinkedListNode<T> Lln<T>(T i) {return new LinkedListNode<T>(i);}
        public static HashSet<T> HSet<T>(params T[] i) {return new HashSet<T>(i);}
        public static SortedDictionary<TKey,TValue> SDict<TKey,TValue>(params KeyValuePair<TKey,TValue>[] i){
            return new SortedDictionary<TKey,TValue>(Dict(i));
        }
        public static SortedList<TKey,TValue> SList<TKey,TValue>(params KeyValuePair<TKey,TValue>[] i){
            return new SortedList<TKey,TValue>(Dict(i));
        }
        public static SortedSet<T> SSet<T>(params T[] i) {return new SortedSet<T>(i);}
        public static Stack<T> Stack<T>(params T[] i) {return new Stack<T>(i);}
        public static Queue<T> Queue<T>(params T[] i) {return new Queue<T>(i);}
        public static KeyValuePair<TKey,TValue> Kvp<TKey,TValue>(TKey key,TValue value){
            return new KeyValuePair<TKey,TValue>(key,value);
        }
        public static BindingList<T> BList<T>(params T[] i) {return new BindingList<T>(i);}
        public static ConcurrentBag<T> CBag<T>(params T[] i) {return new ConcurrentBag<T>(i);}
        public static ConcurrentDictionary<TKey,TValue> CDict<TKey,TValue>(params KeyValuePair<TKey,TValue>[] i){
            return new ConcurrentDictionary<TKey,TValue>(i);
        }
        public static ConcurrentQueue<T> CQueue<T>(params T[] i) {return new ConcurrentQueue<T>(i);}
        public static ConcurrentStack<T> CStack<T>(params T[] i) {return new ConcurrentStack<T>(i);}
        public static BlockingCollection<T> BColl<T>(this T i) where T:IProducerConsumerCollection<T>{
            return new BlockingCollection<T>(i);
        }
        public static Collection<T> Coll<T>(params T[] i) {return new Collection<T>(i);}
        public static ObservableCollection<T> OColl<T>(params T[] i) {return new ObservableCollection<T>(i);}
        public static ReadOnlyCollection<T> RoColl<T>(params T[] i) {return new ReadOnlyCollection<T>(i);}
        public static ReadOnlyDictionary<TKey,TValue> RoDict<TKey,TValue>(params KeyValuePair<TKey,TValue>[] i){
            return new ReadOnlyDictionary<TKey,TValue>(Dict(i));
        }
        public static ReadOnlyObservableCollection<T> RooColl<T>(params T[] i){
            return new ReadOnlyObservableCollection<T>(OColl(i));
        }
        public static ArraySegment<T> ASeg<T>(int offset,int count,params T[] i){
            return new ArraySegment<T>(i,offset,count);
        }
        public static WeakReference<T> WRef<T>(T i) where T:class {return new WeakReference<T>(i);}
        public static Tuple<T> Tuple<T>(T i) {return new Tuple<T>(i);}
        public static Tuple<T1,T2> Tuple<T1,T2>(T1 i1,T2 i2) {return new Tuple<T1,T2>(i1,i2);}
        public static Tuple<T1,T2,T3> Tuple<T1,T2,T3>(T1 i1,T2 i2,T3 i3) {return new Tuple<T1,T2,T3>(i1,i2,i3);}
        public static Tuple<T1,T2,T3,T4> Tuple<T1,T2,T3,T4>(T1 i1,T2 i2,T3 i3,T4 i4){
            return new Tuple<T1,T2,T3,T4>(i1,i2,i3,i4);
        }
        public static Tuple<T1,T2,T3,T4,T5> Tuple<T1,T2,T3,T4,T5>(T1 i1,T2 i2,T3 i3,T4 i4,T5 i5){
            return new Tuple<T1,T2,T3,T4,T5>(i1,i2,i3,i4,i5);
        }
        public static Tuple<T1,T2,T3,T4,T5,T6> Tuple<T1,T2,T3,T4,T5,T6>(T1 i1,T2 i2,T3 i3,T4 i4,T5 i5,T6 i6){
            return new Tuple<T1,T2,T3,T4,T5,T6>(i1,i2,i3,i4,i5,i6);
        }
        public static Tuple<T1,T2,T3,T4,T5,T6,T7> Tuple<T1,T2,T3,T4,T5,T6,T7>(T1 i1,T2 i2,T3 i3,T4 i4,T5 i5,T6 i6,T7 i7){
            return new Tuple<T1,T2,T3,T4,T5,T6,T7>(i1,i2,i3,i4,i5,i6,i7);
        }
        public static Tuple<T1,T2,T3,T4,T5,T6,T7,TRest> Tuple<T1,T2,T3,T4,T5,T6,T7,TRest>(T1 i1,T2 i2,T3 i3,T4 i4,T5 i5,
            T6 i6,T7 i7,TRest iRest){
            return new Tuple<T1,T2,T3,T4,T5,T6,T7,TRest>(i1,i2,i3,i4,i5,i6,i7,iRest);
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
        public static void CWrite(this string s,params object[] o){
            Clear();
            Write(s,o);
        }
        //TODO: text position - left, centre, right, justified
        public static void ShowPageNum(int left=50,int top=-1){
            if(top<0||top>WindowHeight) top=Top;
            if(Pages<=0||!PageNumIsVisible||!ScrollIsEnabled||ScrollingIsContinuous) return;
            ((Page+1)+"/"+(Pages+1)).WriteAt(left+2,top);
            //TODO: page + 1 > 10
        }
        /// <summary>
        ///     Removes the last character in a string.
        /// </summary>
        /// <param name="input">The string to remove the last character from</param>
        /// <returns>The modified string</returns>
        public static string RemoveLast(this string input){
            return input.Remove(input.Length-1);
        }
        /// <summary>
        ///     Removes the last characters in a string.
        /// </summary>
        /// <param name="input">The string to remove characters from</param>
        /// <param name="numberToRemove">The number of characters to remove</param>
        /// <returns>The modified string</returns>
        public static string RemoveLast(this string input,int numberToRemove){
            return input.Remove(input.Length-numberToRemove);
        }
        //TODO: rename params
        /// <summary>
        ///     Writes a string vertically, one character on each line.
        /// </summary>
        /// <param name="s">The string to write</param>
        public static void WriteVertical(this string s){
            s.WriteVertical(CursorLeft,CursorTop);
        }
        /// <summary>
        ///     Writes a string vertically, one character on each line, at a specified position.
        /// </summary>
        /// <param name="s">The string to write</param>
        /// <param name="left">The column to write the string at</param>
        /// <param name="top">The row to write the string at</param>
        public static void WriteVertical(this string s,int left,int top){
            for(var i=1;i<=s.Length;i++) WriteAt(s[i-1],left,top+i,0);
        }
        /// <summary>
        ///     Writes a character at a specified position, padded to the right of the window.
        /// </summary>
        /// <param name="c">The character to write</param>
        /// <param name="left">The column to write the string at</param>
        /// <param name="top">The row to write the string at</param>
        public static void WriteAt(this char c,int left,int top){
            SetCursorPosition(left,top);
            Write(c);
        }
        /// <summary>
        ///     Writes a character at a specified position, padded to a maximum length.
        /// </summary>
        /// <param name="o">The object to write</param>
        /// <param name="left">The column to write the string at</param>
        /// <param name="top">The row to write the string at</param>
        /// <param name="limit">The amount of the padding</param>
        public static void WriteAt(this object o,int left,int top,int limit=-1){
            SetCursorPosition(left,top);
            Write(o.ToString().PadRight((limit<0||limit>=WindowWidth)?WindowWidth-left-1:limit));
        }
        public static void REnsureBetween(ref int n,int min,int max) {n=n.EnsureBetween(min,max);}
        public static int EnsureBetween(this int n,int min,int max) {return Math.Max(min,Math.Min(max,n));}
        public static void WriteBorder(this string s,int width=int.MaxValue){
            REnsureBetween(ref width,1,WindowWidth-1);
            for(var i=0;i<=width;i+=1) Write(s[i%s.Length]);
        }
        public static int Choose(params string[] options) {return Choose(null,options);}
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
        public static List<string> WordWrap(string paragraph,int width=int.MaxValue,bool wordWrap=true,bool trim=true,
            bool write=true){
            var left=CursorLeft;
            var top=CursorTop;
            var numberOfLines=0;
            var lines=new List<string>();
            width=Math.Min(width,WindowWidth-left);
            if(trim) paragraph=new Regex(" {2,}").Replace(paragraph.Trim()," ");
            for(;paragraph.Length>0;numberOfLines++){
                lines.Add(paragraph.Substring(0,Math.Min(width-left,paragraph.Length)));
                int length;
                if(wordWrap&&paragraph.Length>WindowWidth-left&&(length=lines[numberOfLines].LastIndexOf(' '))>0) lines[numberOfLines]=lines[numberOfLines].Remove(length);
                paragraph=paragraph.Substring(Math.Min(lines[numberOfLines].Length+1,paragraph.Length));
                if(write) lines[numberOfLines].WriteAt(left,top+numberOfLines);
            }
            return lines;
        }
        public static List<T> NewList<T>(int count,T value) {return Enumerable.Repeat(value,count).ToList();}
        public static void TurnPage(){
            Page+=PagePlus;
            Maths.RMod(ref Page,Pages);
            ShowPageNum();
            Scroll.RecalculateIndex();
            Indices=Scroll.CalculateIndices();
        }
        /// <summary>
        ///     Lets the user spend currency to obtain items.
        /// </summary>
        /// <param name="text">
        ///     The text, after the spend screen, that tells the user the amount of currency left, in the format [0]
        ///     = "You have ", [1] = " currency", [2] = " left"
        /// </param>
        /// <param name="items">The list of items</param>
        /// <param name="currency">The currency the user has</param>
        /// <param name="currencyLeft">The remaining currency of the user</param>
        /// <param name="singular">The singular form, in place of " currency"</param>
        /// <param name="textZero">The full text to display when the user has no currency left</param>
        /// <returns>The total number of each item</returns>
        public static Item[] Spend(string[] text,Item[] items,int currency,out int currencyLeft,string singular=null,
            string textZero=null){
            //TODO: continue(); extract common methods, add ISelectable maybe, this is really clunky
            Initiate(items.Length);
            var maxNameLength=items.Max(n=>n.Name.Length);
            var sellIsEnabled=items.All(v=>v.Value>=0);
            var maxCurrencyLength=
                Math.Max(
                    currency.ToString().Length+text[0].Length+text[1].Length+text[2].Length+
                        Math.Max(0,(singular=singular??text[1].RemoveLast()).Length-text[1].Length),textZero?.Length??0);
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
                    totalItems[Option].Amount.WriteAt(maxNameLength+2,line,totalItems[Option].ToString().Length+1);
                    var s=textZero;
                    if(currency!=0||textZero==null) s=text[0]+(currency==0?"no":currency.ToString())+(currency==1?singular:text[1])+text[2];
                    //TODO: simplify
                    s.WriteAt(0,Top+MaxLines+2,maxCurrencyLength);
                }
                Maths.RMod(ref Option,TotalOptions);
                if(ScrollIsEnabled)//TODO: draw on first run if scrollisdisabled, rangesign in maths
                    if((PagePlus=Index<0?-1:Index>=PossibleOptions?1:0)!=0){//TODO: change for choose
                        TurnPage();
                        var maxValueLength=totalItems.Max(t=>t.Value).ToString().Length;
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
            public List<int> CalculateIndices(){
                var indices=new List<int>();
                for(var i=0;i<PossibleOptions;i++) indices.Add((Page+i)%TotalOptions);
                return indices;
            }
        }
        public class PageByPage:IScrollable{
            public void RecalculateIndex(){
                PossibleOptions=Math.Min(TotalOptions-MaxLines*Page,MaxLines);
                switch(PagePlus){
                    case 1:
                        Index=0;
                        break;
                    case -1:
                        Index=PossibleOptions-1;
                        break;
                }
            }
            public List<int> CalculateIndices(){
                var indices=new List<int>();
                for(var i=0;i<PossibleOptions;i++) indices.Add(i+(MaxLines*Page));
                return indices;
            }
        }
    }
}
