using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.ViewModels
{
    public class StockViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string NameEng { get; set; }
        [Required]
        public decimal Cost { get; set; }
        public string Adress { get; set; }
        public string Desc { get; set; }
    }
}
