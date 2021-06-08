using OilStationMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.ViewModels
{
    public class MazotInvoiceViewModel
    {
        //public int Id { get; set; }
        //public int BranchId { get; set; }
        //public int JournalType { get; set; }

        //[Required(ErrorMessage = "Innvoice No Is Required")]
        //[Display(Name = "Innvoice No")]
        //public int InnvoiceNo { get; set; }

        //[Required(ErrorMessage = "Date Is Required")]
        //[Display(Name = "Date")]
        //public DateTime dDate { get; set; }

        //[Required(ErrorMessage = "Company Is Required")]
        //[Display(Name = "Company")]
        //public int CompanyId { get; set; }

        //[Required(ErrorMessage = "Charge Area Is Required")]
        //[Display(Name = "Charge Area")]
        //public int ChargeAreaId { get; set; }

        //[Required(ErrorMessage = "Charge Customer Is Required")]
        //[Display(Name = "Charge Customer")]
        //public int ChargeCustomerId { get; set; }

        //[Required(ErrorMessage = "Car Is Required")]
        //[Display(Name = "Car")]
        //public int CarId { get; set; }

        //[Required(ErrorMessage = "Driver Is Required")]
        //[Display(Name = "Driver")]
        //public int DriverId { get; set; }

        //[Required(ErrorMessage = "Unit Price Is Required")]
        //[Display(Name = "Unit Price")]
        //public decimal UnitPrice { get; set; }

        //[Required(ErrorMessage = "Qty Is Required")]
        //[Display(Name = "Qty")]
        //public decimal Qty { get; set; }

        //[Required(ErrorMessage = "Qty Price Is Required")]
        //[Display(Name = "Qty Price")]
        //public decimal QtyPrice { get; set; }

        //[Display(Name = "Tax")]
        //public decimal Tax { get; set; }

        //[Display(Name = "Fee")]
        //public decimal Fee { get; set; } 

        //[Required(ErrorMessage = "Total Price Is Required")]
        //[Display(Name = "Total Price")]
        //public decimal TotalPrice { get; set; }

        //[Display(Name = "Description")]
        //public string Desc { get; set; }

        //public Company Company { get; set; }
        //public ChargeArea ChargeArea { get; set; }
        //public Customer Customer { get; set; }
        //public Car Car { get; set; }
        //public Driver Driver { get; set; }
        //public Customer ChargeCustomer { get; set; }

        //[Required(ErrorMessage = "Customer Is Required")]
        //[Display(Name = "Customer")]
        //public int CustomerId { get; set; }

        //[Required(ErrorMessage = "Customer Unit Price Is Required")]
        //[Display(Name = "Customer Unit Price")]
        //public decimal CustomerUnitPrice { get; set; }

        //[Required(ErrorMessage = "Customer Qty Is Required")]
        //[Display(Name = "Customer Qty")]
        //public decimal CustomerQty { get; set; }

        //[Required(ErrorMessage = "Customer Qty Price Is Required")]
        //[Display(Name = "Customer Qty Price")]
        //public decimal CustomerQtyPrice { get; set; }

        //[Display(Name = "Customer Fee")]
        //public decimal CustomerFee { get; set; }

        //[Required(ErrorMessage = "Customer Total Price Is Required")]
        //[Display(Name = "Customer Total Price")]
        //public decimal CustomerTotalPrice { get; set; }

        //[Required(ErrorMessage = "AreaTransport Is Required")]
        //[Display(Name = "AreaTransport")]
        //public int AreaTransportId { get; set; }
        //public AreaTransport AreaTransport { get; set; }

        //[Required(ErrorMessage = "AreaTransport Unit Is Required")]
        //[Display(Name = "AreaTransport Unit")]
        //public decimal AreaTransportUnit { get; set; }

        //[Required(ErrorMessage = "AreaTransport Amount Is Required")]
        //[Display(Name = "AreaTransport Amount")]
        //public decimal AreaTransportAmount { get; set; }


        public int Id { get; set; }
        public int BranchId { get; set; }
        public int JournalType { get; set; }

        [Required(ErrorMessage = "Innvoice No Is Required")]
        [Display(Name = "Innvoice No")]
        public int InnvoiceNo { get; set; }

        [Required(ErrorMessage = "Date Is Required")]
        [Display(Name = "Date")]
        public DateTime dDate { get; set; }

        [Required(ErrorMessage = "Company Is Required")]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Charge Area Is Required")]
        [Display(Name = "Charge Area")]
        public int ChargeAreaId { get; set; }

        [Required(ErrorMessage = "Charge Customer Is Required")]
        [Display(Name = "Charge Customer")]
        public int ChargeCustomerId { get; set; }

        [Required(ErrorMessage = "Car Is Required")]
        [Display(Name = "Car")]
        public int CarId { get; set; }

        [Required(ErrorMessage = "Driver Is Required")]
        [Display(Name = "Driver")]
        public int DriverId { get; set; }

        [Required(ErrorMessage = "Unit Price Is Required")]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Qty Is Required")]
        [Display(Name = "Qty")]
        public decimal Qty { get; set; }

        [Required(ErrorMessage = "Qty Price Is Required")]
        [Display(Name = "Qty Price")]
        public decimal QtyPrice { get; set; }

        [Display(Name = "Tax")]
        public decimal Tax { get; set; }

        [Display(Name = "Fee")]
        public decimal Fee { get; set; }

        [Required(ErrorMessage = "Total Price Is Required")]
        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Description")]
        public string Desc { get; set; }

        public Company Company { get; set; }
        public ChargeArea ChargeArea { get; set; }
        public Customer Customer { get; set; }
        public Car Car { get; set; }
        public Driver Driver { get; set; }
        public Customer ChargeCustomer { get; set; }

        [Required(ErrorMessage = "Customer Is Required")]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Customer Unit Price Is Required")]
        [Display(Name = "Customer Unit Price")]
        public decimal CustomerUnitPrice { get; set; }

        [Required(ErrorMessage = "Customer Qty Is Required")]
        [Display(Name = "Customer Qty")]
        public decimal CustomerQty { get; set; }

        [Required(ErrorMessage = "Customer Qty Price Is Required")]
        [Display(Name = "Customer Qty Price")]
        public decimal CustomerQtyPrice { get; set; }

        [Display(Name = "Customer Fee")]
        public decimal CustomerFee { get; set; }

        [Required(ErrorMessage = "Customer Total Price Is Required")]
        [Display(Name = "Customer Total Price")]
        public decimal CustomerTotalPrice { get; set; }

        [Required(ErrorMessage = "AreaTransport Is Required")]
        [Display(Name = "AreaTransport")]
        public int AreaTransportId { get; set; }
        public AreaTransport AreaTransport { get; set; }

        [Required(ErrorMessage = "AreaTransport Unit Is Required")]
        [Display(Name = "AreaTransport Unit")]
        public decimal AreaTransportUnit { get; set; }

        [Required(ErrorMessage = "AreaTransport Amount Is Required")]
        [Display(Name = "AreaTransport Amount")]
        public decimal AreaTransportAmount { get; set; }



    }
}
