//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Web;
    using System.ComponentModel.DataAnnotations;
    public partial class Tbl_Coupons
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Coupons()
        {
            this.Tbl_Orders = new HashSet<Tbl_Orders>();
        }
    
        public int coupon_id { get; set; }
        [Required]
        public string coupon_name { get; set; }
        public Nullable<int> event_id { get; set; }
        [Required]
        [Range(0.1, 1.0)]
        [DisplayFormat(DataFormatString = "{0:P0}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> discount_val { get; set; }
        public string couponcode { get; set; }
    
        public virtual Tbl_Events Tbl_Events { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Orders> Tbl_Orders { get; set; }
    }
}
