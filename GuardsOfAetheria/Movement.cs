using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;

namespace GuardsOfAetheria
{
    internal class Movement
    {
        public void ShowLocation()
        {
            var databaseConnection = new OleDbConnection(Properties.Settings.Default.databaseConnectionString);
            Console.SetCursorPosition(0, 0); Console.Clear();
            //TODO: hope that ms access does binary search, make it faster - async or something, tell users to get http://www.microsoft.com/en-us/download/details.aspx?id=13255 (or http://www.microsoft.com/download/en/confirmation.aspx?id=23734)?
            var locations = new List<string>();
            var variables = new List<string>();
            var ids = new List<string>();
            var text = "";
            //TODO: console colour option, font colour, 
            databaseConnection.Open();
            var reader = new OleDbCommand(String.Format("SELECT * FROM Rooms WHERE ID = {0}", Players.You.RoomId), databaseConnection).ExecuteReader();
            while (reader != null && reader.Read())
            {
                locations = (reader["Option Text"].ToString().Split(',').ToList());
                text = (reader["Text to Display"].ToString());
                ids = (reader["Room IDs"].ToString().Split(',').ToList());
                variables = (reader["Variables"].ToString().Split(',').ToList());
            }
            databaseConnection.Close();
            var variableDictionary = new Dictionary<string, object>
            {
                { "Name", Players.You.Name }
            };
            var textVariable = new object[variables.Count];
            for (var i = 0; i < variables.Count; i++)
                if (variableDictionary.ContainsKey(variables[i]))
                    textVariable[i] = variableDictionary[variables[i]];
            Console.WriteLine(text.Replace(@"\n", Environment.NewLine), textVariable);
            Console.SetCursorPosition(0, 5);
            Players.You.RoomId = Utility.IntParseFast(ids[locations.SelectOption()]);
        }
    }
}