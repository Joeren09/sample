using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class AccClass
    {
        public int user_id { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string contact_no { get; set; }

        public static DateTime Now { get; }
        public List<AccClass> Accinfo { get; set; }
    }
}