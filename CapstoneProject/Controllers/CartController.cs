/* This is a C# code for a controller named `CartController` in a web application. It imports necessary
namespaces and defines the controller class that inherits from the `Controller` class. It also
defines a constructor that sets the `userSession` variable to the current context session using the
`IHttpContextAccessor` interface. The controller has several action methods that handle cart-related
functionalities such as adding items to cart, updating cart items, and removing items from cart. It
also has a private method named `isExist` that checks if an item already exists in the cart. The
controller uses a `ProductService` class to retrieve product information and a `Session` object to
store and retrieve cart information. The views associated with the controller are not shown in this
code. */
using CapstoneProject.Helper;
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
    /* The `CartController` class manages the user's shopping cart by adding, updating, and removing
    products from the cart, and displaying the cart and total price. */
    public class CartController : Controller
    {
        ProductService service = new ProductService();
        private readonly ISession userSession;

        /* This is a constructor for the `CartController` class that takes an `IHttpContextAccessor`
        object as a parameter. It sets the `userSession` variable to the current session of the
        `HttpContextAccessor` object. This allows the controller to access and manipulate session
        data for the current user. */
        public CartController(IHttpContextAccessor httpContextAccessor)
        {
            this.userSession = httpContextAccessor.HttpContext.Session;
        }


        /// This function retrieves the cart items from the session, calculates the total price of the items,
        /// and returns the view with the cart and total price as viewbag variables.
        /// 
        /// @return The method is returning a View with the ViewBag containing the cart and total price of the
        /// items in the cart.
        public IActionResult Index()
        {
            var cart = Session.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.productPrice * item.Quantity);
            return View();
        }

        /// This function returns an empty view for the cart.
        /// 
        /// @return A view is being returned.
        public IActionResult EmptyCart() 
        {
            return View();
        }


        /// The function adds a product to the user's cart and redirects to the index page.
        /// 
        /// @param id The ID of the product being added to the cart.
        /// @param quantity The quantity parameter is an integer that represents the number of items the
        /// user wants to add to their cart.
        /// 
        /// @return The method is returning an IActionResult object. The specific action being returned
        /// is a RedirectToAction to the "Index" action.
        public IActionResult AddToCart(int id, int quantity) 
        {
            if (string.IsNullOrEmpty(userSession.GetString("userId")))
            {
                return RedirectToAction("Login", "Home");
            }
            else 
            {
                if (Session.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
                {
                    List<Item> cart = new List<Item>();
                    cart.Add(new Item { Product = service.GetProductById(id), Quantity = quantity, productId = id });
                    Session.SetObjectAsJson(HttpContext.Session, "cart", cart);
                }
                else
                {
                    List<Item> cart = Session.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                    int index = isExist(id);
                    if (index != -1)
                    {
                        cart[index].Quantity++;
                    }
                    else
                    {
                        cart.Add(new Item { Product = service.GetProductById(id), Quantity = quantity, productId = id });
                    }
                    Session.SetObjectAsJson(HttpContext.Session, "cart", cart);
                }
            }
            return RedirectToAction("Index");
        }


        /// This function updates the quantity of a product in the user's shopping cart.
        /// 
        /// @param id The id parameter is an integer that represents the unique identifier of a product
        /// in the shopping cart.
        /// @param quantity The quantity parameter represents the new quantity of a product that the
        /// user wants to update in their shopping cart.
        /// 
        /// @return The method is returning an IActionResult object, which is a result of redirecting to
        /// the "Index" action.
        public IActionResult UpdateCart(int id, int quantity) 
        {
            List<Item> cart = Session.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");           
            int index = isExist(id);
            if (index != -1)
            {
                cart[index] = new Item { Product = service.GetProductById(id), Quantity = quantity, productId = id };
            }

            Session.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        /// This function removes a product from a shopping cart stored in a session object in ASP.NET
        /// Core.
        /// 
        /// @param id an integer representing the ID of the product to be removed from the cart.
        /// 
        /// @return The method is returning a `RedirectToAction` result, which redirects the user to the
        /// "Index" action method.
        public IActionResult RemoveProduct(int id)
        {
            List<Item> cart = Session.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            Session.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        /// The function checks if an item with a given ID exists in a shopping cart stored in a session
        /// object and returns its index if found.
        /// 
        /// @param id an integer representing the ID of a product that needs to be checked if it exists
        /// in the shopping cart.
        /// 
        /// @return The method `isExist` returns an integer value which represents the index of the item
        /// in the cart list if it exists, otherwise it returns -1.
        private int isExist(int id)
        {
            List<Item> cart = Session.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.id == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
