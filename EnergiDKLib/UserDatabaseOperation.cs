using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace EnergiDKLib
{
    public class UserDatabaseOperation : Database, UserDatabaseOperationInterface
    {
        public bool addUser(string username, string password, bool free)
        {
            if (!userExists(username)) // can be used to check if user exsists
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
                return true;
            }
            return false;
        }

        public bool validateLogin( string username, string password )
        {
            using (SqlConnection connection = dbConnect())
            {
                SqlCommand cmd = new SqlCommand("SELECT username, password FROM users WHERE username = @username AND password = @password");             
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

        private bool userExists(string username)
        {
            using (SqlConnection connection = dbConnect())
            {
                SqlCommand cmd = new SqlCommand("SELECT username FROM users WHERE username = @username");
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@username", username);
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
