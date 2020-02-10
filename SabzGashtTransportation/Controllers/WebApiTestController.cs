using System.Web.Mvc;

namespace SabzGashtTransportation.Controllers
{
    [Authorize]
    public class WebApiTestController : Controller
    {
        // GET: WebApiTest
        public ActionResult Index()
        {
            return View();
        }
    }
}