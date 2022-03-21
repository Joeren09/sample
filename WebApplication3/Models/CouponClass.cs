using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class CouponClass
    {
        public int coupon_id { get; set; }
        [Required]
        public string coupon_name { get; set; }
        public int event_id { get; set; }
        [Required]
        [Range(0.1, 1.0)]
        [DisplayFormat(DataFormatString = "{0:P0}", ApplyFormatInEditMode = true)]
        public decimal discount_val { get; set; }


        public List<CouponClass> couponlist { get; set; }
    }
}