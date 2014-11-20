using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class SmsSenderSettings
    {
        public int ID { get; set; } 
        public string Login { get; set; }
        public string Password { get; set; }
        public string Sender { get; set; }
    }
}