using CapstoneProject.Data;
using CapstoneProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Services
{
    public class OrderService
    {
        OrderDAO service = new OrderDAO();

        /// <summary>
        /// Retrieves All Orders by userId from the DAO service
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List<OrderModel></returns>
        public List<OrderModel> getOrders(int userId) 
        {
            return service.getOrders(userId);
        }

        /// <summary>
        /// Retrieves All Order Items by orderId from the DAO service
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>List<OrderItem></returns>
        public List<OrderItem> getOrderItemsByOrderId(int orderId)
        {
            return service.getOrderItemsByOrderId(orderId);
        }

        /// <summary>
        /// Creates a new order by calling service method
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orderDate"></param>
        /// <param name="total"></param>
        /// <returns>int</returns>
        public int newOrder(int userId, DateTime orderDate, float total) 
        {
            return service.newOrder(userId, orderDate, total);
        }

        /// <summary>
        /// Creates a new order item by calling service method
        /// </summary>
        /// <param name="orderItem"></param>
        /// <returns>int</returns>
        public int newOrderItems(OrderItem orderItem) 
        {
            return service.newOrderItems(orderItem);
        }
    }
}
