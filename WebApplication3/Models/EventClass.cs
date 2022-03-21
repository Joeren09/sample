using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class EventClass
    {
        public int event_id { get; set; }
        [Required]
        public string event_name { get; set; }
        public string E_StartDate { get; set; }
        public string E_ExpireDate { get; set; }

        public List<EventClass> eventlist { get; set;}
    }
}