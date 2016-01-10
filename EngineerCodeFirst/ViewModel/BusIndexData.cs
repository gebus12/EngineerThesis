using EngineerCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EngineerCodeFirst.ViewModel
{
    public class BusIndexData
    {
        public IEnumerable<Bus> Buses { get; set; }
        public IEnumerable<Driver> Drivers { get; set; }
        public IEnumerable<Line> Lines { get; set; }
    }
}