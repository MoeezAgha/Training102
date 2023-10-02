using System.ComponentModel.DataAnnotations;

namespace Training102.BAL.SharedModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter your username or email.")]
        [Display(Name = "Username or Email")]
        public string EmailOrUserName { get; init; }

        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; init; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; init; } = false;
    }
}
