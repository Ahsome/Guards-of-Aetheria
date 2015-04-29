using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;

namespace GuardsOfAetheria
{
    static class Database
    {
        public static readonly OleDbConnection DatabaseConnection = new OleDbConnection(Properties.Settings.Default.databaseConnectionString);

        public static void GetData(string commandString, Dictionary<string, object> objects, Dictionary<string, string[]> arrays)
        {
            //TODO: accept all data e.g. ints, esp. for lists
            var command = new OleDbCommand(commandString, DatabaseConnection); //TODO: what is databaseconnection for?
            DatabaseConnection.Open();
            var reader = command.ExecuteReader();
            while (reader != null && reader.Read())
            {
                var objectKeys = objects.Keys.ToArray();
                var listKeys = arrays.Keys.ToArray();
                for (var i = 0; i < objects.Count; i++) objects[objectKeys[i]] = reader[objectKeys[i]];
                for (var i = 0; i < arrays.Count; i++) arrays[listKeys[i]] = ((string)reader[listKeys[i]]).Split(',');
            }
            DatabaseConnection.Close();
        }
    }
}
