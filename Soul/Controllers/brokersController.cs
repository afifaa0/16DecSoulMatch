using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Soul.Models;

namespace Soul.Controllers
{
    public class brokersController : Controller
    {
        private DbModels db = new DbModels();

        // GET: brokers
        public ActionResult Index()
        {
            return View(db.brokers.ToList());
        }

        // GET: brokers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            broker broker = db.brokers.Find(id);
            if (broker == null)
            {
                return HttpNotFound();
            }
            return View(broker);
        }

        // GET: brokers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: brokers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Name,Email,City,CNIC,Contact_No,BrokerID")] broker broker)
        {
            if (ModelState.IsValid)
            {
                db.brokers.Add(broker);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(broker);
        }

        // GET: brokers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            broker broker = db.brokers.Find(id);
            if (broker == null)
            {
                return HttpNotFound();
            }
            return View(broker);
        }

        // POST: brokers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Name,Email,City,CNIC,Contact_No,BrokerID")] broker broker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(broker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(broker);
        }

        // GET: brokers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            broker broker = db.brokers.Find(id);
            if (broker == null)
            {
                return HttpNotFound();
            }
            return View(broker);
        }

        // POST: brokers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            broker broker = db.brokers.Find(id);
            db.brokers.Remove(broker);
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
