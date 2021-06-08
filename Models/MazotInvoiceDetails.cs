using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models
{
    public class MazotInvoiceDetails
    {

        //public int Id { get; set; }
        //public int MasterId{ get; set; } 
        //public int BranchId { get; set; }
        //public int JournalType { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }


        public decimal UnitPrice { get; set; }
        public decimal Qty { get; set; }
        public decimal QtyPrice { get; set; } 
        public decimal Fee { get; set; }
        public decimal TotalPrice { get; set; }

        public int AreaTransportId { get; set; }
        public AreaTransport AreaTransport { get; set; }

        public decimal AreaTransportUnit { get; set; }
        public decimal AreaTransportAmount { get; set; }



    }
}
