using CapstoneProject.Models;
using CapstoneProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Controllers
{
    public class OrderController : Controller
    {
        OrderService orderService = new OrderService();
        private readonly ISession userSession;

        /// <summary>
        /// Constructor that initializes userSession variable
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public OrderController(IHttpContextAccessor httpContextAccessor)
        {
            this.userSession = httpContextAccessor.HttpContext.Session;
        }

        /// <summary>
        /// creates a new order and returns a list of orders
        /// </summary>
        /// <returns>Order/Index.cshtml</returns>
        public IActionResult Index()
        {
            if (ProcessNewOrder() > -1)
            {
                return View(orderService.getOrders(Convert.ToInt32(this.userSession.GetString("userId"))));
            }
            else 
            {
                return View();
            }
            
        }

        /// <summary>
        /// Shows order list
        /// </summary>
        /// <returns>Index.cshtml</returns>
        public IActionResult ViewOrders() 
        {
            return View("Index", orderService.getOrders(Convert.ToInt32(this.userSession.GetString("userId"))));
        }

        /// <summary>
        /// Shows order item details
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>Details.cshtml</returns>
        public IActionResult Details(int orderId) 
        {

            return View(orderService.getOrderItemsByOrderId(orderId));
        }

        /// <summary>
        /// Helper method to add new order
        /// </summary>
        /// <returns>int</returns>
        public int ProcessNewOrder()
        {
            int result = -1;
            List<Item> cart = Helper.Session.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            float total = cart.Sum(item => item.Product.productPrice * item.Quantity);
            List<OrderItem> orderItems = new List<OrderItem>();

            int orderId = orderService.newOrder(Convert.ToInt32(this.userSession.GetString("userId")), DateTime.Now, total);

            foreach (var item in cart)
            {
                if (orderService.newOrderItems(new OrderItem
                {
                    orderId = orderId,
                    productId = item.Product.id,
                    quantity = item.Quantity,
                    subtotal = item.Product.productPrice * item.Quantity,
                    product = item.Product
                }) > -1) 
                {
                    result = 1;
                }
                
            }
            return result;
        }
    }
}
