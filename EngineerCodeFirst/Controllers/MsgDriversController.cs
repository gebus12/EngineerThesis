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
using System.Web.Script.Serialization;

namespace EngineerCodeFirst.Controllers
{
    public class MsgDriversController : Controller
    {
        private TransportPublicContext db = new TransportPublicContext();

        // GET: MsgDrivers
        public ActionResult Index()
        {
            var msgDrivers = db.MsgDrivers.Include(m => m.Bus).Include(m => m.Driver);
            return View(msgDrivers.ToList());
        }

        // GET: MsgDrivers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MsgDriver msgDriver = db.MsgDrivers.Find(id);
            if (msgDriver == null)
            {
                return HttpNotFound();
            }
            return View(msgDriver);
        }

        // GET: MsgDrivers/Create
        public ActionResult Create()
        {
            ViewBag.BusID = new SelectList(db.Buses, "BusID", "RegNum");
            ViewBag.DriverID = new SelectList(db.Drivers, "DriverID", "DriverInfo");
            return View();
        }

        // POST: MsgDrivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MsgDriverID,DriverID,BusID,Text,TimeStamp,Status,Receiver")] MsgDriver msgDriver)
        {
            if (ModelState.IsValid)
            {
                db.MsgDrivers.Add(msgDriver);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusID = new SelectList(db.Buses, "BusID", "RegNum", msgDriver.BusID);
            ViewBag.DriverID = new SelectList(db.Drivers, "DriverID", "DriverInfo", msgDriver.DriverID);
            return View(msgDriver);
        }

        // GET: MsgDrivers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MsgDriver msgDriver = db.MsgDrivers.Find(id);
            if (msgDriver == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusID = new SelectList(db.Buses, "BusID", "RegNum", msgDriver.BusID);
            ViewBag.DriverID = new SelectList(db.Drivers, "DriverID", "DriverInfo", msgDriver.DriverID);
            return View(msgDriver);
        }

        // POST: MsgDrivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MsgDriverID,DriverID,BusID,Text,TimeStamp,Status,Receiver")] MsgDriver msgDriver)
        {
            if (ModelState.IsValid)
            {
                db.Entry(msgDriver).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusID = new SelectList(db.Buses, "BusID", "RegNum", msgDriver.BusID);
            ViewBag.DriverID = new SelectList(db.Drivers, "DriverID", "DriverInfo", msgDriver.DriverID);
            return View(msgDriver);
        }

        // GET: MsgDrivers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MsgDriver msgDriver = db.MsgDrivers.Find(id);
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
            MsgDriver msgDriver = db.MsgDrivers.Find(id);
            db.MsgDrivers.Remove(msgDriver);
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

        // GET: MsgDrivers/MarkAsRead/5
        public ActionResult MarkAsRead(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MsgDriver msgDriver = db.MsgDrivers.Find(id);
            if (msgDriver == null)
            {
                return HttpNotFound();
            }
            msgDriver.Status = Status.Read;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MarkAsRead([Bind(Include = "MsgDriverID,DriverID,BusID,Text,TimeStamp,Status,Receiver")] MsgDriver msgDriver)
        {
            if (ModelState.IsValid)
            {
                db.Entry(msgDriver).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(msgDriver);
        }

        [HttpPost]
        public ActionResult getMessageFromDriver()
        {
            MsgDriver receivedMessage = new MsgDriver();
            try
            {
                Request.InputStream.Position = 0;
                var jsonString = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();

                receivedMessage = js.Deserialize<MsgDriver>(jsonString);
                receivedMessage.Status = Status.Unread;
                receivedMessage.Receiver = Receiver.Provider;
                receivedMessage.Bus = db.Buses.Find(receivedMessage.BusID);
                receivedMessage.Driver = db.Drivers.Find(receivedMessage.DriverID);
                db.MsgDrivers.Add(receivedMessage);

                db.SaveChanges();

                String message = "DONE"; // change this value to some global constant
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                // Creation failed
                String message = "FAIL"; // change this value to some global constant
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
