using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace _7.Print_All_Minion_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Server=.;Integrated Security = true;Database = MinionsDB";
         using var connection = new SqlConnection(connectionString);
            connection.Open();

            var query = @"SELECT Name FROM Villains";
            var getNames = new SqlCommand(query, connection);

            SqlDataReader reader = getNames.ExecuteReader();

            List<string> names = new List<string>();
            while (reader.Read())
            {
                names.Add((string)reader["Name"]);
            }

            for (int i = 0; i < names.Count; i++)
            {
                Console.WriteLine(names[i]);
                if (i == names.Count / 2)
                {
                    return;
                }
                Console.WriteLine(names[names.Count - i - 1]);
            }
        }
    }
}
