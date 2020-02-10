using System.Web.Mvc;

namespace SabzGashtTransportation.Controllers
{
    [Authorize]
    public class SignalRTestController : Controller
    {
        // GET: SignalRTest
        public ActionResult Index()
        {
            return View();
        }
    }
}