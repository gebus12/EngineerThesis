using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverApplication.Models
{
    class DriverModel
    {
        public int DriverID { get; set; }
        public string DriverName { get; set; }
        public string DriverSurname { get; set; }
        // public Status? Status { get; set; }
        public string Status { get; set; }
        public string DriverLogin { get; set; }
        public string DriverPass { get; set; }

        public string DriverInfo
        {
            get
            {
                return DriverName + " " + DriverSurname;
            }
        }
        public virtual ICollection<BusModel> Buses { get; set; }
        public virtual ICollection<LineModel> Lines { get; set; }

    }
}
