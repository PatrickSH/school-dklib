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
        private static string _database = "school";
        private static string _username = "sa";
        private static string _password = "Online0901";

        public static SqlConnection dbConnect()
        {
            string connetionString = null;
            connetionString = @"Data Source = PATRICK\SCHOOLSQL; Initial Catalog = school2; Persist Security Info = True; User ID = sa; Password = Online0901; Pooling = False";
            
            return new SqlConnection(connetionString);
        }
    }
}
