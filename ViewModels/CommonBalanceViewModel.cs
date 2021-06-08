using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.ViewModels
{
    public class CommonBalanceViewModel
    { 
        public int SubledgerTypeId { get; set; }
        public string SubledgerTypeName { get; set; }
        public int SubledgerId { get; set; }
        public string SubledgerName { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public decimal Balance { get; set; }
        public string Telephone { get; set; }
        public int CustomerTypeId { get; set; }

    }
}
