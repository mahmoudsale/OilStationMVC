using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models
{
    public class Station
    {
        [Key]
        [Display(Name = "Code")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "English Name")]
        public string NameEng { get; set; }

        [Display(Name = "Adress")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Responsible Person Is Required")]
        [Display(Name = "Responsible Person")]
        public string ResponsiblePerson { get; set; }

        [Display(Name = "Phone Number")]
        public string Tel { get; set; }

        [Display(Name = "Description")]
        public string Desc { get; set; }
    }
}
