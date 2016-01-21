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

namespace EngineerCodeFirst.Controllers
{
    public class SchedulesController : Controller
    {
        private TransportPublicContext db = new TransportPublicContext();

        /*
        // GET: Schedules
        public ActionResult Index()
        {
            var schedules = db.Schedules.Include(s => s.Line).Include(s => s.Stop);
            return View(schedules.ToList());
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


            //var schedules = from s in db.Stops select s;
            var schedules = db.Schedules.Include(s => s.Line).Include(s => s.Stop);
            if (!String.IsNullOrEmpty(searchString))
            {
                schedules = schedules.Where(s =>
                    s.DepartureTime.ToUpper().Contains(searchString.ToUpper())
                    ||
                    s.LineID.ToString().Contains(searchString.ToUpper())
                    ||
                    s.Line.Direction.ToUpper().Contains(searchString.ToUpper())
                    ||
                    s.Stop.City.ToUpper().Contains(searchString.ToUpper())
                    ||
                    s.Stop.StopName.ToUpper().Contains(searchString.ToUpper())
                    );
            }

            /*********** Paging ********************/
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(schedules.OrderBy(i => i.ScheduleID).ToPagedList(pageNumber, pageSize));
            /******************************************/
        }

        // GET: Schedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // GET: Schedules/Create
        public ActionResult Create()
        {
            ViewBag.LineID = new SelectList(db.Lines, "LineID", "LineInfo");
            ViewBag.StopID = new SelectList(db.Stops, "StopID", "StopInfo");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ScheduleID,BusOrder,DepartureTime,LineID,StopID")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Schedules.Add(schedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LineID = new SelectList(db.Lines, "LineID", "Direction", schedule.LineID);
            ViewBag.StopID = new SelectList(db.Stops, "StopID", "City", schedule.StopID);
            return View(schedule);
        }

        // GET: Schedules/Create
        public ActionResult CreateScheduleForLine(int? id)
        {
            ViewBag.LineID = new SelectList(db.Lines, "LineID", "LineInfo", id);
            ViewBag.StopID = new SelectList(db.Stops, "StopID", "StopInfo");
            return View();
        }

       

        // POST: Schedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateScheduleForLine([Bind(Include = "ScheduleID,BusOrder,DepartureTime,LineID,StopID")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Schedules.Add(schedule);
                db.SaveChanges();
                return RedirectToAction("Index", "Lines");
            }

            ViewBag.StopID = new SelectList(db.Stops, "StopID", "City", schedule.StopID);
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.LineID = new SelectList(db.Lines, "LineID", "Direction", schedule.LineID);
            ViewBag.StopID = new SelectList(db.Stops, "StopID", "City", schedule.StopID);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ScheduleID,BusOrder,DepartureTime,LineID,StopID")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LineID = new SelectList(db.Lines, "LineID", "Direction", schedule.LineID);
            ViewBag.StopID = new SelectList(db.Stops, "StopID", "City", schedule.StopID);
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Schedule schedule = db.Schedules.Find(id);
            db.Schedules.Remove(schedule);
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
