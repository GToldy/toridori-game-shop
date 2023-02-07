using Codecool.CodecoolShop.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class SupplierDbDao : ISupplierDbDao
    {
        private readonly string _connectionString;
        private static SupplierDbDao instance = null;

        private SupplierDbDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static SupplierDbDao GetInstance(string connectionString)
        {
            if (instance is null)
            {
                instance = new SupplierDbDao(connectionString);
            }
            return instance;
        }

        public void Add(Supplier item)
        {
            const string query = @"INSERT INTO Supplier (name) VALUES (@name) SELECT SCOPE_IDENTITY();";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(query, connection);
                    if (connection.State == System.Data.ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    cmd.Parameters.AddWithValue("@name", item.Name);

                    item.Id = Convert.ToInt32(cmd.ExecuteScalar());
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            };
        }

        public void Update(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Supplier Get(int id)
        {
            const string query = @"SELECT * FROM Supplier WHERE id = @id";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(query, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    cmd.Parameters.AddWithValue("@id", id);

                    var reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        return null;
                    }
                    var name = reader.GetString("name");
                    var description = reader.GetString("description");
                    var supplier = new Supplier() { Id = id, Name = name, Description = description };
                    connection.Close();
                    return supplier;
                }
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

        public IEnumerable<Supplier> GetAll()
        {
            const string query = @"SELECT * FROM Supplier";
            try
            {
                var results = new List<Supplier>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(query, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    var reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                        return results;

                    while (reader.Read())
                    {
                        var name = reader["name"] as string;
                        var description = reader["description"] as string;
                        var supplier = new Supplier { Id = (int)reader["id"], Name = name, Description = description };
                        results.Add(supplier);
                    }
                    connection.Close();
                }

                return results;
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }
    }
}
