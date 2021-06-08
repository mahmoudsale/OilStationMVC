using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models
{
    public class Product
    {
        [Display(Name = "Code")]
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Buy Unit Price Is Required")]
        [Display(Name = "Buy Unit Price")]
        public decimal BuyUnitPrice { get; set; }

        [Required(ErrorMessage = "Sale Unit Price Is Required")]
        [Display(Name = "Sale Unit Price")]
        public decimal SaleUnitPrice{ get; set; }

        [Required(ErrorMessage = "First Period Balance Is Required")]
        [Display(Name = "First Period Balance")]
        public decimal FirstPeriodBalance { get; set; }

  

    }
}
