using OilStationMVC.Views.Shared.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models
{
    public class MazotInvoice
    {  
        public int Id { get; set; }
        public int BranchId { get; set; }
        public int JournalType { get; set; }

        
        [Required(ErrorMessageResourceName = "Innvoice_No_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Innvoice_No", ResourceType = typeof(ModelResources))]
        public int InnvoiceNo { get; set; }

 
        [Required(ErrorMessageResourceName = "Date_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Date", ResourceType = typeof(ModelResources))]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime dDate { get; set; }

        [Required(ErrorMessageResourceName = "Company_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Company", ResourceType = typeof(ModelResources))]
        public int CompanyId { get; set; }

 
        [Required(ErrorMessageResourceName = "Charge_Area_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Charge_Area", ResourceType = typeof(ModelResources))]
        public int ChargeAreaId { get; set; }
 
        [Required(ErrorMessageResourceName = "Charge_Customer_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Charge_Customer", ResourceType = typeof(ModelResources))]
        public int ChargeCustomerId { get; set; }

 
        [Required(ErrorMessageResourceName = "Car_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Car", ResourceType = typeof(ModelResources))]
        public int CarId { get; set; }

 
        [Required(ErrorMessageResourceName = "Driver_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Driver", ResourceType = typeof(ModelResources))]
        public int DriverId { get; set; }

 
        [Required(ErrorMessageResourceName = "Unit_Price_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Unit_Price", ResourceType = typeof(ModelResources))]
        public decimal UnitPrice { get; set; }

 
        [Required(ErrorMessageResourceName = "Qty_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Qty", ResourceType = typeof(ModelResources))]
        public decimal Qty { get; set; }

 
        [Required(ErrorMessageResourceName = "Qty_Price_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Qty_Price", ResourceType = typeof(ModelResources))]
        public decimal QtyPrice { get; set; }

  
        [Display(Name = "Tax", ResourceType = typeof(ModelResources))]
        public decimal Tax { get; set; }

 
        [Display(Name = "Fee", ResourceType = typeof(ModelResources))]
        public decimal Fee { get; set; }

 
        [Required(ErrorMessageResourceName = "Total_Price_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Total_Price", ResourceType = typeof(ModelResources))]
        public decimal TotalPrice { get; set; }

 
        [Display(Name = "Description", ResourceType = typeof(ModelResources))]
        public string Desc { get; set; }

        public Company Company { get; set; }
        public ChargeArea ChargeArea { get; set; }
        public Customer Customer { get; set; }
        public Car Car { get; set; }
        public Driver Driver { get; set; }
        public Customer ChargeCustomer { get; set; }


        //[Required(ErrorMessageResourceName = "Customer_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        //[Display(Name = "Customer", ResourceType = typeof(ModelResources))]
        //public int CustomerId { get; set; }


        //[Required(ErrorMessageResourceName = "Customer_Unit_Price_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        //[Display(Name = "Customer_Unit_Price", ResourceType = typeof(ModelResources))]
        //public decimal CustomerUnitPrice { get; set; }


        //[Required(ErrorMessageResourceName = "Customer_Qty_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        //[Display(Name = "Customer_Qty", ResourceType = typeof(ModelResources))]
        //public decimal CustomerQty { get; set; }


        //[Required(ErrorMessageResourceName = "Customer_Qty_Price_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        //[Display(Name = "Customer_Qty_Price", ResourceType = typeof(ModelResources))]
        //public decimal CustomerQtyPrice { get; set; }


        //[Display(Name = "Customer_Fee", ResourceType = typeof(ModelResources))]
        //public decimal CustomerFee { get; set; }


        //[Required(ErrorMessageResourceName = "Customer_Total_Price_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        //[Display(Name = "Customer_Total_Price", ResourceType = typeof(ModelResources))]
        //public decimal CustomerTotalPrice { get; set; }


        //[Required(ErrorMessageResourceName = "AreaTransport_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        //[Display(Name = "AreaTransport", ResourceType = typeof(ModelResources))]
        //public int AreaTransportId { get; set; }
        //public AreaTransport AreaTransport { get; set; }


        //[Required(ErrorMessageResourceName = "AreaTransport_Unit_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        //[Display(Name = "AreaTransport_Unit", ResourceType = typeof(ModelResources))]
        //public decimal AreaTransportUnit { get; set; }


        //[Required(ErrorMessageResourceName = "AreaTransport_Amount_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        //[Display(Name = "AreaTransport_Amount", ResourceType = typeof(ModelResources))]
        //public decimal AreaTransportAmount { get; set; }

        public List<MazotInvoiceDetails> lstDetails { get; set; }
    }
}
