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
    public class MsgDriversController : Controller
    {
        private TransportPublicContext db = new TransportPublicContext();

        // GET: MsgDrivers
        public ActionResult Index()
        {
            return View(db.MsgDriver.ToList());
        }

        // GET: MsgDrivers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MsgDriver msgDriver = db.MsgDriver.Find(id);
            if (msgDriver == null)
            {
                return HttpNotFound();
            }
            return View(msgDriver);
        }

        // GET: MsgDrivers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MsgDrivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MsgDriverID,DriverID,BusID,Text,TimeStamp,Status")] MsgDriver msgDriver)
        {
            if (ModelState.IsValid)
            {
                db.MsgDriver.Add(msgDriver);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(msgDriver);
        }

        // GET: MsgDrivers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MsgDriver msgDriver = db.MsgDriver.Find(id);
            if (msgDriver == null)
            {
                return HttpNotFound();
            }
            return View(msgDriver);
        }

        // POST: MsgDrivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MsgDriverID,DriverID,BusID,Text,TimeStamp,Status")] MsgDriver msgDriver)
        {
            if (ModelState.IsValid)
            {
                db.Entry(msgDriver).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(msgDriver);
        }

        // GET: MsgDrivers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MsgDriver msgDriver = db.MsgDriver.Find(id);
            if (msgDriver == null)
            {
                return HttpNotFound();
            }
            return View(msgDriver);
        }

        // POST: MsgDrivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MsgDriver msgDriver = db.MsgDriver.Find(id);
            db.MsgDriver.Remove(msgDriver);
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
