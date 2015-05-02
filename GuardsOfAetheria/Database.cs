using System.Collections.Generic;
using System.Data.OleDb;

namespace GuardsOfAetheria
{
    static class Database
    {
        public static OleDbConnection Connection = new OleDbConnection(Properties.Settings.Default.databaseConnectionString);
        public static void GetData(string commandString, Dictionary<string, object> objects, Dictionary<string, string[]> arrays)
        {
            //TODO: object[] or something
            Connection.Open();
            var objectKeys = objects.Keys;
            var arrayKeys = arrays.Keys;
            using (var reader = new OleDbCommand(commandString, Connection).ExecuteReader())
            {
                foreach (var k in objectKeys) { objects[k] = reader?[k]; }
                foreach (var k in arrayKeys) { arrays[k] = ((string)reader?[k])?.Split(','); }
            }
            Connection.Close();
        }
    }
}
