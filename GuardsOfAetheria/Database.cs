namespace GuardsOfAetheria{
    using System;
    using System.Collections.Generic;
    using System.Data.OleDb;
    using Properties;
    using Improved;
    using static Improved.Lists;
    internal static class Database{
        public static OleDbConnection Connection=new OleDbConnection(Settings.Default.databaseConnectionString);
        public static Tuple<Dictionary<string,object>,Dictionary<string,string[]>> OleDbRead(this string query,
            string[] objNames,
            string[] arrNames){//TODO: support multiple rows, move to improved?
            Connection.Open();
            var reader=new OleDbCommand(query,Connection).ExecuteReader();
            reader?.Read();
            var tuple=Tuple(objNames.ToDict(k=>reader?[k]),arrNames.ToDict(k=>((string)reader?[k])?.Split(',')));
            Connection.Close();
            return tuple;
        }
    }
}
