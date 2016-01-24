using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EngineerCodeFirst.DAL;
using EngineerCodeFirst.ViewModel;
using System.Dynamic;
using System.Web.Script.Serialization;
using EngineerCodeFirst.Models;

namespace EngineerCodeFirst.Controllers
{
    public class HomeController : Controller
    {
        private TransportPublicContext db = new TransportPublicContext();
        public ActionResult Index()
        {
            dynamic mymodel = new ExpandoObject();

            string query = "SELECT b.RegNum, CONCAT(d.DriverName, ' ', d.DriverSurname) AS Driver, CONCAT(l.LineNumber, ': ', l.Direction) AS Line, b.Latitude, b.Longitude FROM Buses b JOIN BusDriver bd ON b.BusID = bd.BusID JOIN Drivers d ON d.DriverID = bd.DriverID JOIN BusLine bl ON bl.BusID = b.BusID JOIN Lines l ON l.LineID = bl.LineID WHERE b.Status = 'ON' AND d.Status = 'ON';";
            IEnumerable<BusesOnTour> data = db.Database.SqlQuery<BusesOnTour>(query);

            string query2 = "SELECT msg.MsgDriverID, CONCAT(d.DriverName, ' ', d.DriverSurname) AS Driver, msg.Text, msg.TimeStamp FROM Drivers d JOIN MsgDrivers msg ON d.DriverID = msg.DriverID WHERE msg.Status = 1 AND msg.Receiver = 1";
            IEnumerable<DriversNotifications> data2 = db.Database.SqlQuery<DriversNotifications>(query2);

            mymodel.BusesOnTour = db.Database.SqlQuery<BusesOnTour>(query); ;
            mymodel.DriversNotifications = db.Database.SqlQuery<DriversNotifications>(query2);

            return View(mymodel);
            //return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";
            
            return View();
        }

        [HttpPost]
        public ActionResult StartRideFromApp()
        {
            Dictionary<String, String> receivedData = new Dictionary<String, String>();
            try{
                Request.InputStream.Position = 0;
                var jsonString = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();

                receivedData = js.Deserialize<Dictionary<String, String>>(jsonString);

                //get objects from db which correspond to the request from application
                EngineerCodeFirst.Models.Bus bus = db.Buses.Find(int.Parse(receivedData["BusID"]) );
                EngineerCodeFirst.Models.Line line = db.Lines.Find(int.Parse(receivedData["LineID"]));
                EngineerCodeFirst.Models.Driver driver = db.Drivers.Find(int.Parse(receivedData["DriverID"]));

                //set status to ON for driver, bus and line
                bus.Status = "ON";
                driver.Status = "ON";
                line.Status = "ON";

                //assign bus to line, driver to bus ect.
                line.Buses.Clear();
                line.Buses.Add(bus);

                bus.Drivers.Clear();
                bus.Drivers.Add(driver);

                bus.Lines.Clear();
                bus.Lines.Add(line);

                driver.Buses.Clear();
                driver.Buses.Add(bus);

                //finalize
                db.SaveChanges();

                String message = "DONE"; // change this value to some global constant
                return Json(message, JsonRequestBehavior.AllowGet);
                }

            catch(Exception ex){
                // finding elements in db failed or received data is invalid
                String message = "FAIL"; // change this value to some global constant
                return Json(message, JsonRequestBehavior.AllowGet);
                }
        }

        public ActionResult StopRideFromApp()
        {
            Dictionary<String, String> receivedData = new Dictionary<String, String>();
            try
            {
                Request.InputStream.Position = 0;
                var jsonString = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();

                receivedData = js.Deserialize<Dictionary<String, String>>(jsonString);

                //get objects from db which correspond to the request from application
                Bus bus = db.Buses.Find(int.Parse(receivedData["BusID"]));
                Line line = db.Lines.Find(int.Parse(receivedData["LineID"]));
                Driver driver = db.Drivers.Find(int.Parse(receivedData["DriverID"]));


                History historyEvent = new HistoriesController();
                historyEvent.BusID = bus.BusID;
                historyEvent.DriverID = driver.DriverID;
                //historyEvent.StartTime = receivedData["HistoryStart"];
                //historyEvent.StopTime = receivedData["HistoryStop"];

                //set status to OFF for driver, bus and line, clear coordinates
                bus.Status = "OFF";
                driver.Status = "OFF";
                line.Status = "OFF";
                bus.Latitude = null;
                bus.Longitude = null;

                //assign bus to line, driver to bus ect.
                line.Buses.Clear();
                bus.Drivers.Clear();
                bus.Lines.Clear();
                driver.Buses.Clear();

                //finalize
                db.SaveChanges();

                String message = "DONE"; // change this value to some global constant
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                // finding elements in db failed or received data is invalid
                String message = "FAIL"; // change this value to some global constant
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult DriverLogoutFromApp()
        {
            int receivedID;
            try
            {
                Request.InputStream.Position = 0;
                var jsonString = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();

                receivedID = js.Deserialize<int>(jsonString);

                //get objects from db which correspond to the request from application

                EngineerCodeFirst.Models.Driver driver = db.Drivers.Find(receivedID);
                try
                {
                    EngineerCodeFirst.Models.Bus bus = driver.Buses.First();
                    EngineerCodeFirst.Models.Line line = bus.Lines.First();
                    bus.Status = "OFF";
                    bus.Longitude = null;
                    bus.Latitude = null;
                    line.Status = "OFF";

                    line.Buses.Clear();
                    bus.Drivers.Clear();
                    bus.Lines.Clear();
                }
                catch (Exception ex)
                {
                    // line and/or bus not assigned to driver
                    // ignore and update status only of driver
                }
               driver.Status = "OFF"; 
               driver.Buses.Clear();

               db.SaveChanges();

               String message = "DONE"; // change this value to some global constant
               return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // finding elements in db failed or received data is invalid
                String message = "FAIL"; // change this value to some global constant
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }

    }
}