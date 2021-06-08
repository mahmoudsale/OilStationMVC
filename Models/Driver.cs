using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models
{
    public class Driver
    {
        [Key]
        [Display(Name = "Code")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "English Name")]
        public string NameEng { get; set; }

        [Display(Name = "Telephone")]
        [Required(ErrorMessage = "Telephone Is Required")]
        public string Tel { get; set; }

        [Display(Name = "Description")]
        public string Desc { get; set; }
    }
}
