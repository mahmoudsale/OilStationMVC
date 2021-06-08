using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models
{
    public class CustodyType
    {
        [Display(Name = "Code")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        [Display(Name = "Name")]
        public string TypeName { get; set; }
    }
}
