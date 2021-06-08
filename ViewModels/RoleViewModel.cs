using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.ViewModels
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {
            Users = new List<string>();
        }
        public string Id  { get; set; }

        [Required]
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }

        public bool IsSaved { get; set; } = false;

        public List<string> Users  { get; set; }
    }
}
