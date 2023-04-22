using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{
   
    /* The Cart class represents a shopping cart with properties for cart ID, user ID, product IDs,
    total price, and total price after shipping. */
    public class Cart
    {
        public int cartId { get; set; }
        public int userId { get; set; }
        public int[] productId { get; set; }
        public float totalPrice { get; set; }
        public float totalPriceAfterShipping { get; set; }
    }
}
