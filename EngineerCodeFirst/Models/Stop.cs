using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EngineerCodeFirst.Models
{
    public class Stop
    {

        public Stop()
        {
            this.Schedules = new HashSet<Schedule>();
        }
        [Display(Name = "ID")]
        public int StopID { get; set; }
        public string City { get; set; }
        public string StopName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Latitude { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Longitude { get; set; }
        [Display(Name = "Location")]
        public string GeoLocation
        {
            get
            {
                return Latitude + ", " + Longitude;
            }
        }

	    [Display(Name = "City: Stop")]
        public string StopInfo
        {
            get
            {
                return City + " - " + StopName;
            }
        }
    
        public virtual ICollection<Schedule> Schedules { get; set; }

    }
}