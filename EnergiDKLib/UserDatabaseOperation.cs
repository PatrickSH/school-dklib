using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace EnergiDKLib
{
    public class UserDatabaseOperation : Database
    {
        public string addUser(string username, string password, bool free)
        {
            using (SqlConnection connection = dbConnect())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO users (username, password, free) VALUES (@username, @password, @free)");
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@free", free);
                connection.Open();
                cmd.ExecuteNonQuery();
            }

            return "Created";
        }

        public bool validateLogin( string username, string password )
        {
            using (SqlConnection connection = dbConnect())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM users WHERE username = @username AND password = @password");             
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                connection.Open();

                var result = cmd.ExecuteScalar() as string;
                if (string.IsNullOrEmpty(result))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            
        }
    }
}
