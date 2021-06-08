using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models
{
    public class Financial
    { 
        public int Id { get; set; } 
        public int BranchId { get; set; } 
        public int JournalType { get; set; }
        public DateTime dDate { get; set; } 
        public int  SubledgerTypeId{ get; set; }
        public int  SubledgerId{ get; set; }  
        public decimal Amount { get; set; }  
        public string  Desc { get; set; } 
        public string  DepositNo { get; set; }  
        public string InnvoiceNo { get; set; }    
        public bool IsReviewed { get; set; }
        public int CustodyTypeId { get; set; }
    }
}
