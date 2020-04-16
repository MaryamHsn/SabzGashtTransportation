using System.ComponentModel.DataAnnotations;

namespace SabzGashtTransportation.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        //[Required]
        //[Display(Name = "ایمیل")]
        //public string Email { get; set; }

        [Required]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
    }
}