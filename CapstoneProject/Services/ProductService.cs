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
        public List<Product> getAllProducts() 
        {
            return daoService.getAllProducts();
        }

        public Product GetProductById(int productId) 
        {
            return daoService.GetProductById(productId);
        }

        public Supplements GetSupplementByProductId(int productId) 
        {
            return daoService.GetSupplementByProductId(productId);
        }

    }
}
