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
    public class DriversController : Controller
    {
        private TransportPublicContext db = new TransportPublicContext();

        /*
        // GET: Drivers
        public ActionResult Index()
        {
            return View(db.Drivers.ToList());
        }
        */

        public ViewResult Index(string searchString)
        {
            var drivers = from d in db.Drivers select d;
            if (!String.IsNullOrEmpty(searchString))
            {
                drivers = drivers.Where(d =>
                    d.DriverName.ToUpper().Contains(searchString.ToUpper())
                    ||
                    d.DriverSurname.ToUpper().Contains(searchString.ToUpper())
                    ||
                    d.DriverLogin.ToUpper().Contains(searchString.ToUpper())
                    );
            }

            return View(drivers.ToList());
        }

        // GET: Drivers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        // GET: Drivers/Create
        public ActionResult Create()
        {
            ////***************** adding lines ******************//
            //var driver = new Driver();

            //driver.Lines = new List<Line>();
            //PopulateAssignedLineData(driver);
            ////************************************************//

            return View();
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DriverID,DriverName,DriverSurname,Status,DriverLogin,DriverPass")] Driver driver)
        {
            ////******************* adding lines **********************//
            //if (selectedLines != null)
            //{
            //    driver.Lines = new List<Line>();
            //    foreach (var line in selectedLines)
            //    {
            //        var lineToAdd = db.Lines.Find(int.Parse(line));
            //        driver.Lines.Add(lineToAdd);
            //    }
            //}
            ////************************************************//



            if (ModelState.IsValid)
            {
                db.Drivers.Add(driver);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ////************************************************//
            //PopulateAssignedLineData(driver);
            ////************************************************//

            return View(driver);
        }

        // GET: Drivers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Driver driver = db.Drivers.Find(id);

            ////************** editing lines ********************//
            //Driver driver = db.Drivers
            //    .Include(i => i.Lines)
            //    .Where(i => i.DriverID == id)
            //    .Single();
            //PopulateAssignedLineData(driver);
            ////************************************************//



            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DriverID,DriverName,DriverSurname,Status,DriverLogin,DriverPass")] Driver driver)
        {
            if (ModelState.IsValid)
            {
                db.Entry(driver).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(driver);
        }
        

        
        //************** editing with drivers and lines ********************//
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedLines)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var driverToUpdate = db.Drivers
               .Include(i => i.Lines)
               .Where(i => i.DriverID == id)
               .Single();

            if (TryUpdateModel(driverToUpdate, "",
               new string[] { "DriverID","DriverName","DriverSurname","Status","DriverLogin","DriverPass" }))
            {
                try
                {
                    UpdateDriverLines(selectedLines, driverToUpdate); // added for lines
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException ) // dex
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAssignedLineData(driverToUpdate); //****** added for lines
            return View(driverToUpdate);
        }
         */

        //************************************************//


        // GET: Drivers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Driver driver = db.Drivers.Find(id);
            db.Drivers.Remove(driver);
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




        ////********************** adding lines ******************//
        //private void PopulateAssignedLineData(Driver driver)
        //{
        //    var allLines = db.Lines;
        //    var busLines = new HashSet<int>(driver.Lines.Select(c => c.LineID));
        //    var viewModel = new List<AssignedLineData>();
        //    foreach (var line in allLines)
        //    {
        //        viewModel.Add(new AssignedLineData
        //        {
        //            LineID = line.LineID,
        //            Direction = line.Direction,
        //            LineNumber = line.LineNumber,
        //            Assigned = busLines.Contains(line.LineID)
        //        });
        //    }
        //    ViewBag.Lines = viewModel;
        //}
        ////************************************************//

        ////**************** editing lines ***********************//
        //private void UpdateDriverLines(string[] selectedLines, Driver driverToUpdate)
        //{
        //    if (selectedLines == null)
        //    {
        //        driverToUpdate.Lines = new List<Line>();
        //        return;
        //    }

        //    var selectedLinesHS = new HashSet<string>(selectedLines);
        //    var busLines = new HashSet<int>
        //        (driverToUpdate.Lines.Select(c => c.LineID));
        //    foreach (var line in db.Lines)
        //    {
        //        if (selectedLinesHS.Contains(line.LineID.ToString()))
        //        {
        //            if (!busLines.Contains(line.LineID))
        //            {
        //                driverToUpdate.Lines.Add(line);
        //            }
        //        }
        //        else
        //        {
        //            if (busLines.Contains(line.LineID))
        //            {
        //                driverToUpdate.Lines.Remove(line);
        //            }
        //        }
        //    }
        //}
        ////************************************************//
        //***Methods For App support***////
        //[HttpGet]
        public ActionResult LoginDriver()
        {
            DriverForApp dataFromApp;

            //create mock Driver with credentials send from app
            Request.InputStream.Position = 0;
            var result = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
            dataFromApp = JsonConvert.DeserializeObject<DriverForApp>(result);

            IEnumerable<Driver> serverDrivers = db.Drivers.ToList(); //get all drivers from server
            if (serverDrivers != null)
            {
                foreach (Driver driv in serverDrivers) //compare credentials from application with database
                {
                    if (driv.DriverLogin == dataFromApp.DriverLogin && driv.DriverPass == dataFromApp.DriverPass)
                    {
                        //credentials found, return the Driver object but remove password firstly (we dont want to send it via Internet)

                        dataFromApp = new DriverForApp(driv); // create Driver object which application may recognize. It is a copy of driver from server
                        dataFromApp.DriverPass = null; //hide the password
                        return Json(dataFromApp, JsonRequestBehavior.AllowGet); //send driver to app
                    }
                }
            }
            //if not found, login failed
            String message = "FAIL"; // change this value to some global constant
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAllDrivers()
        {

            IEnumerable<Driver> drivers = db.Drivers.ToList();
            if (drivers != null)
            {
                return Json(drivers, JsonRequestBehavior.AllowGet);
            }
            else
            {

                String message = "Empty result"; // change this value to some global constant
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult GetDriver(int id)
        {
            try
            {
                //Bus busik = db.Buses.Find(id);
                Driver driv = db.Drivers.Find(id);
                DriverForApp driverToSend = new DriverForApp(driv);
                if (driverToSend != null)
                {
                    //wybitnie niebezpieczne rozwiązanie
                    return Json(driverToSend, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    String message = "Empty result"; // change this value to some global constant
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                String message = "Error occurred";
                return Json(message, JsonRequestBehavior.AllowGet);
                // change this value to some global constant (for easier future maintenance)
            }

        }

        [HttpPost]
        public ActionResult Post()
        {
            return null;
            //Bus busToAdd;
            //Request.InputStream.Position = 0;
            //var result = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
            //busToAdd = JsonConvert.DeserializeObject<Bus>(result);
            //return Json("OK", JsonRequestBehavior.AllowGet);
        }

    }



}

