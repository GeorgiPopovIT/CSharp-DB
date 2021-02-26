using Microsoft.Data.SqlClient;
using System;

namespace _9.Increase_Age_Stored_Procedure
{
    class Program
    {
        static void Main(string[] args)
        {
            int input = int.Parse(Console.ReadLine());
           string connectionString = "Server=.;Integrated Security = true;Database = MinionsDB";
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var query = @"EXEC usp_GetOlder @id";

            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", input);
            command.ExecuteNonQuery();

            var printQuery = @"SELECT Name, Age FROM Minions WHERE Id = @Id";
            var printCommand = new SqlCommand(printQuery, connection);
            printCommand.Parameters.AddWithValue("@Id", input);

            SqlDataReader reader = printCommand.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{(string)reader["Name"]} - {(int)reader["Age"]} years old");
            }
        }
    }
}
