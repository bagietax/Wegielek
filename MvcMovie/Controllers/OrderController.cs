using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MvcMovie.Dal;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class OrderController : Controller
    {
        private CoalCubeDbContex db = new CoalCubeDbContex();



        //
        // GET: /Order/

        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Client);
            return View(orders.ToList());
        }

        //
        // GET: /Order/Details/5

        public ActionResult Details(int id = 0)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        //
        // GET: /Order/Create

        public ActionResult Create()
        {
            ViewBag.ParentId = new SelectList(db.Clients, "ID", "Name");
            List<SelectListItem> li = db.Clients.Select(x => new SelectListItem() 
            { Text = x.Surnname, Value = SqlFunctions.StringConvert((double)x.ClientID).Trim() }
            ).ToList();
            ViewData["Clients"] = li;
            return View();
        }

        //
        // POST: /Order/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                var client = db.Clients.FirstOrDefault(x => x.ClientID == order.ClientID);
                order.Client = client;
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.ParentId = new SelectList(db.Clients, "ID", "Name", order.ID);
            return View(order);
        }

        //
        // GET: /Order/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentId = new SelectList(db.Clients, "ID", "Name", order.ID);
            return View(order);
        }

        //
        // POST: /Order/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(db.Clients, "ID", "Name", order.ID);
            return View(order);
        }

        //
        // GET: /Order/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        //
        // POST: /Order/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}