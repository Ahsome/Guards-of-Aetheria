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
            var lists = new Dictionary<string, List<string>>
            {
                {"Option Text", new List<string>()},
                {"Room IDs", new List<string>()},
                {"Variables", new List<string>()}
            };
            var objects = new Dictionary<string, object>
            {
                {"Text to Display", ""},
                {"Room Name", ""}
            };
            Database.GetData(String.Format("SELECT * FROM Rooms WHERE ID = {0}", Player.Instance.RoomId), objects, lists);
            Console.Title = String.Format("Guards of Aetheria - {0} at {1}", Player.Instance.Name, objects["Room Name"]);
            var variables = from o in lists["Variables"] select VariableDictionary[o];
            Consoles.WordWrap(String.Format(Regex.Unescape((string)objects["Text to Display"]), variables));
            Console.SetCursorPosition(0, 5); Player.Instance.RoomId = lists["Room IDs"][lists["Option Text"].Select()].ToInt();
        }
    }
}