using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EngineerCodeFirst.Models
{
    public enum ScheduleType
    {
        REGULAR, WEEKDAY, HOLIDAY
    }
    public class Line
    {
        public Line()
        {
            this.Schedules = new HashSet<Schedule>();
            this.Buses = new HashSet<Bus>();
            //this.Drivers = new HashSet<Driver>();
        }
        [Display(Name = "ID")]
        public int LineID { get; set; }
        [Display(Name = "Line Number")]
        public int LineNumber { get; set; }
        [Display(Name = "Direction")]
        public string Direction { get; set; }
        public string Status { get; set; }
        [Display(Name = "Schedule Type")]
        public ScheduleType? ScheduleType { get; set; }

        [Display(Name = "Line: Direction (Type)")]
        public string LineInfo
        {
            get
            {
                return LineNumber + ": " + Direction + " (" + ScheduleType + ")";
            }
        }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<Bus> Buses { get; set; }
        //public virtual ICollection<Driver> Drivers { get; set; }
    }

    public class LineForApps
    {

        public LineForApps(Line lineToBeTransfered)
        {
            this.LineID = lineToBeTransfered.LineID;
            this.LineNumber = lineToBeTransfered.LineNumber;
            this.Direction = lineToBeTransfered.Direction;
            this.Status = lineToBeTransfered.Status;
        }

        public LineForApps()
        {
        }

        public int LineID { get; set; }
        public int LineNumber { get; set; }
        public string Direction { get; set; }
        public string Status { get; set; }

    }
}