using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EngineerCodeFirst.Models
{
    public class Driver
    {
        
        public Driver()
        {
            this.Buses = new HashSet<Bus>();
            this.Lines = new HashSet<Line>();
        }
        [Display(Name = "ID")]
        public int DriverID { get; set; }
        [Display(Name = "Name")]
        public string DriverName { get; set; }
        [Display(Name = "Surname")]
        public string DriverSurname { get; set; }
       // public Status? Status { get; set; }
        public string Status { get; set; }
        [Display(Name = "Login")]
        public string DriverLogin { get; set; }
        [Display(Name = "Pass")]
        public string DriverPass { get; set; }

        [Display(Name = "Driver")]
        public string DriverInfo
        {
            get
            {
                return DriverName + " " + DriverSurname;
            }
        }
        public virtual ICollection<Bus> Buses { get; set; }
        public virtual ICollection<Line> Lines { get; set; }
    }

    public class DriverForApp
    {
        //small comment
        public int DriverID { get; set; }
        public string DriverName { get; set; }
        public string DriverSurname { get; set; }
        public string Status { get; set; }
        public string DriverLogin { get; set; }
        public string DriverPass { get; set; }


        public DriverForApp(){}

        public DriverForApp(Driver driverToBeTransfered)
        {
            this.DriverID = driverToBeTransfered.DriverID;
            this.DriverName = driverToBeTransfered.DriverName;
            this.DriverSurname = driverToBeTransfered.DriverSurname;
            this.Status = driverToBeTransfered.Status;
            this.DriverLogin = driverToBeTransfered.DriverLogin;
            this.DriverPass = driverToBeTransfered.DriverPass;
        }
    }
}