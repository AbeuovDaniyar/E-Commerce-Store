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

            SendMail(orderService.getOrderItemsByOrderId(orderId), orderId);

            return result;
        }

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

            /*
            foreach (var item in list)
            {
                //todo this should actually make an HTML table, not just get the properties requested
                foreach (var column in columns)
                    sb.Append(column(item));
            }*/
            return sb.ToString();
        }
    }
}
