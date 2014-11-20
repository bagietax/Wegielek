using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using MvcMovie.Dal;

namespace MvcMovie.Models
{
    public class Client
    {
        public int ClientID { get; set; }

        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Display(Name = "Nazwisko")]
        public string Surnname { get; set; }
        [Display(Name = "Numer telefonu")]
        public string TelephoneNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "Ostatni zakup")]
        public DateTime LastBoughtTime { get; set; }
        [Display(Name = "Ostatnia ilość")]
        public double LastBoughtAmountInTon { get; set; }

        [Display(Name = "Szablon SMS")]
        public string SmsTemplate { get; set; }

        public string SmsTemplateShort { get { return SmsTemplate.Substring(0, 100); } }

        [Display(Name = "Poinformowano")]
        public bool IsInformed { get; set; }

        [Display(Name = "Zamówienia")]
        public virtual ICollection<Order> Orders { get; set; }

        [Display(Name = "Pelna nazwa")]
        public string FullName { get { return Name + " " + Surnname; } }
    }

    public class ClientSeeder
    {
        public void Seed(CoalCubeDbContex db)
        {
            var client = new Client()
            {
                Name = "Rafał",
                Surnname = "Maciąg",
                LastBoughtTime = new DateTime(2014, 1, 20),
                LastBoughtAmountInTon = 3,
                TelephoneNumber = "500600700",
                SmsTemplate = "Kupił pan za mało węgla"
            };
            db.Clients.Add(client);

            client = new Client()
            {
                Name = "Paweł",
                Surnname = "Matejczyk",
                LastBoughtTime = new DateTime(2012, 7, 20),
                LastBoughtAmountInTon = 5,
                TelephoneNumber = "501502503",
                SmsTemplate = "Kupił pan za mało węgla"
            };
            db.Clients.Add(client);

            client = new Client()
            {
                Name = "Artur",
                Surnname = "Perek",
                LastBoughtTime = new DateTime(2013, 5, 20),
                LastBoughtAmountInTon = 6,
                TelephoneNumber = "502503504",
                SmsTemplate = "Kupił pan za mało węgla"
            };
            db.Clients.Add(client);

            client = new Client()
            {
                Name = "Michał",
                Surnname = "Nowak",
                LastBoughtTime = new DateTime(2014, 2, 11),
                LastBoughtAmountInTon = 1,
                TelephoneNumber = "504505506",
                SmsTemplate = "Kupił pan za mało węgla"
            };
            db.Clients.Add(client);

            client = new Client()
            {
                Name = "Agnieszka",
                Surnname = "Dutkowska",
                LastBoughtTime = new DateTime(2014, 1, 3),
                LastBoughtAmountInTon = 15,
                TelephoneNumber = "505506507",
                SmsTemplate = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit...Nie ma nikogo, kto lubiłby ból dla samego bólu, szukał go tylko po to, by go poczuć, po prostu dlatego, że to ból.."
            };
            db.Clients.Add(client);
            db.SaveChanges();
        }
    }
}