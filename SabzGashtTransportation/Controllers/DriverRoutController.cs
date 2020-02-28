using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Sabz.DataLayer.Context;
using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.Enumration;
using Sabz.ServiceLayer.IService;
using Sabz.ServiceLayer.Mapper;
using Sabz.ServiceLayer.ViewModel;
using SabzGashtTransportation.ViewModel;

namespace SabzGashtTransportation.Controllers
{
    public class DriverRoutController : Controller
    {
        readonly IDriverRoutService _driverRout;
        readonly IDriverService _driver;
        readonly IRoutService _rout;

        private DriverRoutViewModel common;
        private List<DriverRoutViewModel> commonList;

        readonly IUnitOfWork _uow;
        public DriverRoutController(IUnitOfWork uow, IDriverRoutService driverRout, IDriverService driver, IRoutService rout)
        {
            _driver = driver;
            _rout = rout;
            _driverRout = driverRout;
            _uow = uow;
        }

        // GET: Automobile
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            commonList = new List<DriverRoutViewModel>();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Driver = String.IsNullOrEmpty(sortOrder) ? "driver_desc" : "";
            ViewBag.Rout = sortOrder == "rout" ? "rout_desc" : "rout";
            ViewBag.IsTemporary = sortOrder == "isTemporary" ? "isTemporary_desc" : "isTemporary";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var list = _driverRout.GetAllDriverRouts();
            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(s => s.DriverTbl.FullName.ToString().Contains(searchString)
                                       || s.RoutTbl.Name.ToString().Contains(searchString)
                                       || s.IsTemporary.ToString().Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "driver_desc":
                    list = list.OrderByDescending(s => s.DriverTbl.FullName).ToList();
                    break;
                case "rout":
                    list = list.OrderBy(s => s.RoutTbl.Name).ToList();
                    break;
                case "rout_desc":
                    list = list.OrderByDescending(s => s.RoutTbl.Name).ToList();
                    break;
                case "isTemporary":
                    list = list.OrderBy(s => s.IsTemporary).ToList();
                    break;
                case "isTemporary_desc":
                    list = list.OrderByDescending(s => s.IsTemporary).ToList();
                    break;
                default:
                    list = list.OrderBy(s => s.DriverId).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var allDrivers = _driver.GetAllDrivers();
            var allRouts = _rout.GetAllRouts();
            foreach (var item in list)
            {
                var element = BaseMapper<DriverRoutViewModel, DriverRoutTbl>.Map(item);
                element.Driver = allDrivers.Where(x => x.DriverId == item.DriverId).FirstOrDefault();
                element.Rout = allRouts.Where(x => x.RoutID == item.RoutId).FirstOrDefault();
                commonList.Add(element);
            }
            return View(commonList.ToPagedList(pageNumber, pageSize));
        }

        // GET: Drivers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DriverRoutTbl driverRout = _driverRout.GetDriverRout(id);

            if (driverRout == null)
            {
                return HttpNotFound();
            }
            var obj = BaseMapper<DriverRoutViewModel, DriverRoutTbl>.Map(driverRout);
            return View(obj);
        }

        // GET: Drivers/Create
        public ActionResult Create()
        {
            common = new DriverRoutViewModel();
            common.RoutList = _rout.GetAllRouts();
            common.DriverList = _driver.GetAllDrivers();
            return View(common);
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DriverRoutTbl rout)
        {
            if (ModelState.IsValid)
            {
                rout.IsActive = true;
                rout.CFDate = DateTime.Now;
                rout.LFDate = DateTime.Now;

                _driverRout.AddNewDriverRout(rout);
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
            DriverRoutTbl driverRout = _driverRout.GetDriverRout(id);

            if (driverRout == null)
            {
                return HttpNotFound();
            }
            var obj = BaseMapper<DriverRoutViewModel,DriverRoutTbl>.Map(driverRout);
            return View(obj);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DriverRoutViewModel driverRout)
        {
            if (ModelState.IsValid)
            {
                driverRout.LFDate = DateTime.Now;
                driverRout.IsActive = false;
                _driverRout.Delete(driverRout.Id);
                var obj = BaseMapper< DriverRoutTbl, DriverRoutViewModel>.Map(driverRout);
                obj.CFDate = DateTime.Now;
                obj.LFDate = DateTime.Now;
                obj.IsActive = true;
                _driverRout.AddNewDriverRout(obj);
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
            DriverRoutTbl rout = _driverRout.GetDriverRout(id);
            if (rout == null)
            {
                return HttpNotFound();
            }
            var obj = BaseMapper<DriverRoutTbl, DriverRoutViewModel>.Map(rout);
            if (obj.IsTemporary == (int)RoutTypeEnum.Always)
            {
                ViewBag.IsTemporary = RoutTypeEnum.Always;
            }
            else
            {
                ViewBag.IsTemporary = RoutTypeEnum.Temporary;
            }

            return View(obj);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _driverRout.Delete(id);
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
