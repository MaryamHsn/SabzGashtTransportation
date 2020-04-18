using System.ComponentModel.DataAnnotations;

namespace SabzGashtTransportation.Models
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "دسترسی")]
        public string Name { get; set; }
    }
}