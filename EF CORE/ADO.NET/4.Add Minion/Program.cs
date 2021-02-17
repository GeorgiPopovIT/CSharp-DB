using Microsoft.Data.SqlClient;
using System;

namespace _4.Add_Minion
{
    class Program
    {
        static void Main(string[] args)
        {
            var minionInfo = Console.ReadLine().Split();
            var villainInfo = Console.ReadLine().Split(":");
            string town = minionInfo[3];
            string name = minionInfo[1];
            int age = int.Parse(minionInfo[2]);
            string villanName = villainInfo[1];

            var connectionString = "Server =.;Database = MinionsDB;Integrated Security = true";
            var connection = new SqlConnection(connectionString);
            connection.Open();
            using (connection)
            {
                string townId = TownId(connection, town);

                string villanId = VillanId(connection, villanName);

                string minionId = MinionId(connection, name, age, (int.Parse(townId)));

                var mappingQuery = @"INSERT INTO MinionsVillains (MinionId, VillainId)
                                VALUES (@villainId, @minionId)";
                var command = new SqlCommand(mappingQuery, connection);
                command.Parameters.AddRange(new[]
                {
                new SqlParameter("@minionId)",int.Parse(minionId)),
                new SqlParameter("@villainId",int.Parse(villanId))
            });
                command.ExecuteNonQuery();
                Console.WriteLine($"Successfully added {name} to be minion of {villanName}.");
            }
        }
        private static string TownId(SqlConnection connection, string town)
        {
            var townQueryId = @"SELECT Id FROM Towns WHERE Name = @townName";
            SqlCommand getTowncmd = new SqlCommand(townQueryId, connection);
            getTowncmd.Parameters.AddWithValue("@townName", town);
            var townId = getTowncmd.ExecuteScalar()?.ToString();
            using (getTowncmd)
            {
                if (townId == null)
                {
                    var insertTown = @"INSERT INTO Towns (Name) VALUES (@townName)";
                    using var insertCommand = new SqlCommand(insertTown, connection);
                    insertCommand.Parameters.AddWithValue("@townName", town);
                    insertCommand.ExecuteNonQuery();

                    townId = getTowncmd.ExecuteScalar()?.ToString();
                    Console.WriteLine($"Town {town} was added to the database.");
                }
            }
            return townId;
        }
        private static string VillanId(SqlConnection connection, string vallianName)
        {
            var villandQueryId = @"SELECT Id FROM Villains WHERE Name = @Name";
            SqlCommand getVillancmd = new SqlCommand(villandQueryId, connection);
            getVillancmd.Parameters.AddWithValue("@Name", vallianName);
            var villanId = getVillancmd.ExecuteScalar()?.ToString();
            using (getVillancmd)
            {
                if (villanId == null)
                {
                    var insertVallian = @"INSERT INTO Villains (Name, EvilnessFactorId)  VALUES (@villainName, 4)";
                    using var insertCommand = new SqlCommand(insertVallian, connection);
                    insertCommand.Parameters.AddWithValue("@villainName", vallianName);
                    insertCommand.ExecuteNonQuery();

                    villanId = getVillancmd.ExecuteScalar()?.ToString();
                    Console.WriteLine($"Villain {vallianName} was added to the database.");
                }
            }
            return villanId;
        }
        private static string MinionId(SqlConnection connection, string name, int age, int townId)
        {
            var minionIdQuery = @"SELECT Id FROM Minions WHERE Name = @Name";
            var getMinion = new SqlCommand(minionIdQuery, connection);
            getMinion.Parameters.AddWithValue("@Name", name);
            var minionId = getMinion.ExecuteScalar()?.ToString();
            if (minionId == null)
            {
                var insertMinion = @"INSERT INTO Minions (Name, Age, TownId)
                                    VALUES (@nam, @age, @townId)";
                var insertCommand = new SqlCommand(insertMinion, connection);
                insertCommand.Parameters.AddRange(new[]
                {
                    new SqlParameter("@nam",name),
                    new SqlParameter("@age",age),
                    new SqlParameter("@townId",townId)
                });
                insertCommand.ExecuteNonQuery();
                minionId = getMinion.ExecuteScalar()?.ToString();
            }
            return minionId;
        }
    }
}
