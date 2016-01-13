using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EngineerCodeFirst.Models
{
    public enum Status
        {
            Read, Unread
        }
    public class Message
    {
        public int MessageID { get; set; }
        public int DriverID { get; set; } 
        public int BusID { get; set; }
        public string Text { get; set; }

        /*
         * niby
         * http://weblogs.asp.net/jalpeshpvadgama/setting-default-value-for-html-editorfor-in-asp-net-mvc
         * ale nie dziala tak do konca - nie pokazuje tej daty przy create
         */
        public DateTime TimeStamp { get; set; }
        public Status? Status { get; set; }
        
    }
}