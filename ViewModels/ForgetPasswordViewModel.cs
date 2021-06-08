using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.ViewModels
{
    public class ForgetPasswordViewModel
    {
       
        [EmailAddress(ErrorMessage ="Must Be Email Adress")]
        [Required(ErrorMessage = "Email Is Required")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
