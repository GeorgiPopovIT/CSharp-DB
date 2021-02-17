using System;
using Microsoft.Data.SqlClient;

namespace _3.Minion_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            int id = int.Parse(Console.ReadLine());
            string connectionString = "Server =.;Database = MinionsDB;Integrated Security = true";
            var connection = new SqlConnection(connectionString);
            connection.Open();
            using (connection)
            {
                var query1 = @"SELECT Name FROM Villains WHERE Id = @Id";
                var command = new SqlCommand(query1, connection);
                command.Parameters.AddWithValue("@Id", id);

                var resultId = (string)command.ExecuteScalar();
                if (resultId != null)
                {
                    Console.WriteLine($"Villain: {resultId}");
                }
                else
                {
                    Console.WriteLine($"No villain with ID {id} exists in the database.");
                }

                var query2 = @"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
                                         m.Name, 
                                         m.Age
                                    FROM MinionsVillains AS mv
                                    JOIN Minions As m ON mv.MinionId = m.Id
                                   WHERE mv.VillainId = @Id
                                ORDER BY m.Name";
                command = new SqlCommand(query2, connection);
                command.Parameters.AddWithValue("@Id", id);
                var reader = command.ExecuteReader();
                using (reader)
                {
                    if (reader.FieldCount == 0)
                    {
                        Console.WriteLine("(no minions)");
                        return;
                    }
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["RowNum"]}. {reader["Name"]} {reader["Age"]}");
                    }
                }
            }
        }
    }
}
