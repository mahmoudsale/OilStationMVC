using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models
{
    public class Customer
    { 
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Adress { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Must Be Phone Number")]
        public string Telephone { get; set; }
        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage ="Must Be Email Aderess")]
        public string Email { get; set; }
        public string Fax { get; set; }
        [Required]
        public int CustomerTypeId { get; set; }
        public CustomerType CustomerType { get; set; }
        public string Desc { get; set; } 
        public int SubledgerTypeId { get; set; } = 1;
        [NotMapped]
        public decimal MazotUnitPrice { get; set; }
    }
}
