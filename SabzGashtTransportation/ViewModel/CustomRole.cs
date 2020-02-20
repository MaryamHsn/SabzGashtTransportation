using Microsoft.AspNet.Identity.EntityFramework;

namespace  SabzGashtTransportation.ViewModel
{
    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }


    }
}