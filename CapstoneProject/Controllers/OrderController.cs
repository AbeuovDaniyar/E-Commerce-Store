/* The above code is a C# controller for managing orders in a web application. It includes methods for
creating a new order, viewing orders, and viewing order details. It also includes a method for
sending an email to the user with the details of their order using SMTP. The controller uses a
service class to interact with the database and retrieve order information. The code also includes a
helper method for creating an HTML table to display order item information in the email. */
using CapstoneProject.Models;
using CapstoneProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Controllers
{
    /* The OrderController class handles the processing of new orders, displaying order information,
    and sending confirmation emails to users. */
    public class OrderController : Controller
    {
        OrderService orderService = new OrderService();
        private readonly ISession userSession;

        
        /* This is the constructor for the `OrderController` class that takes an `IHttpContextAccessor`
        object as a parameter. It sets the `userSession` field to the current session of the
        `HttpContextAccessor` object. This allows the controller to access and manipulate session
        data for the current user. */
        public OrderController(IHttpContextAccessor httpContextAccessor)
        {
            this.userSession = httpContextAccessor.HttpContext.Session;
        }

        
        /// The function returns a view of orders if a new order is processed successfully, otherwise it
        /// returns an empty view.
        /// 
        /// @return If the result of the `ProcessNewOrder()` method is greater than -1, the method will
        /// return a view of the orders for the user with the ID stored in the `userSession` object.
        /// Otherwise, it will return an empty view.
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

        
        /// This function returns a view of orders for a specific user.
        /// 
        /// @return An IActionResult object is being returned, which will render a view named "Index"
        /// and pass the result of the getOrders method of the orderService object as a parameter. The
        /// getOrders method takes an integer parameter representing the user ID and returns a list of
        /// orders.
        public IActionResult ViewOrders() 
        {
            return View("Index", orderService.getOrders(Convert.ToInt32(this.userSession.GetString("userId"))));
        }

        
        /// This function returns a view of order items based on the given order ID.
        /// 
        /// @param orderId The parameter "orderId" is an integer value that represents the unique
        /// identifier of an order. This method is used to retrieve the details of an order by passing
        /// its orderId as a parameter. The method calls the getOrderItemsByOrderId method of the
        /// orderService object and returns the result to the view.
        /// 
        /// @return The method is returning a view with the order items for a specific order ID.
        public IActionResult Details(int orderId) 
        {

            return View(orderService.getOrderItemsByOrderId(orderId));
        }

        
        /// The function processes a new order by calculating the total price, creating a new order and
        /// order items, and sending a confirmation email.
        /// 
        /// @return The method is returning an integer value, which is either -1 or 1 depending on
        /// whether the order items were successfully added to the database.
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

            SendMail(orderService.getOrderItemsByOrderId(orderId), orderId);

            return result;
        }

        
        /// This function sends an email to the user containing a table of their order items and order
        /// ID.
        /// 
        /// @param orderItems A list of OrderItem objects representing the items in the order.
        /// @param orderId The unique identifier for the order being sent in the email.
        public void SendMail(List<OrderItem> orderItems, int orderId)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("dannyabeuov@gmail.com");
            msg.To.Add(this.userSession.GetString("userEmail"));

            string htmlmsgbody = GetMyTable(orderItems, x => x.Id, x => x.product.productName, x => x.quantity, x => x.subtotal);

            msg.Body = htmlmsgbody;
            msg.IsBodyHtml = true;

            msg.Subject = "Your Order #" + orderId;
            SmtpClient smt = new SmtpClient("smtp.gmail.com");
            

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential("dannyabeuov@gmail.com", "yfrjrzvxvinukxyp");
                smtp.EnableSsl = true;
                smtp.Send(msg);
            }
        }

        
        /// The function generates an HTML table with specified columns for a given list of objects.
        /// 
        /// @param list An IEnumerable collection of objects of type T, which represents the data to be
        /// displayed in the table.
        /// 
        /// @return A string containing an HTML table with the specified columns and data from the
        /// provided list. The table includes a header row with column names and a message at the
        /// beginning.
        public static string GetMyTable<T>(IEnumerable<T> list, params Func<T, object>[] columns)
        {

            var sb = new StringBuilder();

            sb.Append("Dear Customer,");
            sb.Append("<br/>");
            sb.Append("Thank you for shopping with us, here is the details for your order: ");
            sb.Append("<br/>");

            sb.Append("<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:Arial'>");
            sb.Append("<tr>");         
                sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Item Id</th>");
                sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Product</th>");
                sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Quantity</th>");
                sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Subtotal ($)</th>");
            sb.Append("</tr>");

            foreach (var item in list) 
                {
                    sb.Append("<tr>");
                        foreach (var column in columns)
                            sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + column(item) + "</td>");
                    sb.Append("</tr>");
                }
            sb.Append("</table>");
            return sb.ToString();
        }
    }
}
