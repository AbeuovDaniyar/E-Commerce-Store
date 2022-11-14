using CapstoneProject.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Data
{
    public class ProductDAO
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=aspnet-CapstoneProject-96D9AAD4-C520-424C-AF81-4A8C2D540CE1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /// <summary>
        /// Retrieves all products from the database table
        /// </summary>
        /// <returns>List<Product></returns>
        public List<Product> getAllProducts() 
        {
            List<Product> products = new List<Product>();
            string sqlStatement = "SELECT * FROM dbo.Product";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read()) 
                    {
                        products.Add(new Product((int)reader[0], (string)reader[1], (string)reader[2], (string)reader[3],
                            (string)reader[4], (float)(double)reader[5], (int)reader[6], (string)reader[8]));
                    }
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }

            return products;
        }

        /// <summary>
        /// Retrieves all products from the database table by productId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>Product</returns>
        public Product GetProductById(int productId)
        {
            Product foundProduct = null;

            string sqlStatement = "SELECT * FROM dbo.Product WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@id", productId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        foundProduct = new Product((int)reader[0], (string)reader[1], (string)reader[2], (string)reader[3],
                            (string)reader[4], (float)(double)reader[5], (int)reader[6], (string)reader[8]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return foundProduct;
        }
        /// <summary>
        /// Retrieves all supplements from the database table by productId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>Supplements</returns>
        public Supplements GetSupplementByProductId(int productId)
        {
            Supplements foundProduct = null;

            string sqlStatement = "SELECT * FROM dbo.Supplements WHERE ProductId = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@id", productId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        foundProduct = new Supplements((int)reader[0], (string)reader[1], (string)reader[2], (string)reader[3], (int)reader[4],
                            (float)(double)reader[5], (string)reader[6], (string)reader[7], (string)reader[8], (string)reader[9], (string)reader[10], 
                            (string)reader[11], (int)reader[12], (string)reader[13]);                       
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return foundProduct;
        }
    }
}
