using System.ComponentModel.DataAnnotations;

namespace SabzGashtTransportation.Models
{
    public class RegisterViewModel
    {
        //[Required]
        //[EmailAddress]
        //[Display(Name = "Email")]
        //public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تایید رمز عبور")]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن برابر نمی باشد")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }   
    }
}