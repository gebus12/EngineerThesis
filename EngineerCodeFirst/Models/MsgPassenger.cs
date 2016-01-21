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
}