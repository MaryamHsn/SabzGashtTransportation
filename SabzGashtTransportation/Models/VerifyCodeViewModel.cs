using System.ComponentModel.DataAnnotations;

namespace SabzGashtTransportation.Models
{
    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "کد")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "در حافظه  مرورگر ذخیره شود؟")]
        public bool RememberBrowser { get; set; }
    }
}