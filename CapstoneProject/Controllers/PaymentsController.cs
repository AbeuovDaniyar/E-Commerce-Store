using CapstoneProject.Models;
using Microsoft.AspNetCore.Mvc;
using CapstoneProject.Helper;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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


        [HttpPost]
        public ActionResult Create()
        {
            var domain = "http://localhost:44379/Payments";
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
                SuccessUrl = domain + "/success.cshtml",
                CancelUrl = domain + "/cancel.cshtml",
            };
            var service = new SessionService();
            Stripe.Checkout.Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

    }
}
