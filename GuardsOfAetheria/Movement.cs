using System.Collections.Generic;
using System.Linq;
using Improved;
using Improved.Consoles;
using static System.Console;
using static System.Text.RegularExpressions.Regex;

namespace GuardsOfAetheria {
    internal static class Movement {
        public static Dictionary<string,object> VarDict = new Dictionary<string,object> {["Name"]=B.Ag.Player().Name };
        public static void ShowLocation() {
            var data = $"SELECT * FROM Rooms WHERE ID = {B.Ag.Player().RoomId}".OleDbRead(
                new[] { "Text to Display","Room Name" },new[] { "Option Text","Room IDs","Variables" });
            var objects = data.Item1; var arrays = data.Item2;
            Title=$"Guards of Aetheria - {B.Ag.Player().Name} at {objects["Room Name"]}";
            Unescape((string)objects["Text to Display"]).CWrite(
                (from o in arrays["Variables"] select VarDict[o]).ToArray());
            SetCursorPosition(0,++CursorTop); B.Ag.Player().RoomId=arrays["Room IDs"][arrays["Option Text"].Choose()].ToNum();
        } //TODO: more complex interface etc, 'Save' room (and prev room id) :)
    }
}