using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models
{
    public class CustomerType
    { 
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
