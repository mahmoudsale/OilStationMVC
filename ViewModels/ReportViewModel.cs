using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.ViewModels
{
    public class ReportViewModel
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int SubledgerTypeId { get; set; }
        public int FromSubledgerId { get; set; }
        public int ToSubledgerId { get; set; } 
        public string  ReportName { get; set; }

    }
}
