using Microsoft.Data.SqlClient;
using System;

namespace _8.Increase_Minion_Age
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split();

            var connectionString = "Server =.;Integrated Security = true;Database = MinionsDB";
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            for (int i = 0; i < input.Length; i++)
            {
                var query = @" UPDATE Minions
                        SET Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)), Age += 1
                        WHERE Id = @Id";
                var updateCommand = new SqlCommand(query, connection);
                updateCommand.Parameters.AddWithValue("@Id", int.Parse(input[i]));

                updateCommand.ExecuteNonQuery();
            }
            var printQuery = @"SELECT Name, Age FROM Minions";
            var printCommand = new SqlCommand(printQuery, connection);

            SqlDataReader reader = printCommand.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"{(string)reader["Name"]} {(int)reader["Age"]}");
            }
        }
    }
}
