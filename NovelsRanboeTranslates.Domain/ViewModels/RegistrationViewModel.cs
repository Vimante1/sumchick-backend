using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelsRanboeTranslates.Domain.ViewModels
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Login is a required field")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "The login must contain from 4 to 50 characters")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is a required field")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "The password must contain from 6 to 100 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is a mandatory field")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
