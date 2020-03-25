using Microsoft.AspNet.Identity.EntityFramework;

namespace  Sabz.DomainClasses.DTO
{
    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }
}