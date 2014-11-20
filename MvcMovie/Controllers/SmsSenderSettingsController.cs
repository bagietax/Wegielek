using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class SmsSenderSettingsController : Controller
    {
        //
        // GET: /SmsSenderSettings/

        public ActionResult Index()
        {
            var setting = new SmsSenderSettings()
            {
                Login = ConfigurationManager.AppSettings["Login"],
                Password = ConfigurationManager.AppSettings["Password"],
                Sender = ConfigurationManager.AppSettings["Sender"]
            };
            return View(setting);
        }

        public ActionResult Create(SmsSenderSettings setting)
        {
            ConfigurationManager.AppSettings["Login"] = setting.Login;
            ConfigurationManager.AppSettings["Password"] = setting.Password;
            ConfigurationManager.AppSettings["Sender"] = setting.Sender;
            return View("Index");
        }

    }
}
