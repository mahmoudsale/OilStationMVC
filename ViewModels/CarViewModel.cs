 
using OilStationMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.ViewModels
{
    public class CarViewModel
    {
        [Key]
        [Display(Name = "Car Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Car Name Is Required")]
        [Display(Name = "Car Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Desc { get; set; }

        [Required(ErrorMessage = "Driver Is Required")]
        [Display(Name = "Driver Name")]
        public int DriverId { get; set; } 

        //public IFormFile PhotoPath { get; set; }
        public string  PhotoName { get; set; }

        public Driver Driver { get; set; }

    }
}
