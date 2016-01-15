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
    public class MsgDriver
    {
        [Display(Name = "ID")]
        public int MsgDriverID { get; set; }
        public int DriverID { get; set; } 
        public int BusID { get; set; }
        public string Text { get; set; }

        /*
         * POBIERANIE AKTUALNEJ DATY przy tworzeniu obiektu:
         * http://weblogs.asp.net/jalpeshpvadgama/setting-default-value-for-html-editorfor-in-asp-net-mvc
         * ale u mnie nie dziala tak do konca - nie pokazuje tej daty przy create w View
         * moze Tobie sie uda :P
         */
        public DateTime TimeStamp { get; set; }
        public Status? Status { get; set; }
        
    }
}