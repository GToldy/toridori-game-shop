using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ProductDbDao : IProductDbDao
    {
        private readonly string _connectionString;
        private static ProductDbDao instance = null;

        private ProductDbDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static ProductDbDao GetInstance(string connectionString)
        {
            if (instance is null)
            {
                instance = new ProductDbDao(connectionString);
            }
            return instance;
        }

        public void Add(Product product)
        {
            const string query = @"INSERT INTO Product (name) VALUES (@name) SELECT SCOPE_IDENTITY();";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(query, connection);
                    connection.Open();
                    cmd.Parameters.AddWithValue("@name", product.Name);

                    product.Id = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Product Get(int id)
        {
            const string query = @"SELECT Product.id, Product.name, Product.description, Product.players, Product.currency, Product.default_price, ProductCategory.name AS category, Supplier.name AS supplier, Product.image FROM Product
                                    LEFT JOIN ProductCategory ON Product.product_category_id = ProductCategory.id
                                    LEFT JOIN Supplier ON Product.supplier_id = Supplier.id
                                    WHERE Product.id = @id";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(query, connection);
                    connection.Open();
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return null;
                        }
                        return GetProductFromReader(reader);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

        public IEnumerable<Product> GetAll()
        {
            const string query = @"SELECT Product.id, Product.name, Product.description, Product.players, Product.currency, Product.default_price, ProductCategory.name AS category, Supplier.name AS supplier, Product.image FROM Product
                                    LEFT JOIN ProductCategory ON Product.product_category_id = ProductCategory.id
                                    LEFT JOIN Supplier ON Product.supplier_id = Supplier.id";
            try
            {
                var results = new List<Product>();
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
                        results.Add(GetProductFromReader(reader));
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

        public IEnumerable<Product> GetBySupplier(Supplier supplier)
        {
            var supplierId = supplier.Id;
            const string query = @"SELECT Product.id, Product.name, Product.description, Product.players, Product.currency, Product.default_price, ProductCategory.name AS category, Supplier.name AS supplier, Product.image, Product.supplier_id FROM Product
                                    LEFT JOIN ProductCategory ON Product.product_category_id = ProductCategory.id
                                    LEFT JOIN Supplier ON Product.supplier_id = Supplier.id
                                    WHERE supplier_id = @supplierId";
            try
            {
                var results = new List<Product>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(query, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    cmd.Parameters.AddWithValue("@supplierId", supplier.Id);
                    var reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                        return results;

                    while (reader.Read())
                    {
                        results.Add(GetProductFromReader(reader));
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

        public IEnumerable<Product> GetByProductCategory(ProductCategory productCategory)
        {
            const string query = @"SELECT Product.id, Product.name, Product.description, Product.players, Product.currency, Product.default_price, ProductCategory.name AS category, Supplier.name AS supplier, Product.image FROM Product
                                    LEFT JOIN ProductCategory ON Product.product_category_id = ProductCategory.id
                                    LEFT JOIN Supplier ON Product.supplier_id = Supplier.id
                                    WHERE Product.product_category_id = @categoryId";
            try
            {
                var results = new List<Product>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(query, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    cmd.Parameters.AddWithValue("@categoryId", productCategory.Id);
                    var reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                        return results;

                    while (reader.Read())
                    {
                        results.Add(GetProductFromReader(reader));
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

        public IEnumerable<Product> GetBy(Supplier supplier, ProductCategory productCategory)
        {
            const string query = @"SELECT Product.id, Product.name, Product.description, Product.players, Product.currency, Product.default_price, ProductCategory.name AS category, Supplier.name AS supplier, Product.image FROM Product
                                    LEFT JOIN ProductCategory ON Product.product_category_id = ProductCategory.id
                                    LEFT JOIN Supplier ON Product.supplier_id = Supplier.id
                                    WHERE Product.product_category_id = @categoryId AND Product.supplier_id = @supplierId";
            try
            {
                var results = new List<Product>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(query, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    cmd.Parameters.AddWithValue("@categoryId", productCategory.Id);
                    cmd.Parameters.AddWithValue("@supplierId", supplier.Id);
                    var reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                        return results;

                    while (reader.Read())
                    {
                        results.Add(GetProductFromReader(reader));
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

        public void Update(int id)
        {
            throw new System.NotImplementedException();
        }

        private Product GetProductFromReader(SqlDataReader reader)
        {
            return new Product() { 
                Id = (int)reader["id"], 
                Name = reader.GetString("name"), 
                Description = reader.GetString("description"), 
                ProductCategory = new ProductCategory() { Name = reader.GetString("category") }, 
                Supplier = new Supplier() { Name = reader.GetString("supplier") }, 
                DefaultPrice = reader.GetDecimal("default_price"), 
                Players = reader.GetString("players"), 
                Currency = reader.GetString("currency"), 
                Image = reader.GetString("image")
            };
        }
    }
}
