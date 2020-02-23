using System.Collections.Generic;

namespace Sabz.ServiceLayer.ViewModel
{
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { set; get; }
    }
}