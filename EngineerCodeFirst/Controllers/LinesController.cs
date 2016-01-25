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
        public ActionResult Create([Bind(Include = "LineID,LineNumber,Direction,Status,ScheduleType")] Line line)
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
        public ActionResult Edit([Bind(Include = "LineID,LineNumber,Direction,Status,ScheduleType")] Line line)
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

        [HttpPost]
        public ActionResult GetDepartureTimes()
        {
            List<int> IDList = new List<int>();
            Dictionary<String, String> returnSet = new Dictionary<String,String>();
            try
            {
                Request.InputStream.Position = 0;
                var jsonString = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
                
                JavaScriptSerializer js = new JavaScriptSerializer();
                IDList = js.Deserialize< List<int> >(jsonString);

                foreach (int x in IDList)
                {
                    try
                    {
                        Line lineToProcess = db.Lines.Find(x); // get the line identified by id from list
                        String departureTime = lineToProcess.Schedules.ElementAt(0).DepartureTime; // look for the first departure time
                        departureTime += "  " + lineToProcess.ScheduleType.ToString(); //save also type of the schedule
                        returnSet.Add(x.ToString(), departureTime); //id has to be converted to String as int dictionary cant be serialized
                    }
                    catch (Exception ex)
                    {
                        // error while obtaining schedule for line
                    }
                }
                if (returnSet.Count() != 0)return Json(returnSet, JsonRequestBehavior.AllowGet);
                else return Json("Empty set", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("FAIL", JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        /*Returns all departures for given stopID and LineNumber
         * 
         * */
        public ActionResult getDeparturesForStopAndLineUrl()
        {
            
            List<String> receivedData = new List<String>(); //first element StopID, second LineNumber
            Dictionary<String, String> returnSet = new Dictionary<String,String>();
            try
            {
                Request.InputStream.Position = 0;
                var jsonString = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
                
                JavaScriptSerializer js = new JavaScriptSerializer();
                receivedData = js.Deserialize<List<String>>(jsonString);

                int stopid = int.Parse(receivedData.ElementAt(0));
                int linenr = int.Parse(receivedData.ElementAt(1));

                List<ScheduleForApp> dataToSend = new List<ScheduleForApp>;

                foreach (Schedule schedule in db.Schedules.ToList())
                {
                    if (schedule.StopID == stopid && schedule.Line.LineNumber == linenr)
                    {
                        dataToSend.Add( new ScheduleForApp(schedule) ); //convert schedule object for application and add to list
                    }
                }

                if (returnSet.Count() != 0)return Json(returnSet, JsonRequestBehavior.AllowGet);
                else return Json("Empty set", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("FAIL", JsonRequestBehavior.AllowGet);
            }

        }
        
    }
}
