namespace GuardsOfAetheria{
    using Improved;
    using Improved.Consoles;
    using static System.Console;
    using static Improved.Lists;
    using static System.Text.RegularExpressions.Regex;
    using static Toolbox;
    internal static class Movement{
        public static Dict<string,object> Vars=new Dict<string,object>{["Name"]=Bag.Player().Name};
        public static void ShowLocation(){
            Clear();
            var data=
                $"SELECT * FROM Rooms WHERE ID = {Bag.Player().RoomId}".OleDbRead(new[]{"Text to Display","Room Name"},
                    new[]{"Option Text","Room IDs","Variables"});
            var objects=data.Item1;
            var arrays=data.Item2;
            Title=$"Guards of Aetheria - {Bag.Player().Name} at {objects["Room Name"]}";
            Unescape((string)objects["Text to Display"]).WriteAt(false,Vars[arrays["Variables"]]);
            SetCursorPosition(0,++CursorTop);
            Bag.Player().RoomId=arrays["Room IDs"][Consoles.Choose(arrays["Option Text"])].ToNum();
        }//TODO: more complex interface etc, 'Save' room (and prev room id) :)
    }
}
