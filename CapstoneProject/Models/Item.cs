using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{

    /* The class "Item" represents a product and its quantity, with a property for the product and its
    ID. */
    public class Item
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public int productId { get; set; }

    }
}
