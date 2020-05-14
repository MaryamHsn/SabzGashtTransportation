using System.ComponentModel.DataAnnotations;

namespace SabzGashtTransportation.Models
{
    public class RegisterViewModel
    {
        //[Required]
        ////////[EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "پر کردن مقدار اجباری است")]
        [StringLength(100, ErrorMessage = "حداقل طول{0} باید {2} کاراکتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تایید رمز عبور")]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن برابر نمی باشد")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "پر کردن مقدار اجباری است")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }   
    }
}