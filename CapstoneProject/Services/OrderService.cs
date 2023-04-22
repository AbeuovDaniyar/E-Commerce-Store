using CapstoneProject.Data;
using CapstoneProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Services
{
    
    /* The OrderService class provides methods for retrieving and creating orders and order items
    through a DAO service. */
    public class OrderService
    {
        /* Creating a new instance of the `OrderDAO` class and assigning it to the `service` variable
        of type `OrderDAO`. This allows the `OrderService` class to access the methods and
        properties of the `OrderDAO` class through the `service` variable. */
        OrderDAO service = new OrderDAO();

        
        /// This function returns a list of order models for a given user ID.
        /// 
        /// @param userId The ID of the user for whom the orders are being retrieved.
        /// 
        /// @return A List of OrderModel objects is being returned.
        public List<OrderModel> getOrders(int userId) 
        {
            return service.getOrders(userId);
        }

        
        /// This function returns a list of order items based on the provided order ID.
        /// 
        /// @param orderId The parameter "orderId" is an integer value that represents the unique
        /// identifier of an order. This method retrieves a list of order items associated with the
        /// specified order ID.
        /// 
        /// @return A list of OrderItem objects that belong to the order with the specified orderId.
        public List<OrderItem> getOrderItemsByOrderId(int orderId)
        {
            return service.getOrderItemsByOrderId(orderId);
        }

        
        /// This function creates a new order with the given user ID, order date, and total cost.
        /// 
        /// @param userId an integer representing the unique identifier of the user who placed the
        /// order.
        /// @param DateTime DateTime is a data type in C# that represents a date and time value. It can
        /// be used to store and manipulate dates and times in various formats. In the given code
        /// snippet, the DateTime parameter is used to specify the date and time of the order.
        /// @param total The total cost of the order.
        /// 
        /// @return The method is returning an integer value, which is the result of calling the
        /// `newOrder` method of the `service` object with the provided parameters `userId`,
        /// `orderDate`, and `total`.
        public int newOrder(int userId, DateTime orderDate, float total) 
        {
            return service.newOrder(userId, orderDate, total);
        }

        
        /// This function adds a new order item to the service.
        /// 
        /// @param OrderItem OrderItem is an object that represents an item in an order. It may contain
        /// information such as the product name, quantity, price, and any other relevant details about
        /// the item being ordered. The method newOrderItems takes an OrderItem object as a parameter
        /// and adds it to the order. The return
        /// 
        /// @return The method `newOrderItems` is returning an integer value, which is the result of
        /// calling the `newOrderItems` method of the `service` object with the `orderItem` parameter.
        public int newOrderItems(OrderItem orderItem) 
        {
            return service.newOrderItems(orderItem);
        }
    }
}
