using System.ComponentModel.DataAnnotations;

namespace SabzGashtTransportation.Models
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "شماه تلفن")]
        public string Number { get; set; }
    }
}