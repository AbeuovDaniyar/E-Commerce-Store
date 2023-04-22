using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{
    
    /* The Product class contains properties and methods for a product, including its ID, name,
    manufacturer, description, image path, price, stock, and Stripe price ID. */
    public class Product
    {
        public int id { get; set; }
        public string productName { get; set; }
        public string productManufacturer { get; set; }
        public string productDescription { get; set; }
        public string productImagePath { get; set; }
        [DataType(DataType.Currency)]
        public float productPrice { get; set; }
        public int productStock { get; set; }
        public string productStripePriceId { get; set; }

        
        /* This is a constructor for the Product class that takes in several parameters (id,
        productName, productManufacturer, productDescription, productImagePath, productPrice,
        productStock, and productStripePriceId) and assigns them to the corresponding properties of
        the Product object being created. This allows for easy initialization of Product objects
        with all necessary information. */
        public Product(int id, string productName, string productManufacturer, string productDescription, string productImagePath, float productPrice, int productStock, string productStripePriceId)
        {
            this.id = id;
            this.productName = productName;
            this.productManufacturer = productManufacturer;
            this.productDescription = productDescription;
            this.productImagePath = productImagePath;
            this.productPrice = productPrice;
            this.productStock = productStock;
            this.productStripePriceId = productStripePriceId;
        }

        
        /* `public Product() { }` is an empty constructor for the `Product` class. It does not take any
        parameters and does not perform any actions. It is included to allow for the creation of
        `Product` objects without passing in any initial values for the properties. */
        public Product() { }
    }
}
