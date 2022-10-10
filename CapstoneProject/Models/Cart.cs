using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{
    public class Cart
    {
        public int cartId { get; set; }
        public int userId { get; set; }
        public int[] productId { get; set; }
        public float totalPrice { get; set; }
        public float totalPriceAfterShipping { get; set; }
    }
}
