/* This is a C# code for a controller class named `ProductController` in a web application. */
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
    /* The `ProductController` class contains methods for retrieving and displaying product information
    from a service in a view. */
    public class ProductController : Controller
    {
        ProductService service = new ProductService();

        /// This function returns a view with all the products retrieved from a service.
        /// 
        /// @return The `Index` action method is returning a view with all the products retrieved from
        /// the `getAllProducts` method of the `service` object.
        public IActionResult Index()
        {
            return View(service.getAllProducts());
        }

        
        /// The function returns a view with a tuple model containing a product and its supplements
        /// based on the given product ID.
        /// 
        /// @param productId an integer representing the unique identifier of a product. This parameter
        /// is used to retrieve information about a specific product and its associated supplements from
        /// a service. The retrieved data is then passed to a view for display.
        /// 
        /// @return The method is returning a view with a tuple model containing a Product object and a
        /// Supplements object retrieved from the service based on the provided productId.
        public IActionResult ProductDetails(int productId)
        {
            var tupleModel = new Tuple<Product, Supplements>(service.GetProductById(productId), service.GetSupplementByProductId(productId));

            return View(tupleModel);
        }
    }
}
