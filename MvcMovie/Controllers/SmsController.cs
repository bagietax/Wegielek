using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http.OData.Builder;
using System.Web.Mvc;
using MvcMovie.Models;
using MvcMovie.Dal;
using SMSApi.Api;
using Client = MvcMovie.Models.Client;
using Exception = System.Exception;

namespace MvcMovie.Controllers
{
    public class SmsController : Controller
    {
        private CoalCubeDbContex db = new CoalCubeDbContex();

        //
        // GET: /Sms/

        public ActionResult Index()
        {
            return View(db.Sms.ToList());
        }

        //
        // GET: /Sms/Details/5

        public ActionResult Details(int id = 0)
        {
            Sms sms = db.Sms.Find(id);
            if (sms == null)
            {
                return HttpNotFound();
            }
            return View(sms);
        }

        //
        // GET: /Sms/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Sms/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Sms sms)
        {
            if (ModelState.IsValid)
            {
                db.Sms.Add(sms);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sms);
        }

        //
        // POST: /Sms/Create

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Send(int id)
        {
            
            Client client = db.Clients.First(x => x.ClientID == id);
            var smsSender = new SmsSender();
            var res=smsSender.SendTo(client);
            ViewData["IsSmsSended"] = res;
            return PartialView(); 
   
        }

 

        //
        // GET: /Sms/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Sms sms = db.Sms.Find(id);
            if (sms == null)
            {
                return HttpNotFound();
            }
            return View(sms);
        }

        
        //
        // POST: /Sms/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Sms sms)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sms).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sms);
        }

        //
        // GET: /Sms/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Sms sms = db.Sms.Find(id);
            if (sms == null)
            {
                return HttpNotFound();
            }
            return View(sms);
        }

        //
        // POST: /Sms/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sms sms = db.Sms.Find(id);
            db.Sms.Remove(sms);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult GetAvaibleSmsCount()
        {
            try
            {
                var sender = new SmsSender();
                var credits = sender.GetAvaiblePln();
                ViewData["AvaiblePLN"] = credits.Points;
                ViewData["ErrorMessage"] = credits.ErrorMessage;
                return PartialView();

            }
            catch (ClientException ex)
            {
                var authError = ex.Message == "Authorization failed";
                ViewData["ErrorMessage"] = authError ? "Bład połączenia do serwisu SMSApi. (Sprawdź login i hasło)" :ex.Message;
                ViewData["AvaiblePLN"] = 0.0;
                return PartialView();
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                ViewData["AvaiblePLN"] = 0.0;
                return PartialView();
            }

        }

    }
}