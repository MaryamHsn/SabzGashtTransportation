using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SabzGashtTransportation.Models
{
    public class EditUserViewModel
    {
        public int Id { get; set; }

        //[Required(AllowEmptyStrings = false)]
        [Display(Name = "ایمیل")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}