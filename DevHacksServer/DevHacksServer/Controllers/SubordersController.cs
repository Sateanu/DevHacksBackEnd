using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DevHacksServer.Models;

namespace DevHacksServer.Controllers
{
    public class SubordersController : Controller
    {
        private Entities db = new Entities();

        // GET: Suborders
        public ActionResult Index()
        {
            var suborders = db.Suborders.Include(s => s.Foods).Include(s => s.Orders);
            return View(suborders.ToList());
        }

        // GET: Suborders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suborders suborders = db.Suborders.Find(id);
            if (suborders == null)
            {
                return HttpNotFound();
            }
            return View(suborders);
        }

        // GET: Suborders/Create
        public ActionResult Create()
        {
            ViewBag.FoodID = new SelectList(db.Foods, "Id", "Name");
            ViewBag.OrderID = new SelectList(db.Orders, "Id", "Id");
            return View();
        }

        // POST: Suborders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FoodID,OrderID,Quantity")] Suborders suborders)
        {
            if (ModelState.IsValid)
            {
                db.Suborders.Add(suborders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FoodID = new SelectList(db.Foods, "Id", "Name", suborders.FoodID);
            ViewBag.OrderID = new SelectList(db.Orders, "Id", "Id", suborders.OrderID);
            return View(suborders);
        }

        // GET: Suborders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suborders suborders = db.Suborders.Find(id);
            if (suborders == null)
            {
                return HttpNotFound();
            }
            ViewBag.FoodID = new SelectList(db.Foods, "Id", "Name", suborders.FoodID);
            ViewBag.OrderID = new SelectList(db.Orders, "Id", "Id", suborders.OrderID);
            return View(suborders);
        }

        // POST: Suborders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FoodID,OrderID,Quantity")] Suborders suborders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suborders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FoodID = new SelectList(db.Foods, "Id", "Name", suborders.FoodID);
            ViewBag.OrderID = new SelectList(db.Orders, "Id", "Id", suborders.OrderID);
            return View(suborders);
        }

        // GET: Suborders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suborders suborders = db.Suborders.Find(id);
            if (suborders == null)
            {
                return HttpNotFound();
            }
            return View(suborders);
        }

        // POST: Suborders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Suborders suborders = db.Suborders.Find(id);
            db.Suborders.Remove(suborders);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
