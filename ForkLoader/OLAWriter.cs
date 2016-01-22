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

        private readonly string m_connectionString;
        private readonly int m_eventClassId;
        public OlaWriter(string connectionString, int eventClassId)
        {
            m_connectionString = connectionString;
            m_eventClassId = eventClassId;
        }

        public void WriteForkKeys(List<ForkKey> forkKeys)
        {
            List<RaceClass> raceClasses = ReadRaceClasses();
            MySql.Data.MySqlClient.MySqlConnection conn = null;
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(m_connectionString);
                conn.Open();
                IDbCommand cmd = conn.CreateCommand();
                foreach (ForkKey forkKey in forkKeys)
                {
                    for (int leg = 1; leg <= forkKey.Forks.Count; leg++)
                    {
                        int raceClassId =
                            raceClasses.Single(r => r.StartNumberBase == forkKey.TeamNumber && r.RelayLeg == leg)
                                .RaceClassId;
                        cmd.CommandText = "update results set individualCourseId = " + CourseNameToId(forkKey.Forks[leg-1]) +
                            ", forkedCourseId = " + CourseNameToId(forkKey.Forks[leg - 1]) + " where raceClassId = " + raceClassId;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            } 
            string myConnectionString;

            myConnectionString = "server=127.0.0.1;uid=root;" +
                "pwd=12345;database=dmstafett;";          
        }

        public List<ForkKey> ReadForkKeys()
        {
            throw new NotImplementedException();
        }

        private List<RaceClass> ReadRaceClasses()
        {
            MySql.Data.MySqlClient.MySqlConnection conn = null;
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(m_connectionString);
                conn.Open();
                IDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from raceclasses where eventClassId = " + m_eventClassId;
                IDataReader reader = cmd.ExecuteReader();
                var raceClasses = new List<RaceClass>();
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
                return raceClasses;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return null;
        }

        private int? CourseNameToId(string name)
        {
            MySql.Data.MySqlClient.MySqlConnection conn = null;
            int? courseId = null;
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(m_connectionString);
                conn.Open();
                IDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from courses where name = " + name;
                IDataReader reader = cmd.ExecuteReader();
                int nFoundCourses = 0;
                while (reader.Read())
                {
                    try
                    {
                        courseId = Convert.ToInt32(reader["courseId"]);
                        nFoundCourses++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to convert course id " + reader["courseId"] + " for course with name " + name);
                    }
                }
                if (nFoundCourses != 1)
                {
                    Console.WriteLine("Found " + nFoundCourses + " with name " + name + " expected 1.");
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return courseId;
        }
    }
}
