using CapstoneProject.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Data
{
    public class OrderDAO
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=aspnet-CapstoneProject-96D9AAD4-C520-424C-AF81-4A8C2D540CE1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        ProductDAO service = new ProductDAO();

        /// <summary>
        /// Retrieves All Orders by userId from the database table
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List<OrderModel></returns>
        public List<OrderModel> getOrders(int userId) 
        {
            List<OrderModel> orders = new List<OrderModel>();

            string sqlStatement = "SELECT * FROM dbo.Orders WHERE userId = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@id", userId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        orders.Add(new OrderModel
                        {
                            Id = (int)reader[0],
                            userId = (int)reader[1],
                            orderDate = (string)reader[2],
                            total = Convert.ToSingle(reader[3])
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return orders;
        }

        /// <summary>
        /// Retrieve Order Items by orderId from the database table
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>List<OrderItem></returns>
        public List<OrderItem> getOrderItemsByOrderId(int orderId) 
        {
            List<OrderItem> orderItems = new List<OrderItem>();

            string sqlStatement = "SELECT * FROM dbo.OrderItem WHERE orderId = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@id", orderId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        orderItems.Add(new OrderItem
                        {
                            Id = (int)reader[0],
                            orderId = orderId,
                            productId = (int)reader[2],
                            quantity = (int)reader[3],
                            subtotal = Convert.ToSingle(reader[4]),
                            product = service.GetProductById((int)reader[2])
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return orderItems;
        }

        /// <summary>
        /// Creates a new Order entry in the database table
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orderDate"></param>
        /// <param name="total"></param>
        /// <returns>int</returns>
        public int newOrder(int userId, DateTime orderDate, float total) 
        {
            int result = 0;
            string sqlStatement = "INSERT INTO dbo.Orders (userId, dateTime, total) OUTPUT INSERTED.Id VALUES (@userId, @dateTime, @total)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@dateTime", orderDate);
                command.Parameters.AddWithValue("@total", total);

                try
                {
                    connection.Open();

                    result = (int)command.ExecuteScalar();

                    if (result > 0)
                    {
                        connection.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return result;
        }

        /// <summary>
        /// Creates a new Order Item entry in the database table
        /// </summary>
        /// <param name="orderItem"></param>
        /// <returns>int</returns>
        public int newOrderItems(OrderItem orderItem) 
        {
            int result = -1;
            string sqlStatement = "INSERT INTO dbo.OrderItem (orderId, productId, quantity, subtotal) VALUES (@orderId, @productId, @quantity, @subtotal)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@orderId", orderItem.orderId);
                command.Parameters.AddWithValue("@productId", orderItem.productId);
                command.Parameters.AddWithValue("@quantity", orderItem.quantity);
                command.Parameters.AddWithValue("@subtotal", orderItem.subtotal);

                try
                {
                    connection.Open();                   

                    if (command.ExecuteNonQuery() > 0)
                    {
                        result = 1;
                        connection.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return result;
        }
    }
}
