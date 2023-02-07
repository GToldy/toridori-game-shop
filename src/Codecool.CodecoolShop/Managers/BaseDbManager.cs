using Codecool.CodecoolShop.Daos;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System;

namespace Codecool.CodecoolShop.Managers
{
    public class BaseDbManager
    {
        public string connectionString => ConfigurationManager.AppSettings["connectionString"];

        public BaseDbManager()
        {
            EnsureConnectionSuccessful();
        }

        public void EnsureConnectionSuccessful() 
        {
            if (!TestConnection())
            {
                Console.WriteLine(Console.Error);
            }
            Console.WriteLine("Connection estabilished");
        }
        
        public bool TestConnection()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (System.Exception)
                {

                    return false;
                }
            }
        }
    }
}
