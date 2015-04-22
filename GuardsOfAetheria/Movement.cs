using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text.RegularExpressions;

namespace GuardsOfAetheria
{
    internal class Movement
    {
        readonly OleDbConnection databaseConnection = new OleDbConnection(Properties.Settings.Default.databaseConnectionString);
        public void ShowLocation()
        {
            Console.SetCursorPosition(0, 0); Console.Clear();
            //TODO: hope that ms access does binary search, make it faster - async or something, tell users to get http://www.microsoft.com/en-us/download/details.aspx?id=13255 (or http://www.microsoft.com/download/en/confirmation.aspx?id=23734)?
            var locations = new List<string>();
            var variables = new List<string>();
            var ids = new List<string>();
            var text = "";
            var name = "";
            databaseConnection.Open();
            var reader = new OleDbCommand(String.Format("SELECT * FROM Rooms WHERE ID = {0}", Player.Instance.RoomId), databaseConnection).ExecuteReader();
            while (reader != null && reader.Read())
            {
                locations = (reader["Option Text"].ToString().Split(',').ToList());
                text = (reader["Text to Display"].ToString());
                ids = (reader["Room IDs"].ToString().Split(',').ToList());
                variables = (reader["Variables"].ToString().Split(',').ToList());
                name = (reader["Room Name"].ToString());
            }
            Console.Title = String.Format("Guards of Aetheria - {0} at {1}", Player.Instance.Name, name);
            databaseConnection.Close();
            var variableDictionary = new Dictionary<string, object>
            {
                { "Name", Player.Instance.Name }
            };
            var textVariable = new object[variables.Count];
            for (var i = 0; i < variables.Count; i++) textVariable[i] = variableDictionary[variables[i]];
            Console.WriteLine(Regex.Unescape(text), textVariable);
            Console.SetCursorPosition(0, 5);
            Player.Instance.RoomId = ids[locations.Select()].ToInt();
        }
    }
}