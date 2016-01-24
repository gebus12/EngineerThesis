using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EngineerCodeFirst.Models
{
    public enum Status
        {
            Read, Unread
        }

    public enum Receiver
    {
        Driver, Provider
    }
    public class MsgDriver
    {
        [Display(Name = "ID")]
        public int MsgDriverID { get; set; }
        public int DriverID { get; set; } 
        public int BusID { get; set; }
        public string Text { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual Bus Bus { get; set; }

        /*
         * POBIERANIE AKTUALNEJ DATY przy tworzeniu obiektu:
         * http://weblogs.asp.net/jalpeshpvadgama/setting-default-value-for-html-editorfor-in-asp-net-mvc
         * ale u mnie nie dziala tak do konca - nie pokazuje tej daty przy create w View
         * moze Tobie sie uda/przyda :P
         */
        //[DataType(DataType.Date)]
        public DateTime TimeStamp { get; set; }
        public Status? Status { get; set; }
        public Receiver? Receiver { get; set; }
        
    }

    public class MsgDriverForApp
    {
        public int MsgDriverID { get; set; }
        public int DriverID { get; set; }
        public int BusID { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }

        public MsgDriverForApp(MsgDriver x){
            this.BusID = x.BusID;
            this.DriverID = x.DriverID;
            this.MsgDriverID = x.MsgDriverID;
            this.Text = x.Text;
            this.TimeStamp = x.TimeStamp;
        }
    }
}