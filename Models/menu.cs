using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models
{
    public class menu
    {
        internal int nModuleId;

        public int id { get; set; }
        public int parent { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public bool IsForm { get; set; }
        public int ModuleId { get; set; } 
        public string FormName { get; set; }
        public string FormType { get; set; }
        public string FormParameter { get; internal set; }
        public string TableName { get; set; }
        public List<menu> LstChild { get; set; }
        public string PageUrl { get; set; } 
        public string Controller { get; set; }
        public string Action { get; set; }
        public string MainReportName { get; set; }
    }
}
