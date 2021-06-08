 
using OilStationMVC.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name Is Required")]
        public string FullName { get; set; }

 
        [EmailAddress]
        //[ValidateDomainName(_AllowaedDomainName: "minisoft.com", ErrorMessage = "Domain Must be minisoft.com")]
        //[Remote(action: "IsEmailExist", controller: "Account")]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email Is Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
