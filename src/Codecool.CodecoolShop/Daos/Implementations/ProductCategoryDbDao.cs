using Codecool.CodecoolShop.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ProductCategoryDbDao : IProductCategoryDbDao
    {
        private readonly string _connectionString;
        private static ProductCategoryDbDao instance = null;

        private ProductCategoryDbDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static ProductCategoryDbDao GetInstance(string connectionString)
        {
            if (instance is null)
            {
                instance = new ProductCategoryDbDao(connectionString);
            }
            return instance;
        }

        public void Add(ProductCategory item)
        {
            const string query = @"INSERT INTO ProductCategory (name) VALUES (@name) SELECT SCOPE_IDENTITY();";
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

        public ProductCategory Get(int id)
        {
            const string query = @"SELECT * FROM Product WHERE @id = id";
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

                    var productCategory = new ProductCategory() { Id = id, Name = name, Description = description };
                    connection.Close();
                    return productCategory;
                }
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            const string query = @"SELECT * FROM ProductCategory";
            try
            {
                var results = new List<ProductCategory>();
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
                        var productCategory = new ProductCategory { Id = (int)reader["id"], Name = name, Description = description };
                        results.Add(productCategory);
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
