/* This is a C# controller class for handling payments using Stripe API. It includes necessary
dependencies such as `CapstoneProject.Models`, `Microsoft.AspNetCore.Mvc`, `CapstoneProject.Helper`,
`Stripe`, `Stripe.Checkout`, `CapstoneProject.Services`, `Microsoft.AspNetCore.Http`, and
`System.Net.Mail`. The class defines three methods for handling different payment scenarios:
`Index()`, `Success()`, and `Cancel()`. The `Create()` method is responsible for creating a checkout
session with Stripe API and returning the appropriate URL for success or cancellation. */
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
using System.Net.Mail;

namespace CapstoneProject.Controllers
{
    [Route("create-checkout-session")]
    [ApiController]
    /* The PaymentsController class contains C# functions for creating a Stripe checkout session with
    line items and returning views for the Index, Success, and Cancel pages. */
    public class PaymentsController : Controller
    {

        /// This is a C# function that returns a view for the Index page.
        /// 
        /// @return The `Index` action method is returning a `ViewResult` object, which represents a
        /// view that will be rendered as the response to the client's request.
        public IActionResult Index()
        {

            return View();
        }
        /// This is a C# function that returns a view for a successful action.
        /// 
        /// @return A view result is being returned.
        public IActionResult Success()
        {
            return View();
        }

        /// The function returns a view for canceling an action.
        /// 
        /// @return A View result is being returned.
        public IActionResult Cancel()
        {

            return View();
        }


        
        /// This function creates a Stripe checkout session with line items and redirects the user to
        /// the session URL.
        /// 
        /// @return A StatusCodeResult with a status code of 303.
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
                CancelUrl = domain + "Cart/Index",
            };
            var service = new SessionService();
            Stripe.Checkout.Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

    }
}
