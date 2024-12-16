using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace HighlanderDB
{
    public class DatabaseManager
    {
        private readonly string _connectionString;

        public DatabaseManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Add a Highlander to the database and return the generated ID
        public void AddHighlander(int id, string name, int health, int power, int age, int x, int y, int prevX, int prevY, int kills, bool isAlive, bool isGood)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"
        INSERT INTO Highlanders 
        (ID, Name, Health, Power, Age, X, Y, PrevX, PrevY, Kills, IsAlive, IsGood) 
        VALUES 
        (@ID, @Name, @Health, @Power, @Age, @X, @Y, @PrevX, @PrevY, @Kills, @IsAlive, @IsGood);";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Health", health);
                    command.Parameters.AddWithValue("@Power", power);
                    command.Parameters.AddWithValue("@Age", age);
                    command.Parameters.AddWithValue("@X", x);
                    command.Parameters.AddWithValue("@Y", y);
                    command.Parameters.AddWithValue("@PrevX", prevX);
                    command.Parameters.AddWithValue("@PrevY", prevY);
                    command.Parameters.AddWithValue("@Kills", kills);
                    command.Parameters.AddWithValue("@IsAlive", isAlive);
                    command.Parameters.AddWithValue("@IsGood", isGood);

                    command.ExecuteNonQuery(); // Insert the Highlander into the database
                }
            }
        }


        // Retrieve a Highlander by ID
        public Dictionary<string, object> GetHighlanderById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Highlanders WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var highlanderData = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                highlanderData.Add(reader.GetName(i), reader.GetValue(i));
                            }
                            return highlanderData;
                        }
                    }
                }
            }
            return null; // Return null if no Highlander is found
        }

        // Update a Highlander in the database
        public bool UpdateHighlander(int id, int health, int power, int kills, bool isAlive)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                UPDATE Highlanders 
                SET Health = @Health, Power = @Power, Kills = @Kills, IsAlive = @IsAlive 
                WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Health", health);
                    command.Parameters.AddWithValue("@Power", power);
                    command.Parameters.AddWithValue("@Kills", kills);
                    command.Parameters.AddWithValue("@IsAlive", isAlive);
                    command.Parameters.AddWithValue("@ID", id);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // Delete a Highlander by ID
        public bool DeleteHighlander(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Highlanders WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public string GetAllAliveHighlanders()
        {
            StringBuilder result = new StringBuilder();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"
            SELECT ID, Name, Health, Power, Age, X, Y, Kills, IsGood
            FROM Highlanders
            WHERE IsAlive = 1;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Append Highlander info to the result string
                            result.AppendLine($"ID: {reader.GetInt32(reader.GetOrdinal("ID"))}, " +
                                              $"Name: {reader.GetString(reader.GetOrdinal("Name"))}, " +
                                              $"Health: {reader.GetInt32(reader.GetOrdinal("Health"))}, " +
                                              $"Power: {reader.GetInt32(reader.GetOrdinal("Power"))}, " +
                                              $"Age: {reader.GetInt32(reader.GetOrdinal("Age"))}, " +
                                              $"Position: ({reader.GetInt32(reader.GetOrdinal("X"))}, {reader.GetInt32(reader.GetOrdinal("Y"))}), " +
                                              $"Kills: {reader.GetInt32(reader.GetOrdinal("Kills"))}, " +
                                              $"IsGood: {reader.GetBoolean(reader.GetOrdinal("IsGood"))}");
                        }
                    }
                }
            }

            return result.ToString();
        }


        public void RecordKill(int killerId, int victimId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"
        INSERT INTO Kills (KillerID, VictimID, KillTime) 
        VALUES (@KillerID, @VictimID, GETDATE());";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@KillerID", killerId);
                    command.Parameters.AddWithValue("@VictimID", victimId);

                    command.ExecuteNonQuery(); // Insert kill record
                }
            }
        }
        //outputs kills
        public List<(int VictimID, string VictimName)> GetKillsByHighlanderWithDetails(int killerId)
        {
            var kills = new List<(int VictimID, string VictimName)>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"
            SELECT K.VictimID, H.Name AS VictimName
            FROM Kills K
            INNER JOIN Highlanders H ON K.VictimID = H.ID
            WHERE K.KillerID = @KillerID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@KillerID", killerId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int victimId = reader.GetInt32(reader.GetOrdinal("VictimID"));
                            string victimName = reader.GetString(reader.GetOrdinal("VictimName"));
                            kills.Add((victimId, victimName));
                        }
                    }
                }
            }

            return kills;
        }



        // List all Highlanders in the database
        public List<Dictionary<string, object>> GetAllHighlanders()
        {
            var highlanders = new List<Dictionary<string, object>>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Highlanders";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var highlanderData = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                highlanderData.Add(reader.GetName(i), reader.GetValue(i));
                            }
                            highlanders.Add(highlanderData);
                        }
                    }
                }
            }

            return highlanders;
        }
    }
}
