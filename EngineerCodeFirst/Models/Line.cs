using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EngineerCodeFirst.Models
{
    public class Line
    {
        public Line()
        {
            this.Schedules = new HashSet<Schedule>();
            this.Buses = new HashSet<Bus>();
            this.Drivers = new HashSet<Driver>();
        }
        [Display(Name = "ID")]
        public int LineID { get; set; }
        public int LineNumber { get; set; }
        public string Direction { get; set; }

        [Display(Name = "Line: Direction")]
        public string LineInfo
        {
            get
            {
                return LineNumber + ": " + Direction;
            }
        }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<Bus> Buses { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
    }
}