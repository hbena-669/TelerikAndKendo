using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelerikAndKendo.Models
{
    public class Customer
    {
        public int OrderID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string ShipName { get; set; }
        public string ShipCity { get; set; }
    }
}