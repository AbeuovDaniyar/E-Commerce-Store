using CapstoneProject.Data;
using CapstoneProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Services
{
    /* The ProductService class provides methods to retrieve products and supplements from a ProductDAO
    object. */
    public class ProductService
    {
        /* `ProductDAO daoService = new ProductDAO();` is creating a new instance of the `ProductDAO`
        class and assigning it to the `daoService` variable of type `ProductDAO`. This allows the
        `ProductService` class to access the methods and properties of the `ProductDAO` class. */
        ProductDAO daoService = new ProductDAO();

        /// This function returns a list of all products using a DAO service.
        /// 
        /// @return A list of all products from the DAO service.
        public List<Product> getAllProducts() 
        {
            return daoService.getAllProducts();
        }

        
        /// This function retrieves a product from the database by its ID.
        /// 
        /// @param productId an integer value representing the unique identifier of a product that needs
        /// to be retrieved from the data source.
        /// 
        /// @return A product object with the specified productId is being returned.
        public Product GetProductById(int productId) 
        {
            return daoService.GetProductById(productId);
        }

        
        /// This function retrieves a supplement by its product ID using a DAO service.
        /// 
        /// @param productId an integer value representing the unique identifier of a product for which
        /// the corresponding supplement needs to be retrieved.
        /// 
        /// @return The method `GetSupplementByProductId` is returning an object of type `Supplements`.
        public Supplements GetSupplementByProductId(int productId) 
        {
            return daoService.GetSupplementByProductId(productId);
        }

    }
}
