using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class Sms
    {
        public int SmsID { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Text { get; set; }
        public DateTime When { get; set; }
        public bool IsDelivred { get; set; }
        public string ErrorMessage { get; set; }
    }
}