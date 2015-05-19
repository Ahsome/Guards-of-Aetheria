using static System.Console;
using static System.Text.RegularExpressions.Regex;
using static GuardsOfAetheria.Toolbox;
namespace GuardsOfAetheria{
    using System.Collections.Generic;
    using System.Linq;
    using Improved;
    using Improved.Consoles;
    internal static class Movement{
        public static Dictionary<string,object> VarDict=new Dictionary<string,object>{["Name"]=Bag.Player().Name};
        public static void ShowLocation(){
            var data=
                $"SELECT * FROM Rooms WHERE ID = {Bag.Player().RoomId}".OleDbRead(new[]{"Text to Display","Room Name"},
                    new[]{"Option Text","Room IDs","Variables"});
            var objects=data.Item1;
            var arrays=data.Item2;
            Title=$"Guards of Aetheria - {Bag.Player().Name} at {objects["Room Name"]}";
            Unescape((string)objects["Text to Display"])
                .CWrite(false,o:(from o in arrays["Variables"] select VarDict[o]).ToArray());
            SetCursorPosition(0,++CursorTop);
            Bag.Player().RoomId=arrays["Room IDs"][Consoles.Choose(arrays["Option Text"])].ToNum();
        }//TODO: more complex interface etc, 'Save' room (and prev room id) :)
    }
}
