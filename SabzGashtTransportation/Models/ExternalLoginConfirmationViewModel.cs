using System.ComponentModel.DataAnnotations;

namespace SabzGashtTransportation.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        //[Required]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "پر کردن مقدار اجباری است")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
    }
}