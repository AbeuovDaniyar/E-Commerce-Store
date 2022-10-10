using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{
    public class OrderModel
    {
        public int orderId { get; set; }
        public int productId { get; set; }
        public int userId { get; set; }
        public int orderNumber { get; set; }
        public DateTime orderDate { get; set; }
        public List<Product> products { get; set; }
    }
}
