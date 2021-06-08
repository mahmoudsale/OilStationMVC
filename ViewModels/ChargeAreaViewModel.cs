using OilStationMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.ViewModels
{
    public class ChargeAreaViewModel
    {
        [Display(Name = "Code")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Unit Price Is Required")]
        [Display(Name = "Unit Price")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Company Is Required")]
        [Display(Name = "Company")]
        public decimal CompanyId { get; set; }

        public List <Company> Companies { get; set; }
    }
}
