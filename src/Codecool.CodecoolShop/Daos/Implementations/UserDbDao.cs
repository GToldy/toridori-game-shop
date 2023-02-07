using Codecool.CodecoolShop.Models;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;
using System;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class UserDbDao : IUserDbDao
    {
        private readonly string _connectionString;
        private static UserDbDao instance = null;

        private UserDbDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static UserDbDao GetInstance(string connectionString)
        {
            if (instance is null)
            {
                instance = new UserDbDao(connectionString);
            }
            return instance;
        }

        public void Add(User user)
        {
            var name = user.Name;
            var email = user.Email;
            var password = user.Password;
            const string query = @"INSERT INTO Users (name, email, password) VALUES (@name, @email, @password) SELECT SCOPE_IDENTITY();";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(query, connection);
                    if (connection.State == System.Data.ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);
                    user.Id = Convert.ToInt32(cmd.ExecuteScalar());
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }
    }
}
