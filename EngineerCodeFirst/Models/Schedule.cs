using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EngineerCodeFirst.Models
{
    public class Schedule
    {
        [Display(Name = "ID")]
        public int ScheduleID { get; set; }
        public int BusOrder { get; set; }
        public string DepartureTime { get; set; }
        public int LineID { get; set; }
        public int StopID { get; set; }

        public virtual Line Line { get; set; }
        public virtual Stop Stop { get; set; }
    }

    public class ScheduleForApp
    {
        public int ScheduleID { get; set; }
        public int BusOrder { get; set; }
        public string DepartureTime { get; set; }
        public int LineID { get; set; }
        public int StopID { get; set; }

        public int BusIdOfLine { get; set; }
        public String StatusOfLine { get; set; }

        public String typeOfSchedule { get; set; }

        public ScheduleForApp(Schedule x)
        {
            this.ScheduleID = x.ScheduleID;
            this.BusOrder = x.BusOrder;
            this.DepartureTime = x.DepartureTime;
            this.LineID = x.LineID;
            this.StopID = x.StopID;

            if (x.Line != null)
            {
                this.StatusOfLine = x.Line.Status;
                this.typeOfSchedule = x.Line.ScheduleType.ToString();
                if(x.Line.Buses.Count != 0) this.BusIdOfLine = x.Line.Buses.First().BusID;
            }
        }
    }
}