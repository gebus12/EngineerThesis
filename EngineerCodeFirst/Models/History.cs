using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EngineerCodeFirst.Models
{
    public class History
    {
        public int HistoryID { get; set; }
        [Required(ErrorMessage = "Time required")]
        [Display(Name = "Shift start time")]
        public string StartTime { get; set; }
        [Required(ErrorMessage = "Time required")]
        [Display(Name = "Shift end time")]
        public string EndTime { get; set; }
        [Required(ErrorMessage = "Driver required")]
        [Display(Name = "Driver")]
        public int DriverID { get; set; }
        [Required(ErrorMessage = "Bus required")]
        [Display(Name = "Bus")]
        public int BusID { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual Bus Bus { get; set; }
    }
}