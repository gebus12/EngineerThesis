using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EngineerCodeFirst.DAL;
using EngineerCodeFirst.ViewModel;

namespace EngineerCodeFirst.Controllers
{
    public class HomeController : Controller
    {
        private TransportPublicContext db = new TransportPublicContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";
            
            return View();
        }

        public ActionResult Statistic()
        {
            //ViewBag.Message = "Your contact page.";

            //string query = "SELECT b.RegNum, CONCAT(d.DriverName, ' ', d.DriverSurname) AS Driver FROM Buses b JOIN BusDriver bd ON b.BusID = bd.BusID JOIN Drivers d ON d.DriverID = bd.DriverID WHERE b.Status = 'ON' AND d.Status = 'ON';";

            string query = "SELECT b.RegNum, CONCAT(d.DriverName, ' ', d.DriverSurname) AS Driver, CONCAT(l.LineNumber, ': ', l.Direction) AS Line, b.Latitude, b.Longitude FROM Buses b JOIN BusDriver bd ON b.BusID = bd.BusID JOIN Drivers d ON d.DriverID = bd.DriverID JOIN BusLine bl ON bl.BusID = b.BusID JOIN Lines l ON l.LineID = bl.LineID WHERE b.Status = 'ON' AND d.Status = 'ON';";
            
            IEnumerable<BusesOnTour> data = db.Database.SqlQuery<BusesOnTour>(query);

            return View(data.ToList());
        }
    }
}