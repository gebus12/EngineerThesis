using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace EngineerCodeFirst.Models
{
    //public enum Status
    //{
    //    ON, OFF
    //}

    public enum Accessible
    {
        YES, NO
    }
    public class Bus
    {
        
        public Bus()
        {
            this.Lines = new HashSet<Line>();
            this.Drivers = new HashSet<Driver>();
            this.Histories = new HashSet<History>();
            this.MsgDrivers = new HashSet<MsgDriver>();
            this.MsgPassengers = new HashSet<MsgPassenger>();
        }
        [Display(Name = "ID")]
        public int BusID { get; set; }
        [Display(Name = "Registration ID")]
        public string RegNum { get; set; }
        public string Status { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        [Display(Name = "Location")]
        public string GeoLocation
        {
            get
            {
                return Latitude + ", " + Longitude;
            }
        }
        [Display(Name = "Wheelchair accessibility")]
        public Accessible? Accessible { get; set; }
        public virtual ICollection<Line> Lines { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
        public virtual ICollection<History> Histories { get; set; }
        public virtual ICollection<MsgPassenger> MsgPassengers { get; set; }
        public virtual ICollection<MsgDriver> MsgDrivers { get; set; }
    }

    public class BusForApps {

        public BusForApps(Bus busToBeTransfered)
        {
            this.BusID = busToBeTransfered.BusID;
            this.RegNum = busToBeTransfered.RegNum;
            this.Status = busToBeTransfered.Status;
            this.Latitude = busToBeTransfered.Latitude;
            this.Longitude = busToBeTransfered.Longitude;
            //obsluzyc driversow i linie
        }

        public BusForApps()
        {
        }

        public int BusID { get; set; }
        public string RegNum { get; set; }
        public string Status { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    
    }

}