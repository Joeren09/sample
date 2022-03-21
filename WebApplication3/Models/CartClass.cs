using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class CartClass
    {
        public int cart_items_id { get; set; }
        public int user_id { get; set; }
        public int product_id { get; set; }
        public int product_qty { get; set; }
        public decimal product_price { get; set; }
        public string product_name { get; set; }
        public decimal total_val { get; set; }

        public List<CartClass> cartlist { get; set; }
    }
}