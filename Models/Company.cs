using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models
{
    public class Company
    {
        public Company()
        {
            SubledgerTypeId = 2;
        }
        [Key]
        [Display(Name = "Code")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Opening Balance State Is Required")]
        [Display(Name = "Opening Balance State")]
        public int OpeningBalanceState { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Opening Balance Date Is Required")]
        [Display(Name = "Opening Balance Date")]
        public DateTime OpeningBalanceDate { get; set; }

        [Required(ErrorMessage = "Opening Balance Is Required")]
        [Display(Name = "Opening Balance")]
        public decimal OpeningBalance { get; set; }

        public int SubledgerTypeId { get; set; } = 2;
    }
}
