using EngineerCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EngineerCodeFirst.ViewModel
{
    public class AssignedLineData
    {
        public int LineID { get; set; }
        public int LineNumber { get; set; }
        public string Direction { get; set; }

        public string LineInfo
        {
            get
            {
                return LineNumber + ": " + Direction;
            }
        }
        public bool Assigned { get; set; }
    }
}