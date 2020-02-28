using System; 
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Sabz.DataLayer.Context;
using Sabz.DomainClasses.DTO; 
using Sabz.ServiceLayer.IService;

namespace SabzGashtTransportation.Controllers
{
    public class RegionController : Controller
    {
        private readonly IRegionService _region;
        readonly IUnitOfWork _uow;
        private Re common { get; set; }
        private List<AutomobileTypeViewModel> commonList { get; set; }

        public RegionController(IUnitOfWork uow, IRegionService region)
        {
            _region = region;
            _uow = uow;
        }

        // GET: Automobile
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.RegionName = String.IsNullOrEmpty(sortOrder) ? "regionName_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var list = _region.GetAllRegions();
            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(s => s.RegionName.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "regionName_desc":
                    list = list.OrderByDescending(s => s.RegionName).ToList();
                    break;
                default:
                    list = list.OrderBy(s => s.RegionName).ToList();
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        // GET: Drivers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegionTbl region = _region.GetRegion(id);

            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // GET: Drivers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegionTbl region)
        {
            if (ModelState.IsValid)
            {
                region.IsActive = true;
                region.CFDate = DateTime.Now;
                region.LFDate = DateTime.Now;

                _region.AddNewRegion(region);
                _uow.SaveAllChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Drivers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegionTbl region= _region.GetRegion(id);

            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RegionTbl region)
        {
            if (ModelState.IsValid)
            {
                _region.Delete(region.RegionId);
                region.LFDate = DateTime.Now;
                _region.AddNewRegion(region);
                _uow.SaveAllChanges();
            }
            return RedirectToAction("Index");

        }

        // GET: Drivers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegionTbl region= _region.GetRegion(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _region.Delete(id);
            _uow.SaveAllChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _uow.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
