using System.ComponentModel.DataAnnotations;

namespace SabzGashtTransportation.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}