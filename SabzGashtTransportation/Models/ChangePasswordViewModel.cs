using System.ComponentModel.DataAnnotations;

namespace SabzGashtTransportation.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "پر کردن مقدار اجباری است")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور فعلی")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "پر کردن مقدار اجباری است")]
        [StringLength(100, ErrorMessage = "حداقل طول{0} باید {2} کاراکتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور جدید")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور جدید")]
        [Compare("NewPassword", ErrorMessage = "رمز عبور های وترد شده مطابق هم نیستتند")]
        public string ConfirmPassword { get; set; }
    }
}