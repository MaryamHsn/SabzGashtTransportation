using System.ComponentModel.DataAnnotations;

namespace SabzGashtTransportation.Models
{
    public class VerifyCodeViewModel
    {
        [Required(ErrorMessage = "پر کردن مقدار اجباری است")]
        public string Provider { get; set; }

        [Required(ErrorMessage = "پر کردن مقدار اجباری است")]
        [Display(Name = "کد")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "در حافظه  مرورگر ذخیره شود؟")]
        public bool RememberBrowser { get; set; }
    }
}