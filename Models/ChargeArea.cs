using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models
{
    public class ChargeArea
    {
        [Key] 
        public int Id { get; set; }
        [Required] 
        public string Name { get; set; }
        [Required]
        public decimal Cost { get; set; }
      
        public Company Company{ get; set; }
        [Required]
        public decimal CompanyId { get; internal set; }
    }
}
