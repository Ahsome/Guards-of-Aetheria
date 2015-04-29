using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Improved;

namespace GuardsOfAetheria
{
    internal static class Movement
    {
        public static Dictionary<string, object> VariableDictionary = new Dictionary<string, object>
        {
            { "Name", Player.Instance.Name }
        };
        public static void ShowLocation()
        {
            Console.Clear();
            var arrays = new Dictionary<string, string[]>
            {
                {"Option Text", new[]{""}},
                {"Room IDs", new[]{""}},
                {"Variables", new[]{""}}
            };
            var objects = new Dictionary<string, object>
            {
                {"Text to Display", ""},
                {"Room Name", ""}
            };
            Database.GetData(String.Format("SELECT * FROM Rooms WHERE ID = {0}", Player.Instance.RoomId), objects, arrays);
            Console.Title = String.Format("Guards of Aetheria - {0} at {1}", Player.Instance.Name, objects["Room Name"]);
            var variables = from o in arrays["Variables"] select VariableDictionary[o];
            Consoles.WordWrap(String.Format(Regex.Unescape((string)objects["Text to Display"]), variables));
            Console.SetCursorPosition(0, 5); Player.Instance.RoomId = arrays["Room IDs"][arrays["Option Text"].Select()].ToInt();
        }
    }
}