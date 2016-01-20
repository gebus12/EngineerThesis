using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EngineerCodeFirst.Models
{
    public class History
    {
        public int HistoryID { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public int DriverID { get; set; }
        public virtual Driver Driver { get; set; }
    }
}