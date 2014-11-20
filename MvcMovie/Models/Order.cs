using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMovie.Models
{
    public class Order
    {
        public int ID { get; set; }
        [Display(Name = "Data sprzedaży")]
        public DateTime When { get; set; }
        [Display(Name = "Ilość zakupu")]
        public Double Amout { get; set; }
        [Display(Name = "Wysokość zakupu")]
        public Double Cost { get; set; }

        private int _clientID;
        [Display(Name = "Id Klienta")]
        public int ClientID {
            get { return _clientID; }
            set { Int32.TryParse(value.ToString(), out _clientID); }
        }

        [Display(Name = "Klient")]
        public virtual Client Client { get; set; }

        

    }
   
}