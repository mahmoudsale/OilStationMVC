using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OilStationMVC.Views.Shared.Resources;
namespace OilStationMVC.Models
{
    public class Stock
    { 
        [Key] 
        [Display(Name = "Stock Id")]
        public int Id { get; set; }


        [Required(ErrorMessageResourceName = "Stock_Name_Is_Required", ErrorMessageResourceType = typeof(ModelResources))]
        [Display(Name = "Stock_Name", ResourceType = typeof(ModelResources))]
        public string Name { get; set; }

        [Display(Name = "Stock Name English")]
        public string NameEng { get; set; }

        [Required(ErrorMessage = "Stock Unit Cost Is Required")]
        [Display(Name = "Stock Unit Cost")]
        public decimal Cost { get; set; }

        [Display(Name = "Adress")]
        public string Adress { get; set; }

        [Display(Name = "Description")]
        public string Desc { get; set; } 
    }
}
