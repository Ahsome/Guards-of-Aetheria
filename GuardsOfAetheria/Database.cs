using System;
using System.Collections.Generic;
using System.Data.OleDb;
using static Improved.Consoles.Lists;

namespace GuardsOfAetheria {
    static class Database {
        public static OleDbConnection Connection = new OleDbConnection(Properties.Settings.Default.databaseConnectionString);
        public static Tuple<Dictionary<string,object>,Dictionary<string,string[]>> OleDbRead(this string query,string[] objNames,string[] arrNames) {
            //TODO: object[] or something, multiple rows, move to improved?, open + close -> 1 line, shorter tuple syntax
            Connection.Open();
            var reader = new OleDbCommand(query,Connection).ExecuteReader();
            reader?.Read();
            var tuple = Tuple(
                objNames.ToDict(k => reader?[k]),
                arrNames.ToDict(k => ((string)reader?[k])?.Split(',')));
            Connection.Close();
            return tuple;
        }
    }
}
