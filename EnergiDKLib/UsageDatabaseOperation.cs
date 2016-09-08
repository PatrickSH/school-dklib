using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace EnergiDKLib
{
    public class UsageDatabaseOperation : Database, UsageDatabaseOperationInterface
    {
        public string addUsage(string type, string usage, string date)
        {
            using (SqlConnection connection = dbConnect())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO energy_usage (type, km_driven, created_at) VALUES (@type, @driven, @created)");
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@driven", usage);
                cmd.Parameters.AddWithValue("@created", date);
                connection.Open();
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("INSERT INTO normal_energy_usage (type, km_a_liter) VALUES (@type, 12)");
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@type", type);
                
                cmd.ExecuteNonQuery();
            }

            return "Created";
        }

        private string getNormalUsage( string type)
        {
            using (SqlConnection connection = dbConnect())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM normal_energy_usage WHERE type=@type");
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@type", type);
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
            
                while (reader.Read())
                {
                    return reader["km_a_liter"].ToString();
                }

                return "1";
            }
        }
        public List<string[]> getAllUsage( string dateFrom = null, string dateTo = null )
        {
            using (SqlConnection connection = dbConnect())
            {
                SqlCommand cmd = new SqlCommand();

                if ( string.IsNullOrEmpty(dateFrom) || string.IsNullOrEmpty(dateTo) )
                {
                    cmd = new SqlCommand("SELECT * FROM energy_usage");
                }
                else
                {
                    cmd = new SqlCommand("SELECT * FROM energy_usage WHERE created_at BETWEEN @dateFrom AND @dateTo");
                    cmd.Parameters.AddWithValue("@dateFrom", dateFrom);
                    cmd.Parameters.AddWithValue("@dateTo", dateTo);
                }
                
                cmd.Connection = connection;
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                List<string[]> datas = new List<string[]>();

                while (reader.Read())
                {
                    var gasCalc = Convert.ToInt32(Convert.ToInt32(reader["km_driven"].ToString()) / Convert.ToInt32(this.getNormalUsage(reader["type"].ToString())));
                    string[] fields = new string[4];
                    fields[0] = reader["type"].ToString();
                    fields[1] = reader["km_driven"].ToString();
                    fields[2] = reader["created_at"].ToString();
                    fields[3] = gasCalc.ToString();
                    
                    datas.Add(fields);
                }

                return datas;
            }

        }

        public List<string[]> getPeriodUsage(string dateFrom, string dateTo)
        {
            return this.getAllUsage(dateFrom, dateTo);
        }

    }
}
