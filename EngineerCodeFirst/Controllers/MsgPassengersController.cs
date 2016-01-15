using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EngineerCodeFirst.DAL;
using EngineerCodeFirst.Models;

namespace EngineerCodeFirst.Controllers
{
    public class MsgPassengersController : Controller
    {
        private TransportPublicContext db = new TransportPublicContext();

        // GET: MsgPassengers
        public ActionResult Index()
        {
            return View(db.MsgPassengers.ToList());
        }

        // GET: MsgPassengers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MsgPassenger msgPassenger = db.MsgPassengers.Find(id);
            if (msgPassenger == null)
            {
                return HttpNotFound();
            }
            return View(msgPassenger);
        }

        // GET: MsgPassengers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MsgPassengers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MsgPassengerID,BusID,ValidityPeriod,Text,TimeStamp,Status")] MsgPassenger msgPassenger)
        {
            if (ModelState.IsValid)
            {
                db.MsgPassengers.Add(msgPassenger);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(msgPassenger);
        }

        // GET: MsgPassengers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MsgPassenger msgPassenger = db.MsgPassengers.Find(id);
            if (msgPassenger == null)
            {
                return HttpNotFound();
            }
            return View(msgPassenger);
        }

        // POST: MsgPassengers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MsgPassengerID,BusID,ValidityPeriod,Text,TimeStamp,Status")] MsgPassenger msgPassenger)
        {
            if (ModelState.IsValid)
            {
                db.Entry(msgPassenger).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(msgPassenger);
        }

        // GET: MsgPassengers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MsgPassenger msgPassenger = db.MsgPassengers.Find(id);
            if (msgPassenger == null)
            {
                return HttpNotFound();
            }
            return View(msgPassenger);
        }

        // POST: MsgPassengers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MsgPassenger msgPassenger = db.MsgPassengers.Find(id);
            db.MsgPassengers.Remove(msgPassenger);
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
