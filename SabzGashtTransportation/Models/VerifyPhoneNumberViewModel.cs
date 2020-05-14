using System.ComponentModel.DataAnnotations;

namespace SabzGashtTransportation.Models
{
    public class VerifyPhoneNumberViewModel
    {
        [Required(ErrorMessage = "پر کردن مقدار اجباری است")]
        [Display(Name = "کد")]
        public string Code { get; set; }

        [Required(ErrorMessage = "پر کردن مقدار اجباری است")]
        [Phone]
        [Display(Name = "شماره تلفن")]
        public string PhoneNumber { get; set; }
    }
}