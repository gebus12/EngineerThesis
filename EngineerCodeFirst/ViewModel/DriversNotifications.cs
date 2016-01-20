using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EngineerCodeFirst.ViewModel
{
    public class DriversNotifications
    {
        public int MsgDriverID { get; set; }
        public string Driver { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}