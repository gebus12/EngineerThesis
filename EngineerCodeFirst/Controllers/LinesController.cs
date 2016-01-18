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
    public class LinesController : Controller
    {
        private TransportPublicContext db = new TransportPublicContext();

        /*
        // GET: Lines
        public ActionResult Index()
        {
            return View(db.Lines.ToList());
        }
        */

        public ViewResult Index(string searchString)
        {
            var lines = from l in db.Lines select l;
            if (!String.IsNullOrEmpty(searchString))
            {
                lines = lines.Where(l =>
                    l.LineNumber.ToString().Contains(searchString)
                    ||
                    l.Direction.ToUpper().Contains(searchString.ToUpper())
                    );
            }

            return View(lines.ToList());
        }

        // GET: Lines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Line line = db.Lines.Find(id);
            if (line == null)
            {
                return HttpNotFound();
            }
            return View(line);
        }

        // GET: Lines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LineID,LineNumber,Direction")] Line line)
        {
            if (ModelState.IsValid)
            {
                db.Lines.Add(line);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(line);
        }

        // GET: Lines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Line line = db.Lines.Find(id);
            if (line == null)
            {
                return HttpNotFound();
            }
            return View(line);
        }

        // POST: Lines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LineID,LineNumber,Direction")] Line line)
        {
            if (ModelState.IsValid)
            {
                db.Entry(line).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(line);
        }

        // GET: Lines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Line line = db.Lines.Find(id);
            if (line == null)
            {
                return HttpNotFound();
            }
            return View(line);
        }

        // POST: Lines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Line line = db.Lines.Find(id);
            db.Lines.Remove(line);
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

        //********************************//
        // Methods for mobile applications
        [HttpGet]
        public ActionResult GetAllLines()
        {
            List<Line> allWebLines = db.Lines.ToList();
            List<LineForApps> allAppLines = new List<LineForApps>();
            if (allWebLines != null)
            {
                foreach (Line x in allWebLines)
                {
                    allAppLines.Add(new LineForApps(x));
                }
                return Json(allAppLines, JsonRequestBehavior.AllowGet);
            }
            else
            {
                String message = "Empty result"; // change this value to some global constant
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }



    }
}
