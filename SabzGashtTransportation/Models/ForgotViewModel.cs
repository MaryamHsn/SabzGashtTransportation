using System.ComponentModel.DataAnnotations;

namespace SabzGashtTransportation.Models
{
    public class ForgotViewModel
    {
        //[Required]
        [Display(Name  = "ایمیل")]
        public string email { get; set; }

        [Required]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
    }
}