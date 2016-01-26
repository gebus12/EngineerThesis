using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EngineerCodeFirst.Models
{
    public class MsgPassenger
    {
        [Display(Name = "ID")]
        public int MsgPassengerID { get; set; }
        public int BusID { get; set; }
        public int ValidityPeriod { get; set; }
        public string Text { get; set; }
        public virtual Bus Bus { get; set; }

        /*
         * niby
         * http://weblogs.asp.net/jalpeshpvadgama/setting-default-value-for-html-editorfor-in-asp-net-mvc
         * ale nie dziala tak do konca - nie pokazuje tej daty przy create
         */
        public DateTime TimeStamp { get; set; }
        public Status? Status { get; set; }
    }

    public class MsgPassengerForApp
    {
        public int MsgPassengerID { get; set; }
        public int BusID { get; set; }
        public int ValidityPeriod { get; set; }
        public string Text { get; set; }
        public String BusNum { get; set; }
        public DateTime TimeStamp { get; set; }

        public MsgPassengerForApp(MsgPassenger x)
        {
            this.BusID = x.BusID;
            this.BusNum = x.Bus.RegNum;
            this.MsgPassengerID = x.MsgPassengerID;
            this.Text = x.Text;
            this.TimeStamp = x.TimeStamp;
            this.ValidityPeriod = x.ValidityPeriod;
        }
    }
}