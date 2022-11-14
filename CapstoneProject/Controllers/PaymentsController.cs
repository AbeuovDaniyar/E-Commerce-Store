using CapstoneProject.Models;
using Microsoft.AspNetCore.Mvc;
using CapstoneProject.Helper;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapstoneProject.Services;
using Microsoft.AspNetCore.Http;

namespace CapstoneProject.Controllers
{
    [Route("create-checkout-session")]
    [ApiController]
    public class PaymentsController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Cancel()
        {

            return View();
        }


        /// <summary>
        /// API post request to Stripe API to create a checkout-session
        /// </summary>
        /// <returns>SuccessUrl or CancelUrl</returns>
        [HttpPost]
        public ActionResult Create()
        {
            var domain = "https://localhost:44379/";
            List<Item> cart = Helper.Session.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

            List<SessionLineItemOptions> LineItemOptions = new List<SessionLineItemOptions>();
            foreach (var item in cart) 
            {
                var currentLineItem = new SessionLineItemOptions { Price = item.Product.productStripePriceId, Quantity = item.Quantity };
                LineItemOptions.Add(currentLineItem);
            }


            var options = new SessionCreateOptions
            {
                LineItems = LineItemOptions,
                Mode = "payment",
                SuccessUrl = domain + "Order/Index",
                CancelUrl = domain + "Payments/cancel",
            };
            var service = new SessionService();
            Stripe.Checkout.Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

    }
}
