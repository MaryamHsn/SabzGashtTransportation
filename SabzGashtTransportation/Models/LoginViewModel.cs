using System.ComponentModel.DataAnnotations;

namespace SabzGashtTransportation.Models
{
    public class LoginViewModel
    {
        //[Required]
        //[Display(Name = "ایمیل")]
        //[EmailAddress]
        //public string Email { get; set; }

        [Required]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Display(Name = "مرابه خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}