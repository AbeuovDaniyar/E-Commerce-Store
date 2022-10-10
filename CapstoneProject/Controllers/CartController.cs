using CapstoneProject.Helper;
using CapstoneProject.Models;
using CapstoneProject.Services;
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
        public IActionResult Index()
        {
            var cart = Session.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.productPrice * item.Quantity);
            return View();
        }

        public IActionResult AddToCart(int id, int quantity) 
        {
            if (Session.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Product = service.GetProductById(id), Quantity = quantity, productId = id});
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
            return RedirectToAction("Index");
        }

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
