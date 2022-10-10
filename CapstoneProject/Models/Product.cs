using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{
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

        public Product() { }
    }
}
