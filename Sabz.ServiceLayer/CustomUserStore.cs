using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.IService;
using Microsoft.AspNet.Identity.EntityFramework;
using Sabz.DataLayer.Context;

namespace Sabz.ServiceLayer
{
    public class CustomUserStore :
        UserStore<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>,
        ICustomUserStore
    {
        private readonly IUnitOfWork _context;

        public CustomUserStore(IUnitOfWork context)
            : base((ApplicationDbContext)context)
        {
            _context = context;
        }

    }
}