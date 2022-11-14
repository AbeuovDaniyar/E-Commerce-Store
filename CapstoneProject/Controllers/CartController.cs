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
    public class CartController : Controller
    {
        ProductService service = new ProductService();
        private readonly ISession userSession;

        /// <summary>
        /// Cart Controller sets userSession variable to current Context Session
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public CartController(IHttpContextAccessor httpContextAccessor)
        {
            this.userSession = httpContextAccessor.HttpContext.Session;
        }
        /// <summary>
        /// Index method initializes cart
        /// </summary>
        /// <returns>View(Index.cshtml)</returns>
        public IActionResult Index()
        {
            var cart = Session.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.productPrice * item.Quantity);
            return View();
        }

        /// <summary>
        /// Returns View for Empty Cart
        /// </summary>
        /// <returns>View(EmptyCart.cshtml)</returns>
        public IActionResult EmptyCart() 
        {
            return View();
        }

        /// <summary>
        /// Adds a new item to cart
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <returns>Login View if user is not logged in, or Cart Index View after adding an item to cart</returns>
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

        /// <summary>
        /// Updates cart item values
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <returns>Cart Index View</returns>
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

        public IActionResult RemoveProduct(int id)
        {
            List<Item> cart = Session.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            Session.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

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
