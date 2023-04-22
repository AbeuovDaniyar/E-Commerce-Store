using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{
    /* The class "Address" contains properties for storing information about a user's address. */
    public class Address
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address_text { get; set; }
        public string ZipCode { get; set; }
        public int userId { get; set; }
    }
}
