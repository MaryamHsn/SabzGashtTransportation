﻿using System.ComponentModel.DataAnnotations;

namespace SabzGashtTransportation.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}