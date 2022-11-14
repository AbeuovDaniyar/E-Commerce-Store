using CapstoneProject.Data;
using CapstoneProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Services
{
    public class ProductService
    {
        ProductDAO daoService = new ProductDAO();

        /// <summary>
        /// Retrieves all Products from service method
        /// </summary>
        /// <returns>List<Product></returns>
        public List<Product> getAllProducts() 
        {
            return daoService.getAllProducts();
        }

        /// <summary>
        /// Retrieves Product by id from service method
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>Product</returns>
        public Product GetProductById(int productId) 
        {
            return daoService.GetProductById(productId);
        }

        /// <summary>
        /// retrieves Supplement by product Id from service method
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>Supplements</returns>
        public Supplements GetSupplementByProductId(int productId) 
        {
            return daoService.GetSupplementByProductId(productId);
        }

    }
}
