using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class BookClass
    {
        public int product_id { get; set; }
        public string product_name { get; set; }
        public int category_id { get; set; }
        public int product_price { get; set; }
        public int product_qty { get; set; }

        public string product_image { get; set; }
        public HttpPostedFileBase UploadFile { get; set; }
    }
}