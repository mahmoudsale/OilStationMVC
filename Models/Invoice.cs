 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models
{
    public class Invoice
    {
        public Invoice()
        { 
            JournalType = 12;
        }
        [Key]
        public int Id { get; set; }
        public int BranchId { get; set; } 
        public int JournalType { get; set; }
        public int InnvoiceNo { get; set; }
        public DateTime dDate { get; set; } 
        public int SolarQTY { get; set; }
        public int Oil80QTY { get; set; }
        public int Oil92QTY { get; set; }
        public int Oil95QTY { get; set; }
        public decimal SolarUnitPrice { get; set; }
        public decimal Oil80UnitPrice { get; set; }
        public decimal Oil92UnitPrice { get; set; }
        public decimal Oil95UnitPrice { get; set; }
        public decimal SolarTotalPrice { get; set; }
        public decimal Oil80TotalPrice { get; set; }
        public decimal Oil92TotalPrice { get; set; }
        public decimal Oil95TotalPrice { get; set; }
        public decimal TotalPrice { get; set; }
       
        public string Desc { get; set; }

        public int CompanyId { get; set; }
        public int StockId { get; set; }
        public int StationId { get; set; }
        public int CarId { get; set; }
        public int DriverId { get; set; }

        public decimal StockTransportUnit { get; set; }
         public decimal StockTransportAmount { get; set; }

        

        public Company Company { get; set; }
        public Stock Stock { get; set; }
        public Station Station { get; set; }
        public Car Car { get; set; }
        public Driver Driver { get; set; }

    }
}
