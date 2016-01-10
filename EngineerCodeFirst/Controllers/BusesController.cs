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
using EngineerCodeFirst.ViewModel;
using System.Data.Entity.Infrastructure;
using System.Net.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json;



namespace EngineerCodeFirst.Controllers
{
    public class BusesController : Controller
    {
        private TransportPublicContext db = new TransportPublicContext();

        /*
        // GET: Buses
        public ActionResult Index()
        {
            return View(db.Buses.ToList());
        }
        */

        public ViewResult Index(string searchString)
        {
            var buses = from b in db.Buses select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                buses = buses.Where(b =>
                    b.RegNum.ToUpper().Contains(searchString.ToUpper())
                    );
            }

            return View(buses.ToList());
        }
        

        // GET: Buses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus = db.Buses.Find(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            return View(bus);
        }

        // GET: Buses/Create
        public ActionResult Create()
        {
            //***************** adding drivers ******************//
            var bus = new Bus();
            bus.Drivers = new List<Driver>();
            PopulateAssignedDriverData(bus);

            bus.Lines = new List<Line>();   //********* adding lines*********************//
            PopulateAssignedLineData(bus); //********* adding lines*********************//
            //************************************************//

            return View();
        }

        // POST: Buses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusID,RegNum,Status")] Bus bus, string[] selectedDrivers, string[] selectedLines)
        {

            //******************* adding drivers **********************//
            if (selectedDrivers != null)
            {
                bus.Drivers = new List<Driver>();
                foreach (var course in selectedDrivers)
                {
                    var driverToAdd = db.Drivers.Find(int.Parse(course));
                    bus.Drivers.Add(driverToAdd);
                }
            }
            //************************************************//



            //******************* adding lines **********************//
            if (selectedLines != null)
            {
                bus.Lines = new List<Line>();
                foreach (var line in selectedLines)
                {
                    var lineToAdd = db.Lines.Find(int.Parse(line));
                    bus.Lines.Add(lineToAdd);
                }
            }
            //************************************************//


            if (ModelState.IsValid)
            {
                db.Buses.Add(bus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bus);
        }

        // GET: Buses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            //Bus bus = db.Buses.Find(id);

            //************** editing drivers and lines ********************//
            Bus bus = db.Buses
                .Include(i => i.Drivers)
                .Include(i => i.Lines) //****** for editing lines ******//
                .Where(i => i.BusID == id)
                .Single();
            PopulateAssignedDriverData(bus);
            PopulateAssignedLineData(bus); //****** for editing lines ******//
            //************************************************//


            if (bus == null)
            {
                return HttpNotFound();
            }
            return View(bus);
        }


        // POST: Buses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusID,RegNum,Status")] Bus bus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bus);
        }
         */

        //************** editing with drivers and lines ********************//

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedDrivers, string[] selectedLines)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var busToUpdate = db.Buses
               .Include(i => i.Drivers)
               .Include(i => i.Lines) //****** added for lines *******//
               .Where(i => i.BusID == id)
               .Single();



            if (TryUpdateModel(busToUpdate, "",
               new string[] { "BusID,RegNum,Status" }))
            {
                try
                {

                    UpdateBusDrivers(selectedDrivers, busToUpdate);
                    UpdateBusLines(selectedLines, busToUpdate); //****** added for lines *******//
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAssignedDriverData(busToUpdate);
            PopulateAssignedLineData(busToUpdate); //****** added for lines *******//
            return View(busToUpdate);
        }

        //************************************************//

        // GET: Buses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Bus bus = db.Buses.Find(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            return View(bus);
        }

        // POST: Buses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bus bus = db.Buses.Find(id);
            db.Buses.Remove(bus);
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


        //********************** adding drivers ******************//
        private void PopulateAssignedDriverData(Bus bus)
        {
            var allDrivers = db.Drivers;
            var busDrivers = new HashSet<int>(bus.Drivers.Select(c => c.DriverID));
            var viewModel = new List<AssignedDriverData>();
            foreach (var driver in allDrivers)
            {
                viewModel.Add(new AssignedDriverData
                {
                    DriverID = driver.DriverID,
                    DriverName = driver.DriverName,
                    DriverSurname = driver.DriverSurname,
                    Assigned = busDrivers.Contains(driver.DriverID)
                });
            }
            ViewBag.Drivers = viewModel;
        }
        //************************************************//



        //**************** editing drivers ***********************//
        private void UpdateBusDrivers(string[] selectedDrivers, Bus busToUpdate)
        {
            if (selectedDrivers == null)
            {
                busToUpdate.Drivers = new List<Driver>();
                return;
            }

            var selectedDriversHS = new HashSet<string>(selectedDrivers);
            var busDrivers = new HashSet<int>
                (busToUpdate.Drivers.Select(c => c.DriverID));
            foreach (var driver in db.Drivers)
            {
                if (selectedDriversHS.Contains(driver.DriverID.ToString()))
                {
                    if (!busDrivers.Contains(driver.DriverID))
                    {
                        busToUpdate.Drivers.Add(driver);
                    }
                }
                else
                {
                    if (busDrivers.Contains(driver.DriverID))
                    {
                        busToUpdate.Drivers.Remove(driver);
                    }
                }
            }
        }
        //************************************************//











        //********************** adding lines ******************//
        private void PopulateAssignedLineData(Bus bus)
        {
            var allLines = db.Lines;
            var busLines = new HashSet<int>(bus.Lines.Select(c => c.LineID));
            var viewModel = new List<AssignedLineData>();
            foreach (var line in allLines)
            {
                viewModel.Add(new AssignedLineData
                {
                    LineID = line.LineID,
                    Direction = line.Direction,
                    LineNumber = line.LineNumber,
                    Assigned = busLines.Contains(line.LineID)
                });
            }
            ViewBag.Lines = viewModel;
        }
        //************************************************//

        //**************** editing lines ***********************//
        private void UpdateBusLines(string[] selectedLines, Bus busToUpdate)
        {
            if (selectedLines == null)
            {
                busToUpdate.Lines = new List<Line>();
                return;
            }

            var selectedLinesHS = new HashSet<string>(selectedLines);
            var busLines = new HashSet<int>
                (busToUpdate.Lines.Select(c => c.LineID));
            foreach (var line in db.Lines)
            {
                if (selectedLinesHS.Contains(line.LineID.ToString()))
                {
                    if (!busLines.Contains(line.LineID))
                    {
                        busToUpdate.Lines.Add(line);
                    }
                }
                else
                {
                    if (busLines.Contains(line.LineID))
                    {
                        busToUpdate.Lines.Remove(line);
                    }
                }
            }
        }
        //************************************************//

        //***** Gebala zaczyna czarowac*****

        [HttpGet]
        public HttpRequestMessage GetAllBusesDetails()
        {
            IEnumerable<Bus> buses = db.Buses.ToList();
            if (buses != null)
            {
                var response = new System.Net.Http.HttpRequestMessage();
                response.CreateResponse<IEnumerable<Bus>>(HttpStatusCode.OK, buses);
                return response;
            }
            else
            {
                var response = new System.Net.Http.HttpRequestMessage();
                response.CreateResponse(HttpStatusCode.NotFound);
                return response;
            }
        }


        [HttpGet]
        public ActionResult GetBusDetails(int id)
        {
            BusForApps busToSend;

            try
            {
                Bus busik = db.Buses.Find(id);
                busToSend = new BusForApps(busik);
                if (busToSend != null)
                {
                    return Json(busToSend, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    String message = "Empty result"; // change this value to some global constant
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                String message = "Error occurred";
                return Json(message, JsonRequestBehavior.AllowGet);
                // change this value to some global constant (for easier future maintenance)
            }

        }

        [HttpPost]
        public ActionResult Post()
        {
            Bus busToAdd;
            Request.InputStream.Position = 0;
            var result = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
            busToAdd = JsonConvert.DeserializeObject<Bus>(result);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

    }
}
