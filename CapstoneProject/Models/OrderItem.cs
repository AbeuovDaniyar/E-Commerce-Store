using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{
    
    /* The class OrderItem contains properties related to an order item, including its ID, order ID,
    product ID, quantity, subtotal, and associated product. */
    public class OrderItem
    {
        public int Id { get; set; }
        public int orderId { get; set; }
        public int productId {get; set;}
        public int quantity { get; set; }
        public float subtotal { get; set; }
        public Product product { get; set; }
    }
}
