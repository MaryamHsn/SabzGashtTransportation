using System.Threading.Tasks;
using System.Web.Http;
using Sabz.ServiceLayer.IService;

namespace SabzGashtTransportation.Controllers
{
    public class AccountApiTestController : ApiController
    {
        private readonly IApplicationUserManager _userManager;
        public AccountApiTestController(IApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            var applicationUser = await _userManager.FindByIdAsync(id).ConfigureAwait(false);
            if (applicationUser == null)
            {
                return NotFound();
            }
            return Ok(new { userName = applicationUser.UserName });
        }
    }
}