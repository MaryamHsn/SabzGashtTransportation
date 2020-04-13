using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using Sabz.DataLayer.Context;
using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.Enumration;
using Sabz.ServiceLayer.IService;
using Sabz.ServiceLayer.Mapper;
using Sabz.ServiceLayer.Utils;
using Sabz.ServiceLayer.ViewModel;
using SabzGashtTransportation.ViewModel;
using StructureMap.Configuration.DSL;

namespace SabzGashtTransportation.Controllers
{
    public class LogDriverRoutController : Controller
    {
        readonly ILogDriverRoutService _LogDriverRout;
        readonly IRoutService _rout;
        readonly IDriverService _driver;
        readonly IDriverRoutService _driverRout;
        readonly IRegionService _region;
        readonly IUnitOfWork _uow;
        private LogDriverRoutViewModel common { get; set; }
        private List<LogDriverRoutViewModel> commonList { get; set; }

        public LogDriverRoutController(IUnitOfWork uow, ILogDriverRoutService LogDriverRout, IRoutService rout,
            IDriverService driver, IDriverRoutService driverRout, IRegionService region)
        {
            _region = region;
            _driver = driver;
            _rout = rout;
            _driverRout = driverRout;
            _LogDriverRout = LogDriverRout;
            _uow = uow;
        }

        // GET: Automobile
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, string searchDriver, string searchRout, string searchDateFrom, string searchDateTo, string dropRegionId, int? page)
        {
            commonList = new List<LogDriverRoutViewModel>();
            var regions = _region.GetAllRegions();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var allDrivers = _driver.GetAllDrivers();
            IEnumerable<SelectListItem> regionItems = regions.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.RegionName
            });
            ViewBag.RegionItems = regionItems; 
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Driver = String.IsNullOrEmpty(sortOrder) ? "driver_desc" : "";
            ViewBag.IsDone = sortOrder == "isDone" ? "isDone_desc" : "isDone";
            ViewBag.DoDate = sortOrder == "doDate" ? "doDate_desc" : "doDate";
            ViewBag.Fine = sortOrder == "fine" ? "fine_desc" : "fine";
            ViewBag.RoutTransaction = sortOrder == "routTransaction" ? "routTransaction_desc" : "routTransaction";
            ViewBag.RoutShiftType = sortOrder == "routShiftType" ? "routShiftType_desc" : "routShiftType";
            ViewBag.RoutRegion = sortOrder == "routRegion" ? "routRegion_desc" : "routRegion";
            ViewBag.RoutTime = sortOrder == "routTime" ? "routTime_desc" : "routTime";
            ViewBag.RoutDate = sortOrder == "routDate" ? "routDate_desc" : "routDate";
            //if (searchString != null)
            //{
            //    page = 1;
            //}
            //else
            //{
            //    searchString = currentFilter;
            //}
            //ViewBag.CurrentFilter = searchString;
            var log = _LogDriverRout.GetAllLogDriverRouts();
            var driverRout = _driverRout.GetAllDriverRouts();
            var routs = _rout.GetAllRouts();
            var drivers = _driver.GetAllDrivers();
            if (string.IsNullOrEmpty(dropRegionId))
            {
                //dropRegionId = allRegion.FirstOrDefault().Id.ToString();
                dropRegionId = "0";
            }
            var list = _LogDriverRout.GetAllLogDriverRoutsByRegionId(int.Parse(dropRegionId));
            if (!string.IsNullOrWhiteSpace(searchDateFrom) && !string.IsNullOrWhiteSpace(searchDriver))
            {
                if (string.IsNullOrWhiteSpace(searchDateTo))
                {
                    list = _LogDriverRout.GetAllLogDriverRoutsByDriverNameByDateByRegionId(searchDriver, int.Parse(dropRegionId), searchDateFrom.ToGeorgianDate(), DateTime.Now);
                }
                else
                {
                    list = _LogDriverRout.GetAllLogDriverRoutsByDriverNameByDateByRegionId(searchDriver, int.Parse(dropRegionId), searchDateFrom.ToGeorgianDate(), searchDateTo.ToGeorgianDate());
                }
            }
            else if (!string.IsNullOrWhiteSpace(searchDateFrom) && string.IsNullOrWhiteSpace(searchDriver))
            {
                if (string.IsNullOrWhiteSpace(searchDateTo))
                {
                    list = _LogDriverRout.GetAllLogDriverRoutsByDateByRegionId(int.Parse(dropRegionId), searchDateFrom.ToGeorgianDate(), DateTime.Now);
                }
                else
                {
                    list = _LogDriverRout.GetAllLogDriverRoutsByDateByRegionId(int.Parse(dropRegionId), searchDateFrom.ToGeorgianDate(), searchDateTo.ToGeorgianDate());
                }
            }
            else if (string.IsNullOrWhiteSpace(searchDateFrom) && !string.IsNullOrWhiteSpace(searchDriver))
            {
                list = _LogDriverRout.GetAllLogDriverRoutsByDriverNameByRegionId(searchDriver, int.Parse(dropRegionId));
            }
            foreach (var item in list)
            {
                item.DriverRoutTbl = driverRout.Where(x => x.Id == item.DriverRoutId).FirstOrDefault();
                item.DriverRoutTbl.RoutTbl = routs.Where(x => x.Id == item.DriverRoutTbl.RoutId).FirstOrDefault();
                item.DriverRoutTbl.DriverTbl = drivers.Where(x => x.Id == item.DriverRoutTbl.DriverId).FirstOrDefault();
            }
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    list = list.Where(s => s.DriverRoutTbl.DriverTbl.FullName.ToString().Contains(searchString)  || s.IsDone.ToString().Contains(searchString)).ToList();
            //}
            switch (sortOrder)
            {
                case "driver_desc":
                    list = list.OrderByDescending(s => s.DriverRoutTbl.DriverTbl.FullName).ToList();
                    break;
                case "isDone":
                    list = list.OrderBy(s => s.IsDone).ToList();
                    break;
                case "isDone_desc":
                    list = list.OrderByDescending(s => s.IsDone).ToList();
                    break;
                case "doDate":
                    list = list.OrderBy(s => s.DoDate).ToList();
                    break;
                case "doDate_desc":
                    list = list.OrderByDescending(s => s.DoDate).ToList();
                    break;
                case "fine":
                    list = list.OrderBy(s => s.FinePrice).ToList();
                    break;
                case "fine_desc":
                    list = list.OrderByDescending(s => s.FinePrice).ToList();
                    break;
                default:
                    list = list.OrderBy(s => s.DriverRoutTbl.DriverTbl.FullName).ToList();
                    break;
            }
            foreach (var item in list)
            {
                var element = BaseMapper<LogDriverRoutViewModel, LogDriverRoutTbl>.Map(item);
                element.DoDateString = element.DoDate.ToPersianDateString();
                element.DriverFullName = allDrivers.Where(x => x.Id == item.DriverRoutTbl.DriverId).FirstOrDefault().FullName;
                element.RoutEnterTimeString = item.DriverRoutTbl.RoutTbl.EnterTime.ToString();
                element.RoutStartDate = item.DriverRoutTbl.RoutTbl.StartDate;
                element.RoutStartDateString = item.DriverRoutTbl.RoutTbl.StartDate.ToPersianDateString();
                element.RoutRegionName = item.DriverRoutTbl.RoutTbl.RegionTbl.RegionName;
                element.RoutShiftType = item.DriverRoutTbl.RoutTbl.ShiftType;
                element.RoutTransactionType = item.DriverRoutTbl.RoutTbl.RoutTransactionType;
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
            LogDriverRoutTbl routDriver = _LogDriverRout.GetLogDriverRout(id);
            if (routDriver == null)
            {
                return HttpNotFound();
            }
            common = new LogDriverRoutViewModel();
            common = BaseMapper<LogDriverRoutViewModel, LogDriverRoutTbl>.Map(routDriver);
            common.DoDateString = routDriver.DoDate.ToPersianDateString();
            common.DriverRoutTbl = _driverRout.GetDriverRout(routDriver.DriverRoutId);
            common.DriverRoutTbl.DriverTbl = _driver.GetDriver(routDriver.DriverRoutTbl.DriverId);
            common.DriverRoutTbl.RoutTbl = _rout.GetRout(routDriver.DriverRoutTbl.RoutId);
            common.DoDateString = routDriver.DoDate.ToPersianDateString();
            common.DriverFullName = _driver.GetDriver(routDriver.DriverRoutTbl.DriverId).FullName;
            common.RoutEnterTimeString = routDriver.DriverRoutTbl.RoutTbl.EnterTime.ToString();
            common.RoutStartDate = routDriver.DriverRoutTbl.RoutTbl.StartDate;
            common.RoutStartDateString = routDriver.DriverRoutTbl.RoutTbl.StartDate.ToPersianDateString();
            common.RoutRegionName = routDriver.DriverRoutTbl.RoutTbl.RegionTbl.RegionName;
            common.RoutShiftType = routDriver.DriverRoutTbl.RoutTbl.ShiftType;
            common.RoutTransactionType = routDriver.DriverRoutTbl.RoutTbl.RoutTransactionType;

            return View(common);
        }

        // GET: Drivers/Create
        public ActionResult Create(string sortOrder, string currentFilter, string searchString, string searchDriver, string searchRout, string searchDateFrom, string searchDateTo, string dropRegionId, int? page)
        {
            var commonList = new List<LogDriverRoutViewModel>();
            var regions = _region.GetAllRegions();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var allDrivers = _driver.GetAllDrivers();
            IEnumerable<SelectListItem> regionItems = regions.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.RegionName
            });
            ViewBag.RegionItems = regionItems;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Driver = String.IsNullOrEmpty(sortOrder) ? "driver_desc" : "";
            ViewBag.IsDone = sortOrder == "isDone" ? "isDone_desc" : "isDone";
            ViewBag.DoDate = sortOrder == "doDate" ? "doDate_desc" : "doDate";
            ViewBag.Fine = sortOrder == "fine" ? "fine_desc" : "fine";
            ViewBag.RoutTransaction = sortOrder == "routTransaction" ? "routTransaction_desc" : "routTransaction";
            ViewBag.RoutShiftType = sortOrder == "routShiftType" ? "routShiftType_desc" : "routShiftType";
            ViewBag.RoutRegion = sortOrder == "routRegion" ? "routRegion_desc" : "routRegion";
            ViewBag.RoutTime = sortOrder == "routTime" ? "routTime_desc" : "routTime";
            ViewBag.RoutDate = sortOrder == "routDate" ? "routDate_desc" : "routDate";
            //if (searchString != null)
            //{
            //    page = 1;
            //}
            //else
            //{
            //    searchString = currentFilter;
            //}
            //ViewBag.CurrentFilter = searchString;
            if (string.IsNullOrEmpty(dropRegionId))
            {
                //dropRegionId = allRegion.FirstOrDefault().Id.ToString();
                dropRegionId = "0";
            }
            var log = _LogDriverRout.GetAllLogDriverRouts();
            var driverRout = _driverRout.GetAllDriverRoutsByRegionId(int.Parse(dropRegionId));
            var routs = _rout.GetAllRouts();
            var drivers = _driver.GetAllDrivers();

            var listId = driverRout.Select(x => x.Id).Except(log.Select(x => x.DriverRoutId));
            var list = _driverRout.GetAllDriverRoutsByIds(listId.ToList());
      
            /////////////
            if (!string.IsNullOrWhiteSpace(searchDateFrom) && !string.IsNullOrWhiteSpace(searchDriver))
            {
                if (string.IsNullOrWhiteSpace(searchDateTo))
                {
                    list = _driverRout.GetDriverRoutByDateByDriverNameByRegionId(searchDateFrom.ToGeorgianDate(), DateTime.Now, searchDriver, int.Parse(dropRegionId));
                }
                else
                {
                    list = _driverRout.GetDriverRoutByDateByDriverNameByRegionId(searchDateFrom.ToGeorgianDate(), searchDateTo.ToGeorgianDate(), searchDriver, int.Parse(dropRegionId));
                }
            }
            else if (!string.IsNullOrWhiteSpace(searchDateFrom) && string.IsNullOrWhiteSpace(searchDriver))
            {
                if (string.IsNullOrWhiteSpace(searchDateTo))
                {
                    list = _driverRout.GetDriverRoutByDateByRegionId( searchDateFrom.ToGeorgianDate(), DateTime.Now, int.Parse(dropRegionId));
                }
                else
                {
                    list = _driverRout.GetDriverRoutByDateByRegionId(searchDateFrom.ToGeorgianDate(), searchDateFrom.ToGeorgianDate(), int.Parse(dropRegionId));

                }
            }
            else if (string.IsNullOrWhiteSpace(searchDateFrom) && !string.IsNullOrWhiteSpace(searchDriver))
            {
                list = _driverRout.GetDriverRoutByDriverNameByRegionId(searchDriver, int.Parse(dropRegionId));
            }
            ///////////
            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(s => s.DriverTbl.FullName.ToString().Contains(searchString) ).ToList();
            }
            switch (sortOrder)
            {
                case "driver_desc":
                    list = list.OrderByDescending(s => s.DriverTbl.FullName).ToList();
                    break;

                case "doDate":
                    list = list.OrderBy(s => s.RoutTbl.StartDate).ToList();
                    break;
                case "doDate_desc":
                    list = list.OrderByDescending(s => s.RoutTbl.StartDate).ToList();
                    break;
                default:
                    list = list.OrderBy(s => s.DriverTbl.FullName).ToList();
                    break;
            }

            foreach (var item in list)
            {
                var element = new LogDriverRoutViewModel(); //BaseMapper<DriverRoutViewModel, DriverRoutTbl>.Map(item);
                element.RoutStartDateString = item.RoutTbl.StartDate.ToPersianDateString();
                element.DriverFullName = allDrivers.Where(x => x.Id == item.DriverId).FirstOrDefault().FullName;
                element.RoutEnterTimeString = item.RoutTbl.EnterTime.ToString();
                element.RoutStartDate = item.RoutTbl.StartDate;
                element.RoutStartDateString = item.RoutTbl.StartDate.ToPersianDateString();
                element.RoutRegionName = item.RoutTbl.RegionTbl.RegionName;
                element.RoutShiftType = routs.Where(x => x.Id == item.RoutId).FirstOrDefault().ShiftType;
                element.RoutTransactionType = routs.Where(x => x.Id == item.RoutId).FirstOrDefault().RoutTransactionType;
                element.DriverRoutId = item.Id;
                commonList.Add(element);
            }
            return View(commonList.ToPagedList(pageNumber, pageSize));
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(List<LogDriverRoutViewModel> routDrivers)
        {
            if (ModelState.IsValid)
            {
                foreach (var routDriver in routDrivers)
                {
                    var obj = BaseMapper<LogDriverRoutViewModel, LogDriverRoutTbl>.Map(routDriver);
                    obj.DoDate = routDriver.RoutStartDateString.ToGeorgianDate();
                    obj.IsDone = Convert.ToBoolean(routDriver.IsDone);
                    obj.DriverRoutId = routDriver.DriverRoutId;
                    obj.FinePrice= routDriver.FinePrice == null ? 0 : routDriver.FinePrice;
                    _LogDriverRout.AddNewLogDriverRout(obj);
                }
                _uow.SaveAllChanges();

            }
            return RedirectToAction("Index");
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(LogDriverRoutViewModel routDriver)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // common = new LogDriverRoutViewModel();
        //        var obj = BaseMapper<LogDriverRoutViewModel, LogDriverRoutTbl>.Map(routDriver);
        //        obj.DoDate = routDriver.RoutStartDateString.ToGeorgianDate();
        //        obj.IsDone = Convert.ToBoolean(routDriver.WorkDoneEnum);
        //        var findDriverRoutTbl = _driverRout.GetDriverRoutByDriverIdRoutId(routDriver.DriverId, routDriver.RoutId);
        //        if (findDriverRoutTbl != null)
        //        {
        //            obj.DriverRoutId = findDriverRoutTbl.Id;
        //            _LogDriverRout.AddNewLogDriverRout(obj);
        //            _uow.SaveAllChanges();
        //        }

        //    }
        //    return RedirectToAction("Index");
        //}

        // GET: Drivers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogDriverRoutTbl LogDriverRout = _LogDriverRout.GetLogDriverRout(id);

            if (LogDriverRout == null)
            {
                return HttpNotFound();
            }
            var obj = BaseMapper<LogDriverRoutViewModel, LogDriverRoutTbl>.Map(LogDriverRout);
            obj.DriverTblList = _driver.GetAllDrivers();
            obj.RoutTblList = _rout.GetAllRouts();
            obj.DriverRoutTbl = _driverRout.GetDriverRout(obj.DriverRoutId);
            obj.DriverTbl = _driver.GetDriver(obj.DriverRoutTbl.DriverId);
            obj.RoutTbl = _rout.GetRout(obj.DriverRoutTbl.RoutId);
            obj.DriverFullName = obj.DriverRoutTbl.DriverTbl.FullName;
            obj.DoDateString = obj.DoDate.ToPersianDateString();
            obj.DriverId = obj.DriverTbl.Id;
            obj.RoutId = obj.RoutTbl.Id;
            if (obj.IsDone == Convert.ToBoolean(WorkDoneEnum.Done))
            {
                obj.WorkDoneEnum = WorkDoneEnum.Done;
            }
            else
            {
                obj.WorkDoneEnum = WorkDoneEnum.NotDone;
            }
            return View(obj);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LogDriverRoutViewModel LogDriverRout)
        {
            if (ModelState.IsValid)
            {
                var obj = BaseMapper<LogDriverRoutViewModel, LogDriverRoutTbl>.Map(LogDriverRout);
                obj.DoDate = LogDriverRout.DoDateString.ToGeorgianDate();
                obj.IsActive = true;
                obj.IsDone = Convert.ToBoolean(LogDriverRout.WorkDoneEnum);
                obj.DriverRoutTbl = _driverRout.GetDriverRoutByDriverIdRoutId(LogDriverRout.DriverId, LogDriverRout.RoutId);
                if (obj.DriverRoutTbl != null)
                {
                    obj.DriverRoutId = obj.DriverRoutTbl.Id;
                    _LogDriverRout.UpdateLogDriverRout(obj);
                    _uow.SaveAllChanges();
                }
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
            LogDriverRoutTbl LogDriverRout = _LogDriverRout.GetLogDriverRout(id);
            if (LogDriverRout == null)
            {
                return HttpNotFound();
            }
            var obj = BaseMapper<LogDriverRoutViewModel, LogDriverRoutTbl>.Map(LogDriverRout);
            obj.DriverRoutTbl = _driverRout.GetDriverRout(obj.DriverRoutId);
            obj.DriverRoutTbl.DriverTbl = _driver.GetDriver(obj.DriverRoutTbl.DriverId);
            obj.DriverRoutTbl.RoutTbl = _rout.GetRout(obj.DriverRoutTbl.RoutId);
            obj.DriverFullName = obj.DriverRoutTbl.DriverTbl.FullName;
            obj.DoDateString = obj.DoDate.ToPersianDateString();
            return View(obj);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _LogDriverRout.Delete(id);
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
