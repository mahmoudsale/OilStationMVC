using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models
{
    public class AreaTransport
    {
        public AreaTransport()
        {
            BranchId = 1;
        }
        [Key]
 
        [Display(Name = "Code")]
        public int Id { get; set; }
        [NotMapped]
        public string EncryptedID { get; set; }

        public int BranchId { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

 
        [Display(Name = "English Name")]
        public string NameEng { get; set; }
 
        [Display(Name = "Adress")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Unti Cost Is Required")]
        [Display(Name = "Unit Cost")]
        public decimal Cost { get; set; }
 
        [Display(Name = "Description")]
        public string Desc { get; set; } 
    }
}
