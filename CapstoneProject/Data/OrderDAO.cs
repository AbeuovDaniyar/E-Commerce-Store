/* The above code is a C# class that defines a data access object (DAO) for handling orders and order
items in a database. It includes methods for retrieving orders and order items by user ID or order
ID, creating new orders and order items, and retrieving order items as a DataTable. The class uses a
connection string to connect to a Microsoft SQL Server database and includes error handling for
database operations. */
using CapstoneProject.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Data
{
    /* The OrderDAO class contains methods for retrieving and creating orders and order items in a
    database. */
    public class OrderDAO
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=aspnet-CapstoneProject-96D9AAD4-C520-424C-AF81-4A8C2D540CE1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        ProductDAO service = new ProductDAO();

        
        /// This function retrieves a list of orders for a specific user from a database.
        /// 
        /// @param userId The ID of the user whose orders are being retrieved.
        /// 
        /// @return A list of OrderModel objects for a specific user ID.
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

        
        /// This function retrieves a list of order items based on a given order ID from a SQL database.
        /// 
        /// @param orderId The ID of the order for which we want to retrieve the order items.
        /// 
        /// @return The method is returning a list of OrderItem objects that belong to a specific order
        /// ID.
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

        /// This function retrieves a DataTable of order items based on a given order ID.
        /// 
        /// @param orderId an integer value representing the ID of the order for which we want to
        /// retrieve the order items.
        /// 
        /// @return A DataTable containing the order items for a given order ID.
        public DataTable getOrderItemsByOrderIdDataTable(int orderId)
        {
            DataTable dataTable = new DataTable();

            string sqlStatement = "SELECT * FROM dbo.OrderItem WHERE orderId = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.CommandType = CommandType.TableDirect;

                command.Parameters.AddWithValue("@id", orderId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        dataTable.Load(reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return dataTable;
        }

        
        /// The function inserts a new order into a database table and returns the ID of the inserted
        /// row.
        /// 
        /// @param userId an integer representing the user ID of the customer who placed the order.
        /// @param DateTime DateTime is a data type in C# that represents a date and time value. In this
        /// specific method, it is used to specify the date and time when an order is placed.
        /// @param total The total cost of the order.
        /// 
        /// @return The method is returning an integer value, which is the ID of the newly inserted
        /// order in the database.
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

        
        /// This function inserts a new order item into a database table.
        /// 
        /// @param OrderItem An object representing an order item, with properties for orderId,
        /// productId, quantity, and subtotal.
        /// 
        /// @return The method is returning an integer value, which is either -1 or 1.
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
