using System.ComponentModel.DataAnnotations;

namespace NovelsRanboeTranslates.Domain.ViewModels
{
    public class AuthorizationViewModel
    {
        [Required(ErrorMessage = "Login is a required field")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "The login must contain from 4 to 50 characters")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is a required field")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "The password must contain from 6 to 100 characters")]
        public string Password { get; set; }

    }
}
