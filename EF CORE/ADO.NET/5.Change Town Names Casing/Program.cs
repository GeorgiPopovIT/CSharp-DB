using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace _5.Change_Town_Names_Casing
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Server =.;Integrated Security = true;Database = MinionsDB";
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            string countryName = Console.ReadLine();

            var updateQuery = @"UPDATE Towns
                                SET Name = UPPER(Name)
                                WHERE CountryCode = (SELECT c.Id FROM Countries AS c 
                                    WHERE c.Name = @countryName)";
            var updateCommand = new SqlCommand(updateQuery, connection);
            updateCommand.Parameters.AddWithValue("@countryName", countryName);

            var affectedRows = updateCommand.ExecuteNonQuery();
            if (affectedRows == 0)
            {
                Console.WriteLine("No town names were affected.");
                return;
            }
            Console.WriteLine($"{affectedRows} town names were affected.");

            var getTownsQuery = @" SELECT t.Name FROM Towns as t
                                    JOIN Countries AS c ON c.Id = t.CountryCode
                                    WHERE c.Name = @countryName";
            var getTownsCommand = new SqlCommand(getTownsQuery, connection);
            getTownsCommand.Parameters.AddWithValue("@countryName", countryName);

            using SqlDataReader townsReader = getTownsCommand.ExecuteReader();

            List<string> cities = new List<string>();
            while (townsReader.Read())
            {
                cities.Add((string)townsReader["Name"]);
            }
            Console.WriteLine($"[{string.Join(", ",cities)}]");
        }
    }
}
