using CapstoneProject.Helper;
using CapstoneProject.Models;
using CapstoneProject.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Controllers
{
    public class ProductController : Controller
    {
        ProductService service = new ProductService();
        public IActionResult Index()
        {
            return View(service.getAllProducts());
        }

        public IActionResult ProductDetails(int productId)
        {
            var tupleModel = new Tuple<Product, Supplements>(service.GetProductById(productId), service.GetSupplementByProductId(productId));

            return View(tupleModel);
        }
    }
}
