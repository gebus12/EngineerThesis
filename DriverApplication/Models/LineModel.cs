using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverApplication.Models
{
    class LineModel
    {
        public int LineID { get; set; }
        public int LineNumber { get; set; }
        public string Direction { get; set; }

        public string LineInfo
        {
            get
            {
                return LineNumber + ": " + Direction;
            }
        }
        public virtual ICollection<ScheduleModel> Schedules { get; set; }
        public virtual ICollection<BusModel> Buses { get; set; }
        public virtual ICollection<DriverModel> Drivers { get; set; }


    }
}
