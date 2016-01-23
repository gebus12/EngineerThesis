using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EngineerCodeFirst.DAL;
using EngineerCodeFirst.ViewModel;
using System.Dynamic;
using System.Web.Script.Serialization;

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

                //set data to the objects in db
                EngineerCodeFirst.Models.Bus bus = db.Buses.Find(int.Parse(receivedData["BusID"]) );
                //omomomom musze tutaj umieć edytować!!
                }

            catch(Exception ex){

                }
            return null;

        }

    }
}