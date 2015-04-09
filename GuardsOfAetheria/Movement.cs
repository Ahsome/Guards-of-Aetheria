using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace GuardsOfAetheria
{
    internal class Movement
    {
        private static OleDbDataAdapter databaseAdapter;
        private static DataRowCollection databaseRows;
        private static readonly databaseDataSet DatabaseResults = new databaseDataSet();

        public void ShowLocation()
        {
            Console.SetCursorPosition(0, 0); Console.Clear();
            var databaseConnection = new OleDbConnection(Properties.Settings.Default.databaseConnectionString);
            //TODO: hope that ms access does binary search
            var command = new OleDbCommand(String.Format("SELECT * FROM Rooms WHERE ID = {0}", Player.Instance.RoomId),
                databaseConnection);
            databaseAdapter = new OleDbDataAdapter(new OleDbCommand(String.Format("SELECT * FROM Rooms WHERE ID = {0}", Player.Instance.RoomId), databaseConnection));
            //TODO: tell users to get http://www.microsoft.com/en-us/download/details.aspx?id=13255
            databaseConnection.Open();
            var locations = new List<string>();
            var variables = new List<string>();
            var ids = new List<string>();
            var text = "";
            //TODO: console colour option, font colour, 
            databaseAdapter.Fill(DatabaseResults, "Rooms");
            
            using (var reader = command.ExecuteReader())
            {
                while (reader != null && reader.Read())
                {
                    locations = (reader["Option Text"].ToString().Split(',').ToList());
                    text = (reader["Text to Display"].ToString());
                    ids = (reader["Room IDs"].ToString().Split(',').ToList());
                    variables = (reader["Variables"].ToString().Split(',').ToList());
                }
            }
            databaseConnection.Close();
            var variableDictionary = new Dictionary<string, object> { { "Name", Player.Instance.Name } };
            var textVariable = new object[variables.Count];
            for (var i = 0; i < variables.Count; i++)
                if (variableDictionary.ContainsKey(variables[i]))
                    textVariable[i] = variableDictionary[variables[i]];
            Console.WriteLine(text.Replace(@"\n", Environment.NewLine), textVariable);
            Console.SetCursorPosition(0, 5);
            Player.Instance.RoomId = Utility.IntParseFast(ids[locations.SelectOption()]);
        }
    }
}