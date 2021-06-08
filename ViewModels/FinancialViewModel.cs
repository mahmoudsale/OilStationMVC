using OilStationMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.ViewModels
{
    public class FinancialViewModel
    {
        [Display(Name = "Code")]
        public int Id { get; set; }

        public int BranchId { get; set; }

        [Required]
        public int JournalType { get; set; }

        [Display(Name = "Date")] 
        [Required(ErrorMessage = "Date Is Required")]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime dDate { get; set; }


        public int SubledgerTypeId { get; set; }

        [Required(ErrorMessage = "Cost Center Is Required")]
        [Display(Name = "Cost Center")]
        public int SubledgerId { get; set; }

        [Required(ErrorMessage = "Amount Is Required")]
        [Display(Name = "Amount")]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Invalid Target Price; Maximum Two Decimal Points.")]
        [Range(0, 1000000.99, ErrorMessage = "Invalid Target Price; Max 18 digits")]
        public decimal Amount { get; set; }

        [Display(Name = "Description")]
        public string Desc { get; set; }

        [Required(ErrorMessage = "Deposit No Is Required")]
        [Display(Name = "Deposit No")]
        public string DepositNo { get; set; }
        public string InnvoiceNo { get; set; }
        public bool IsReviewed { get; set; }

        public Subledger Subledger { get; set; }
        public SubledgerType SubledgerType { get; set; }

        [Required(ErrorMessage = "Custody Type Is Required")]
        [Display(Name = "Custody Type")]
        public int CustodyTypeId { get; set; }
        public CustodyType CustodyType { get; set; }

    }
}
