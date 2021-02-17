using Microsoft.Data.SqlClient;
using System;

namespace _6.Remove_Villain
{
    class Program
    {
        static void Main(string[] args)
        {
            int villainId = int.Parse(Console.ReadLine());
            string connectionString = "Server =.;Integrated Security = true;Database = MinionsDB";
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            var getVillain = @"SELECT Name FROM Villains 
                            WHERE Id = @villainId";
            var getVillainCommand = new SqlCommand(getVillain, connection);
            getVillainCommand.Parameters.AddWithValue("@villainId", villainId);

            var villainName = getVillainCommand.ExecuteScalar()?.ToString();
            if (villainName == null)
            {
                Console.WriteLine("No such villain was found.");
            }
            else
            {
                var getMinionVallians = @"DELETE FROM MinionsVillains 
                                        WHERE VillainId = @villainId";

                var getMVcommand = new SqlCommand(getMinionVallians, connection);
                getMVcommand.Parameters.AddWithValue("@villainId", villainId);

                var affectedRows = getMVcommand.ExecuteNonQuery();

                var getVallians = @"DELETE FROM Villains
                                    WHERE Id = @villainId";

                var getValliansCommand = new SqlCommand(getVallians, connection);
                getValliansCommand.Parameters.AddWithValue("@villainId", villainId);

                getValliansCommand.ExecuteNonQuery();

                Console.WriteLine($"{villainName} was deleted.");
                Console.WriteLine($"{affectedRows} minions were released.");
            }
        }
    }
}
