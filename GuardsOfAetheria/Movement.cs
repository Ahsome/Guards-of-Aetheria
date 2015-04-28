using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using Improved;

namespace GuardsOfAetheria
{
    internal static class Movement
    {
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
            Database.GetData(new OleDbCommand(String.Format("SELECT * FROM Rooms WHERE ID = {0}", Player.Instance.RoomId), Database.DatabaseConnection), objects, lists);
            Console.Title = String.Format("Guards of Aetheria - {0} at {1}", Player.Instance.Name, objects["Room Name"]);
            var variableDictionary = new Dictionary<string, object>
            {
                { "Name", Player.Instance.Name }
            };
            var textVariable = new object[lists["Variables"].Count];
            for (var i = 0; i < lists["Variables"].Count; i++) textVariable[i] = variableDictionary[lists["Variables"][i]];
            int lines;
            Consoles.WordWrap(String.Format(Regex.Unescape((string)objects["Text to Display"]), textVariable), out lines);
            Console.SetCursorPosition(0, 5);
            Player.Instance.RoomId = lists["Room IDs"][lists["Option Text"].Select()].ToInt();
        }
    }
}