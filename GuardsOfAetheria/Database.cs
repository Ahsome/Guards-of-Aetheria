using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;

namespace GuardsOfAetheria
{
    static class Database
    {
        public static readonly OleDbConnection DatabaseConnection = new OleDbConnection(Properties.Settings.Default.databaseConnectionString);

        public static void GetData(string commandString, Dictionary<string, object> objects, Dictionary<string, List<string>> lists)
        {
            //TODO: accept all data e.g. ints, esp. for lists
            var command = new OleDbCommand(commandString, DatabaseConnection); //TODO: what is databaseconnection for?
            DatabaseConnection.Open();
            var reader = command.ExecuteReader();
            while (reader != null && reader.Read())
            {
                var objectKeys = objects.Keys.ToList();
                var listKeys = lists.Keys.ToList();
                for (var i = 0; i < objects.Count; i++) objects[objectKeys[i]] = reader[objectKeys[i]];
                for (var i = 0; i < lists.Count; i++) lists[listKeys[i]] = ((string)reader[listKeys[i]]).Split(',').ToList();
            }
            DatabaseConnection.Close();
        }
    }
}
