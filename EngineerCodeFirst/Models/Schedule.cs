using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EngineerCodeFirst.Models
{
    public class Schedule
    {
        public int ScheduleID { get; set; }
        public int BusOrder { get; set; }
        public string DepartureTime { get; set; }
        public int LineID { get; set; }
        public int StopID { get; set; }

        public virtual Line Line { get; set; }
        public virtual Stop Stop { get; set; }
    }
}