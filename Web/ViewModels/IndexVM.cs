using DAL.Models;
using LazZiya.ExpressLocalization.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class InputModel
    {
        [ExRequired]
        [Display(Name = "Name")]
        public string Nome { get; set; }
        [Display(Name = "WhatsApp")]
        public string WhatsApp { get; set; }
        [ExRequired]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [ExRequired]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [ExStringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [ExCompare("Password")]
        public string ConfirmPassword { get; set; }
    }

    public class IndexVM
    {
        public List<Curso> Cursos { get; set; }
        public InputModel InputModel { get; set; }
        public List<string> CheckoutIds { get; set; }
    }
}
