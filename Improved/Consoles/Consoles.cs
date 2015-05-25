namespace Improved.Consoles{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using static Lists;
    using static System.Console;
    using static System.ConsoleKey;
    public class BoolObject{
        public Dictionary<string,string> Opposites=new Dictionary<string,string>();
        public Dictionary<string,bool> Values=new Dictionary<string,bool>();
        public BoolObject(params string[] names) {foreach(var n in names) Values.Add(n,true);}
        public BoolObject(params KeyValuePair<string,bool>[] names) {foreach(var n in names) Values.Add(n.Key,n.Value);}
        public BoolObject(params KeyValuePair<string,string>[] opposites){
            foreach(var n in opposites){
                Opposites.Add(n.Key,n.Value);
                Values.Add(n.Value,true);
            }
        }
        public bool this[string s]{
            get{
                try {
                    return Values[s];
                } catch {
                    return !Values[Opposites[s]];
                }
            }
            set {
                try {
                    Values[s]=value;
                } catch {
                    Values[Opposites[s]]=!value;
                }
            }
        }
        public BoolObject AddOpposites(params KeyValuePair<string,string>[] opposites){
            foreach(var o in opposites) Opposites.Add(o.Key,o.Value);
            return this;
        }
        public BoolObject Opp(params KeyValuePair<string,string>[] opposites) =>AddOpposites(opposites);
        public bool Is(string s) =>this[s];
        public bool IsNot(string s) =>!this[s];
        public bool Are(string s) =>this[s];
        public bool AreNot(string s) =>!this[s];
        public bool IsBeing(string s) =>this[s];
        public bool IsNotBeing(string s) =>!this[s];
        public bool AreBeing(string s) =>this[s];
        public bool AreNotBeing(string s) =>!this[s];
        public bool Was(string s) =>this[s];
        public bool WasNot(string s) =>!this[s];
        public bool Were(string s) =>this[s];
        public bool WereNot(string s) =>!this[s];
        public bool HasBeen(string s) =>this[s];
        public bool HasNotBeen(string s) =>!this[s];
        public bool HaveBeen(string s) =>this[s];
        public bool HaveNotBeen(string s) =>!this[s];
    }
    public class CustomIo{//TODO: fix > 255 char stuff
        public static string ReadLine(Dictionary<int,string> maxPerWord,
            bool lettersNeeded=true,
            int minLength=1,
            int maxLength=255){//TODO:minperword? min/max length? 
            var x=CursorLeft;
            var y=CursorTop; //TODO: disable more than maxlength, fix backspacing
            var s="";
            while(true){
                var k=ReadKey(true).KeyChar;
                if(!char.IsLetter(k)&&k!='\b'&&Str.Separators.All(c=>c!=k)&&maxPerWord.Values.All(v=>v.All(c=>c!=k))) continue;
                var lastSpace=Math.Max(0,Str.Separators.Max(sep=>s.LastIndexOf(sep)));
                var lastWord=s.Substring(lastSpace);
                if(maxPerWord.Any(max=>lastWord.Count(c=>max.Value.Any(m=>m==c))>=max.Key&&max.Value.Any(c=>c==k))) continue;
                if(k=='\b'){
                    if(s.Length<=0) continue;
                    (s=s.RemoveLast()).WriteAt(x,y,maxLength:s.Length+1);
                    s.WriteAt(x,y,maxLength:s.Length);//TODO: do something else to get cursor back into place
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
    public interface ISelect{
        void TurnPage();
    }
    public static class Consoles{
        public static bool Continue;
        public static BoolObject Scrolling,PageNum,ThePage;
        public static int Left,Top,Option,Index,Page,MaxLines,PagePlus,TotalOptions,PossibleOptions,Pages;
        public static ISelect Select;
        public static ConsoleKey Input;
        public static IScrollable Scroll;
        public static List<int> Indices=new List<int>();
        //TODO: rearrange methods, readability, replace footerheight with frame, frameify spend and select
        public static void SetBools(){
            Scrolling=new BoolObject("continuous").Opp(Kvp("disabled","enabled"));
            PageNum=new BoolObject(Kvp("hidden","visible"));
            ThePage=new BoolObject("turned");
        }
        public static void Initiate(int arrayLength) {Initiate(arrayLength,WindowHeight-3);}
        public static void Initiate(int arrayLength,int lastLine,int left=-1){
            Select=new FirstRun();
            Aligned.Initiate(Alignment.Left);
            if(Scrolling.Is("continuous")) Scroll=new Continuous();
            else Scroll=new PageByPage();
            Option=Index=PagePlus=1;
            Page=-1;
            Input=UpArrow;
            Left=left==-1?0:left;
            Top=CursorTop+1;
            MaxLines=lastLine-Top;
            TotalOptions=arrayLength;
            PossibleOptions=Math.Min(MaxLines,TotalOptions);
            Pages=Scrolling.Is("continuous")?TotalOptions:1+TotalOptions/MaxLines;
            Scrolling["enabled"]=TotalOptions>MaxLines;
        }
        public static string RemoveLast(this string input,int numberToRemove=1)
            =>input.Length==0?input:input.Remove((input.Length-numberToRemove).EnsureBetween(0,input.Length));
        public static void WriteVertical(this string s,int left=-1,int top=-1){
            left=left==-1?CursorLeft:left;
            top=top==-1?CursorTop:top;
            foreach(var c in s) c.WriteAt(left,top++);
        }//TODO: ++top?
        public static void REnsureBetween(ref int n,int min,int max) {n=n.EnsureBetween(min,max);}
        public static int EnsureBetween(this int n,int min,int max) =>n<min?min:n>max?max:n;
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
        public static int Choose(params string[] items) =>Choose(null,items).First();
        //TODO BLOCK: bools: spaceIsEdge, spaceIsCorner, borderIsVisible, ascii fonts, index = option % possibleoptions
        public static IEnumerable<int> Choose(KeyMenu[] keyMenus,params string[] items){
            //MAYBE: option to jump to start for continuous scrolling?
            if(!Continue) Initiate(items.Length);
            else Continue=false;
            //TODO: continue
            //Continue =!;, =; <- is this possible in c#
            var maxLength=items.Max(s=>s.Length);//MAYBE: option to change appearance of arrow? >/-
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
                        yield return Option;
                        yield break;
                    case default(ConsoleKey):
                        break;
                    default:
                        var index=keyMenus.ToList().FindIndex(k=>k.Key==Input);
                        if(index!=-1){
                            Continue=true;
                            yield return -index;
                        }
                        Input=ReadKey(true).Key;
                        //TODO: is default value -1?
                        continue;
                }
                Maths.RMod(ref Option,TotalOptions);
                Select.TurnPage();
                if(ThePage.Was("turned")){
                    var i=0;
                    for(;i<PossibleOptions;i++) items[Indices[i]].WriteAt(Left+2,Top+i,maxLength);
                    for(;i<MaxLines;i++) "".WriteAt(Left+2,Top+i,maxLength);
                    ThePage["turned"]=false;//TODO: pass arrays as parameters to turnpage? look up how arrays are passed
                }
                ' '.WriteAt(Left,oldIndex+Top);
                '>'.WriteAt(Left,Index+Top);
                Input=ReadKey(true).Key;
            }
        }
        public static string Trm(this string s) =>s.Trim(Str.Separators.ToCharArray());
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
            if(trim) paragraph=new Regex($"{Str.Separators}{{2,}}").Replace(paragraph.Trm()," ");
            for(;paragraph.Length>0;n++){
                lines.Add(paragraph.Substring(0,Math.Min(width-left,paragraph.Length)));
                var length=lines[n].LastIndexOf(' ');
                if(wordWrap&&paragraph.Length>WindowWidth-left&&length>0) lines[n]=lines[n].Remove(length);
                paragraph=paragraph.Substring(Math.Min(lines[n].Length+1,paragraph.Length));
                if(write) lines[n].WriteAt(left,top+n);
            }
            return lines;
        }
        public static int Length(this object o) =>o.ToString().Length;
        /// <summary>
        ///     Lets the user spend currency to obtain items.
        /// </summary>
        /// <param name="text">Current currency in the format "You have "," currency"," left"</param>
        /// <param name="items">Current item list</param>
        /// <param name="currency">Current currency</param>
        /// <param name="currencyLeft">Final currency</param>
        /// <param name="singular">Singular form of " currency"</param>
        /// <param name="textZero">Text when the user has no currency left</param>
        /// <returns>Final item list</returns>
        public static Item[] Spend(string[] text,
            List<Item> items,
            int currency,
            out int currencyLeft,
            string singular=null,
            string textZero=null){
            //TODO: continue(); extract common methods, add ISelectable maybe, this is really clunky, currency text placement
            Initiate(items.Count);
            var maxNameLength=items.Max(n=>n.Name.Length);
            var sellIsEnabled=items.All(v=>v.Value>=0);//TODO: account for maximum currency after selling
            var maxCurrencyLength=Math.Max(//TODO: i +=?
                items.Aggregate(currency,(i,j)=>j.Value).Length()+text.Aggregate(0,(i,j)=>j.Length)+//TODO: first 3 only
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
                    //case default(ConsoleKey):
                    //    amtIsChanged=true;
                    //    break;
                    default:
                        Input=ReadKey(true).Key;
                        continue;
                }
                if(amtIsChanged){
                    totalItems[Option].Amount.WriteAt(maxNameLength+2,line,totalItems[Option].Length()+1);
                    (currency!=0||textZero==null?text[0]+(currency==0?"no":currency.ToString())+(currency==1?singular:text[1])+text[2]:textZero).WriteAt(0,Top+MaxLines+2,maxCurrencyLength);
                }
                Maths.RMod(ref Option,TotalOptions);
                Select.TurnPage();
                if(ThePage.Was("turned")){//TODO: change for choose
                        Select.TurnPage();
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
            public List<int> CalculateIndices() =>PossibleOptions.Range(i=>(Page+i+1)%TotalOptions);
        }
        public class PageByPage:IScrollable{
            public void RecalculateIndex(){
                PossibleOptions=Math.Min(TotalOptions-MaxLines*Page,MaxLines);
                Index=PagePlus==1?0:PagePlus==-1?PossibleOptions-1:Index;
            }
            public List<int> CalculateIndices() =>PossibleOptions.Range(i=>i+(MaxLines*Page));
        }
        public class FirstRun:ISelect{
            public void TurnPage(){
                Indices=Scroll.CalculateIndices();
                ThePage["turned"]=true;Select=new Normal();
            }
        }
        public class Normal:ISelect{
            public void TurnPage(){
            if((PagePlus=Index<0?-1:Index>=PossibleOptions?1:0)!=0) ThePage["turned"]=true;
            if(Scrolling.Is("enabled")) Page+=PagePlus;
            else Index=Option;
            Maths.RMod(ref Page,Pages);
            if(Pages>0&&PageNum.Is("visible")&&Scrolling.Is("enabled")&&Scrolling.IsNot("continuous")) $"{Page+1}/{Pages+1}".WriteAt(Left+2,Top);
            //TODO: page + 1 > 10
            Scroll.RecalculateIndex();
            Indices=Scroll.CalculateIndices();
            PagePlus=0;
        }
        }
    }
}
