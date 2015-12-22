using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForkLoader
{
    public class OlaWriter
    {
        // *** OLA Table Results ***
        // bibNumber - 
        // raceStartNumber - 
        // individualCourseId
        // forkedCourseId
        // raceClassId

        // Update individualCourseId, forkedCourseId for the entry in the results table that has the raceClassId given by the 
        // raceClasses table keyed by startNumberBase and relayLeg

        public OlaWriter(string connectionString)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=127.0.0.1;uid=root;" +
                "pwd=12345;database=dmstafett;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();
                IDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from raceclasses";
                IDataReader reader = cmd.ExecuteReader();
                List<RaceClass> raceClasses = new List<RaceClass>();
                while (reader.Read())
                {
                    if (reader["eventClassId"] != null && reader["eventClassId"] != DBNull.Value &&
                        reader["raceClassId"] != null && reader["raceClassId"] != DBNull.Value &&
                        reader["startNumberBase"] != null && reader["startNumberBase"] != DBNull.Value &&
                        reader["relayLeg"] != null && reader["relayLeg"] != DBNull.Value)
                    {
                        raceClasses.Add(new RaceClass(Convert.ToInt32(reader["eventClassId"]),
                            Convert.ToInt32(reader["raceClassId"]),
                            Convert.ToInt32(reader["startNumberBase"]), Convert.ToInt32(reader["relayLeg"])));
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
