using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace EnergiDKLib
{

    public class Database : DatabaseInterface
    {
        private static string _host = "PATRICK"+"\"SCHOOLSQL";
        private static string _database = "school2";
        private static string _username = "sa";
        private static string _password = "Online0901";

        public SqlConnection dbConnect()
        {
            string connetionString = null;
            connetionString = @"Data Source = "+
                _host+
                "; Initial Catalog = "+
                _database+
                "; Persist Security Info = True; User ID = "+
                _username+
                "; Password = "+
                _password+
                "; Pooling = False";
            
            return new SqlConnection(connetionString);
        }
    }
}
