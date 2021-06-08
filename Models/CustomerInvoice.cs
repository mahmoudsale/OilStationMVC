using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models
{
    public class CustomerInvoice
    {
        public CustomerInvoice()
        {
            JournalType = 4;
        }
        [Key]
        public int Id { get; set; }
        public int BranchId { get; set; }
        public int JournalType { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer{ get; set; } 
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

        
    }
}
