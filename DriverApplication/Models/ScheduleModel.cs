using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverApplication.Models
{
    class ScheduleModel
    {
        public int ScheduleID { get; set; }
        public int BusOrder { get; set; }
        public string DepartureTime { get; set; }
        public int LineID { get; set; }
        public int StopID { get; set; }

        public virtual LineModel Line { get; set; }
        public virtual StopModel Stop { get; set; }

    }
}
