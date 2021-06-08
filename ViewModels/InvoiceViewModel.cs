using OilStationMVC.Models;
using OilStationMVC.Views.Shared.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks; 
namespace OilStationMVC.ViewModels
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public int JournalType { get; set; }

        [Required(ErrorMessageResourceName = "Innvoice_No_Is_Required", ErrorMessageResourceType = typeof(ModelResources))] 
        [Display(Name = "Innvoice_No", ResourceType = typeof(ModelResources))]
        public int InnvoiceNo { get; set; }
         
        [DataType(DataType.Date)] 
        [Required(ErrorMessageResourceName ="Date_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Date", ResourceType = typeof(ModelResources))]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime dDate { get; set; }

      
        [Required(ErrorMessageResourceName = "Solar_QTY_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Solar_QTY", ResourceType = typeof(ModelResources))]
        public int SolarQTY { get; set; }

 
        [Required(ErrorMessageResourceName = "Oil80_QTY_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Oil80_QTY", ResourceType = typeof(ModelResources))]
        public int Oil80QTY { get; set; }

        [Required(ErrorMessageResourceName = "Oil92_QTY_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Oil92_QTY", ResourceType = typeof(ModelResources))] 
        public int Oil92QTY { get; set; }


        [Required(ErrorMessageResourceName = "Oil95_QTY_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Oil95_QTY", ResourceType = typeof(ModelResources))]
        public int Oil95QTY { get; set; }

 
        [Required(ErrorMessageResourceName = "Solar_Unit_Price_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Solar_Unit_Price", ResourceType = typeof(ModelResources))]
        public decimal SolarUnitPrice { get; set; }

 
        [Required(ErrorMessageResourceName = "Oil80_Unit_Price_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Oil80_Unit_Price", ResourceType = typeof(ModelResources))]
        public decimal Oil80UnitPrice { get; set; }

        [Required(ErrorMessageResourceName = "Oil92_Unit_Price_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Oil92_Unit_Price", ResourceType = typeof(ModelResources))]
        public decimal Oil92UnitPrice { get; set; }

        [Required(ErrorMessageResourceName = "Oil95_Unit_Price_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Oil95_Unit_Price", ResourceType = typeof(ModelResources))]
        public decimal Oil95UnitPrice { get; set; }
 
        [Required(ErrorMessageResourceName = "Solar_Total_Price_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Solar_Total_Price", ResourceType = typeof(ModelResources))]
        public decimal SolarTotalPrice { get; set; }

 
        [Required(ErrorMessageResourceName = "Oil80_Total_Price_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Oil80_Total_Price", ResourceType = typeof(ModelResources))]
        public decimal Oil80TotalPrice { get; set; }

        [Required(ErrorMessageResourceName = "Oil92_Total_Price_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Oil92_Total_Price", ResourceType = typeof(ModelResources))]
        public decimal Oil92TotalPrice { get; set; }

        [Required(ErrorMessageResourceName = "Oil95_Total_Price_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Oil95_Total_Price", ResourceType = typeof(ModelResources))]
        public decimal Oil95TotalPrice { get; set; }

 
        [Required(ErrorMessageResourceName = "Total_Price_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Total_Price", ResourceType = typeof(ModelResources))]
        public decimal TotalPrice { get; set; }

 
        [Display(Name = "Description", ResourceType = typeof(ModelResources))]
        public string Desc { get; set; }

        [Required(ErrorMessageResourceName = "Company_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Company", ResourceType = typeof(ModelResources))]
        public int CompanyId { get; set; }

        [Required(ErrorMessageResourceName = "Stock_Name_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Stock_Name", ResourceType = typeof(ModelResources))]
        public int StockId { get; set; }

        [Required(ErrorMessageResourceName = "Station_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Station", ResourceType = typeof(ModelResources))]
        public int StationId { get; set; }

        [Required(ErrorMessageResourceName = "Car_Name_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Car_Name", ResourceType = typeof(ModelResources))]
        public int CarId { get; set; }

        [Required(ErrorMessageResourceName = "Driver_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Driver", ResourceType = typeof(ModelResources))]
        public int DriverId { get; set; }

 
        [Required(ErrorMessageResourceName = "Stock_Transport_Unit_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Stock_Transport_Unit", ResourceType = typeof(ModelResources))]
        public decimal StockTransportUnit { get; set; }

 
        [Required(ErrorMessageResourceName = "Stock_Transport_Amount_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Stock_Transport_Amount", ResourceType = typeof(ModelResources))]
        public decimal StockTransportAmount { get; set; }


        public Company Company { get; set; } 
        public Stock Stock { get; set; } 
        public Station Station { get; set; } 
        public Car Car { get; set; } 
        public Driver Driver { get; set; }

    }
}
