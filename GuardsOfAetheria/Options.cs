namespace GuardsOfAetheria{
    using System;
    using GuardsOfAetheria.Properties;
    using Improved;
    using Improved.Consoles;
    using static System.Console;
    using static System.ConsoleKey;
    public class Options{
        public static string[] Names={"Menu scrolling","Page numbers"};
        public static string[][] Strings={new[]{"Pages","Scroll"},new[]{"Hidden","Visible"}};
        public static object[][] List={new object[]{false,true},new object[]{false,true}};
        public static void Load(){
            Consoles.SetBools();
            Consoles.Scrolling["continuous"]=Settings.Default.Menu_scrolling;
            Consoles.PageNum["visible"]=Settings.Default.Page_numbers;
        }
        public static void Change(){
            //TODO: scrolling
            "Options\n\n".WriteAt();
            var top=CursorTop;
            const int left=38;
            const int spacing=12;
            var number=0;
            var oldNumber=0;
            CursorLeft=0;
            foreach(var name in Names) WriteLine(name);
            for(var i=0;i<Names.Length;i++) for(var j=0;j<List[i].Length;j++) Strings[i][j].WriteAt(left+2+spacing*j,top+i);
            var choice=Array.IndexOf(List[number],Settings.Default[Names[number].Replace(" ","_")]);
            '>'.WriteAt(left+spacing*choice,top+number);
            while(true){
                var input=ReadKey(true).Key;
                ' '.WriteAt(left+spacing*choice,top+number);
                switch(input){
                    case UpArrow:
                        number--;
                        break;
                    case DownArrow:
                        number++;
                        break;
                    case LeftArrow:
                        choice--;
                        break;
                    case RightArrow:
                        choice++;
                        break;
                    case Enter:
                        Settings.Default[Names[number].Replace(" ","_")]=List[number][choice];
                        Settings.Default.Save();
                        Load();
                        return;
                }
                if(input==DownArrow||input==UpArrow){
                    Settings.Default[Names[oldNumber].Replace(" ","_")]=List[oldNumber][choice];
                    Maths.RMod(ref number,Names.Length);
                    choice=Array.IndexOf(List[number],Settings.Default[Names[number].Replace(" ","_")]);
                } else Maths.RMod(ref choice,List[number].Length);
                '>'.WriteAt(left+spacing*choice,top+number);
                oldNumber=number;
            }
        }
    }
}
