using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{
    
    /* The OrderModel class represents an order with an ID, user ID, order date, total cost, and a list
    of products. */
    public class OrderModel
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public string orderDate { get; set; }

        [DataType(DataType.Currency)]
        public float total { get; set; }
        public List<Product> products { get; set; }
    }
}
