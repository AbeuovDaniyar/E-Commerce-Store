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
                        //foundProduct = new Supplements(0, "", "", "", "", 0.00F, 0, (int)reader[0], (string)reader[1], (string)reader[2], (string)reader[3], (int)reader[4],
                        //(float)reader[5], (string)reader[6], (string)reader[7], (string)reader[8], (string)reader[9], (string)reader[10], (string)reader[11], (int)reader[12], (string)reader[13]);
                        /*foundProduct = new Supplements
                        {
                            id = 0,
                            productName = "",
                            productManufacturer = "",
                            productDescription = "",
                            productImagePath = "",
                            productPrice = 0.00F,
                            productStock = 0,
                            supplementId = (int)reader[0],
                            productExpirationDate = (string)reader[1],
                            productCode = (string)reader[2],
                            productUPCCode = (string)reader[3],
                            productPckgQuantity = (int)reader[4],
                            productShippingWeight = (float)reader[5],
                            productDimensions = (string)reader[6],
                            productKeyWords = (string)reader[7],
                            productSuggestedUse = (string)reader[8],
                            productIngridients = (string)reader[9],
                            productWarnings = (string)reader[10],
                            productDisclaimer = (string)reader[11],
                            productId = (int)reader[12],
                            productManufacturerLink = (string)reader[13],                            
                        };
                        foundProduct.supplementId = (int)reader[0];
                        foundProduct.productExpirationDate = (string)reader[1];
                        foundProduct.productCode = (string)reader[2];
                        foundProduct.productUPCCode = (string)reader[3];
                        foundProduct.productPckgQuantity = (int)reader[4];
                        foundProduct.productShippingWeight = (float)reader[5];
                        foundProduct.productDimensions = (string)reader[6];
                        foundProduct.productKeyWords = (string)reader[7];
                        foundProduct.productSuggestedUse = (string)reader[8];
                        foundProduct.productIngridients = (string)reader[9];
                        foundProduct.productWarnings = (string)reader[10];
                        foundProduct.productDisclaimer = (string)reader[11];
                        foundProduct.productId = (int)reader[12];
                        foundProduct.productManufacturerLink = (string)reader[13];*/
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
