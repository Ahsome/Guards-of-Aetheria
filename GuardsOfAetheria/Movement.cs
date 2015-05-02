using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Improved;
using Improved.Consoles;
using static System.Console;

namespace GuardsOfAetheria
{
    internal static class Movement
    {
        public static Dictionary<string, object> VariableDictionary = new Dictionary<string, object> { ["Name"] = Player.Instance.Name };
        public static void ShowLocation()
        {
            var arrays = new Dictionary<string, string[]> {["Option Text"] = new[]{""}, ["Room IDs"] = new[]{""}, ["Variables"] = new[]{""}};
            var objects = new Dictionary<string, object> {["Text to Display"] = "", ["Room Name"] = ""};
            Database.GetData($"SELECT * FROM Rooms WHERE ID = {Player.Instance.RoomId}", objects, arrays);
            Title = $"Guards of Aetheria - {Player.Instance.Name} at {objects["Room Name"]}";
            Clear();
            Consoles.WordWrap(string.Format(Regex.Unescape((string)objects["Text to Display"]), (from o in arrays["Variables"] select VariableDictionary[o]).ToArray()));
            SetCursorPosition(0, ++CursorTop); Player.Instance.RoomId = arrays["Room IDs"][arrays["Option Text"].Choose()].ToInt();
        }
    }
}