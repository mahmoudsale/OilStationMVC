using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int DriverId { get; set; }

        public Driver Driver { get; set; }

        public string Desc { get; set; }
        public string PhotoPath { get; set; }
        public int SubledgerTypeId { get; set; }
    }
}
