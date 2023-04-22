/* This is a C# class called `ProductDAO` that contains methods for retrieving data from a SQL Server
database. It uses the `CapstoneProject.Models` namespace to access the `Product` and `Supplements`
classes, and the `Microsoft.Data.SqlClient` namespace to connect to the database using a connection
string. The `getAllProducts` method retrieves all products from the `Product` table, while the
`GetProductById` method retrieves a single product by its ID. The `GetSupplementByProductId` method
retrieves a single supplement by its associated product ID. All methods return either a `Product` or
`Supplements` object, or a list of `Product` objects. */
using CapstoneProject.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Data
{
    /* The ProductDAO class retrieves products and supplements from a database table using SQL
    statements. */
    public class ProductDAO
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=aspnet-CapstoneProject-96D9AAD4-C520-424C-AF81-4A8C2D540CE1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        
        /// This C# function retrieves all products from a database and returns them as a list of
        /// Product objects.
        /// 
        /// @return A list of all products from the database table "Product".
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

        
        /// This C# function retrieves a product from a database by its ID.
        /// 
        /// @param productId an integer representing the ID of the product to be retrieved from the
        /// database.
        /// 
        /// @return The method is returning a single instance of the `Product` class that matches the
        /// given `productId`.
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
        

        /// This function retrieves a supplement product from a database based on its product ID.
        /// 
        /// @param productId an integer representing the ID of the product for which we want to retrieve
        /// the supplement information.
        /// 
        /// @return The method is returning a single instance of the Supplements class that matches the
        /// provided productId.
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
