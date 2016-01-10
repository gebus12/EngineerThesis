using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverApplication.Models
{
    class StopModel
    {
        public int StopID { get; set; }
        public string City { get; set; }
        public string StopName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public string GeoLocation
        {
            get
            {
                return Latitude + ", " + Longitude;
            }
        }
        public string StopInfo
        {
            get
            {
                return City + " - " + StopName;
            }
        }

        public virtual ICollection<ScheduleModel> Schedules { get; set; }

    }
}
