using Microsoft.Data.SqlClient;
using System;

namespace _2.Vallain_Name
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var connection = new SqlConnection("Server =.;Integrated Security = true;Database = MinionsDB"))
            {
                connection.Open();
                var command = new SqlCommand(@"
            SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount  
               FROM Villains AS v 
                JOIN MinionsVillains AS mv ON v.Id = mv.VillainId 
                GROUP BY v.Id, v.Name 
                HAVING COUNT(mv.VillainId) > 3 
                ORDER BY COUNT(mv.VillainId)", connection);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Name"]} -> {reader["MinionsCount"]}");
                }

            }
        }
    }
}
