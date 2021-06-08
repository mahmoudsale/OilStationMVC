using OilStationMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.ViewModels
{
    public class CustomerViewModel
    {
        [Display(Name = "Code")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Customer Name Is Required")]
        [Display(Name = "Customer Name")]
        public string Name { get; set; }

        [Display(Name = "Customer Adress")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Phone Number Is Required")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Must Be Phone Number")]
        public string Telephone { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Must Be Email Aderess")]
        public string Email { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Required(ErrorMessage = "Customer Type Is Required")]
        [Display(Name = "Customer Type")]
        public string CustomerTypeId { get; set; }

        [Display(Name = "Description")]
        public string Desc { get; set; }

        [Display(Name = "Current Balance")]
        public decimal CurrentBalance { get; set; }

        public string Image { get; set; }

    }
}
