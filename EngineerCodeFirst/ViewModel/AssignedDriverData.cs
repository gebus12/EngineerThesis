using EngineerCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EngineerCodeFirst.ViewModel
{
    public class AssignedDriverData
    {
        public int DriverID { get; set; }
        public string DriverName { get; set; }
        public string DriverSurname { get; set; }


        // w sumie raczej chyba niepotzebne, ale boje sie usunac
        public string DriverInfo
        {
            get
            {
                return DriverName + " " + DriverSurname;
            }
        }
        public bool Assigned { get; set; }
    }
}