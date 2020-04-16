﻿using System.ComponentModel.DataAnnotations;

namespace SabzGashtTransportation.Models
{
    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور جدید")]
        [Compare("NewPassword", ErrorMessage = "رمز عبور های وترد شده مطابق هم نیستتند")]
        public string ConfirmPassword { get; set; }
    }
}