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
using PagedList;
using System.Web.Script.Serialization;

namespace EngineerCodeFirst.Controllers
{
    public class StopsController : Controller
    {
        private TransportPublicContext db = new TransportPublicContext();

        /*
        // GET: Stops
        public ActionResult Index()
        {
            return View(db.Stops.ToList());
        }
        */
       

        public ViewResult Index(string searchString, string currentFilter, int? page)
        {

            /*********** Paging ********************/
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            /******************************************/


            /*********** Search box ********************/
            var stops = from s in db.Stops select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                stops = stops.Where(s =>
                    s.StopName.ToUpper().Contains(searchString.ToUpper())
                    ||
                    s.City.ToUpper().Contains(searchString.ToUpper())
                    );
            }
            /******************************************/

            //return View(stops.ToList());


            /*********** Paging ********************/
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(stops.OrderBy(i => i.StopID).ToPagedList(pageNumber, pageSize));
            /******************************************/
         }
        


        // GET: Stops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stop stop = db.Stops.Find(id);
            if (stop == null)
            {
                return HttpNotFound();
            }
            return View(stop);
        }

        // GET: Stops/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stops/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StopID,City,StopName,Latitude,Longitude")] Stop stop)
        {
            if (ModelState.IsValid)
            {
                db.Stops.Add(stop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stop);
        }

        // GET: Stops/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stop stop = db.Stops.Find(id);
            if (stop == null)
            {
                return HttpNotFound();
            }
            return View(stop);
        }

        // POST: Stops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StopID,City,StopName,Latitude,Longitude")] Stop stop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stop);
        }

        // GET: Stops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stop stop = db.Stops.Find(id);
            if (stop == null)
            {
                return HttpNotFound();
            }
            return View(stop);
        }

        // POST: Stops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stop stop = db.Stops.Find(id);
            db.Stops.Remove(stop);
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

        [HttpGet]
        public ActionResult GetAllStops()
        {
            List<StopForApp> listToReturn = new List<StopForApp>();
            try
            {
                foreach (Stop stop in db.Stops.ToList())
                {
                    //create new list with all sops in app-friendly format
                    listToReturn.Add(new StopForApp(stop));
                }
                if (listToReturn.Count() != 0) return Json(listToReturn, JsonRequestBehavior.AllowGet);
                else return Json("Empty set", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("FAIL", JsonRequestBehavior.AllowGet);
            }
        }


    }
}
