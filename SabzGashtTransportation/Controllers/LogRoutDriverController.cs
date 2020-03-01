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
    public class LogRoutDriverController : Controller
    {
        readonly ILogRoutDriverService _logRoutDriver;
        readonly IRoutService _rout;
        readonly IDriverService _driver;
        readonly IDriverRoutService _driverRout;

        readonly IUnitOfWork _uow;
        private LogRoutDriverViewModel common { get; set; }
        private List<LogRoutDriverViewModel> commonList { get; set; }

        public LogRoutDriverController(IUnitOfWork uow, ILogRoutDriverService logRoutDriver, IRoutService rout,
            IDriverService driver, IDriverRoutService driverRout)
        {
            _driver = driver;
            _rout = rout;
            _driverRout = driverRout;
            _logRoutDriver = logRoutDriver;
            _uow = uow;
        }

        // GET: Automobile
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Driver = String.IsNullOrEmpty(sortOrder) ? "driver_desc" : "";
            ViewBag.Rout = sortOrder == "rout" ? "rout_desc" : "rout";
            ViewBag.IsTemporary = sortOrder == "isTemporary" ? "isTemporary_desc" : "isTemporary";
            ViewBag.IsDone = sortOrder == "isDone" ? "isDone_desc" : "isDone";
            ViewBag.DoDate = sortOrder == "doDate" ? "doDate_desc" : "doDate";
            ViewBag.Fine = sortOrder == "fine" ? "fine_desc" : "fine";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            commonList = new List<LogRoutDriverViewModel>();
            //ViewBag.CurrentFilter = searchString;
            var log = _logRoutDriver.GetAllLogRoutDrivers();
            var driverRout = _driverRout.GetAllDriverRouts();
            var routs = _rout.GetAllRouts();
            var drivers = _driver.GetAllDrivers(); 
            var list = _logRoutDriver.GetAllLogRoutDrivers();
            foreach (var item in list)
            {
                item.DriverRoutTbl = driverRout.Where(x => x.Id == item.DriverRoutId).FirstOrDefault();
                item.DriverRoutTbl.RoutTbl = routs.Where(x => x.RoutID == item.DriverRoutTbl.RoutId).FirstOrDefault();
                item.DriverRoutTbl.DriverTbl = drivers.Where(x => x.DriverId == item.DriverRoutTbl.DriverId).FirstOrDefault();

            }
            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(s => s.DriverRoutTbl.DriverTbl.FullName.ToString().Contains(searchString)
                                       || s.IsTemporary.ToString().Contains(searchString)
                                       || s.IsDone.ToString().Contains(searchString)
                                       || s.DriverRoutTbl.RoutTbl.Name.Contains(searchString)
                                       ).ToList();
            }
            switch (sortOrder)
            {
                case "driver_desc":
                    list = list.OrderByDescending(s => s.DriverRoutTbl.DriverTbl.FullName).ToList();
                    break;
                case "rout":
                    list = list.OrderBy(s => s.DriverRoutTbl.RoutTbl.Name).ToList();
                    break;
                case "rout_desc":
                    list = list.OrderByDescending(s => s.DriverRoutTbl.RoutTbl.Name).ToList();
                    break;
                case "isTemporary":
                    list = list.OrderBy(s => s.IsTemporary).ToList();
                    break;
                case "isTemporary_desc":
                    list = list.OrderByDescending(s => s.IsTemporary).ToList();
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

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            foreach (var item in list)
            {
                var element = BaseMapper<LogRoutDriverViewModel, LogRoutDriverTbl>.Map(item);
                element.DoDateString = element.DoDate.ToPersianDateString();
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
            LogRoutDriverTbl routDriver = _logRoutDriver.GetLogRoutDriver(id);
            if (routDriver == null)
            {
                return HttpNotFound();
            }
            common = new LogRoutDriverViewModel();
            common = BaseMapper<LogRoutDriverViewModel, LogRoutDriverTbl>.Map(routDriver);
            common.DoDateString = common.DoDate.ToPersianDateString();
            common.DoDateString = routDriver.DoDate.ToPersianDateString();
            common.DriverRoutTbl = _driverRout.GetDriverRout(common.DriverRoutId);
            common.DriverRoutTbl.DriverTbl = _driver.GetDriver(common.DriverRoutTbl.DriverId);
            common.DriverRoutTbl.RoutTbl = _rout.GetRout(common.DriverRoutTbl.RoutId);
            return View(common);
        }

        // GET: Drivers/Create
        public ActionResult Create()
        {
            common = new LogRoutDriverViewModel();
            common.DriverTblList = _driver.GetAllDrivers();
            common.RoutTblList = _rout.GetAllRouts();
            return View(common);
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LogRoutDriverViewModel routDriver)
        {
            if (ModelState.IsValid)
            {
                // common = new LogRoutDriverViewModel();
                var obj = BaseMapper<LogRoutDriverViewModel, LogRoutDriverTbl>.Map(routDriver);
                obj.IsActive = true;
                obj.CFDate = DateTime.Now;
                obj.LFDate = DateTime.Now;
                obj.DoDate = routDriver.DoDateString.ToGeorgianDate();
                   var findDriverRoutTbl = _driverRout.GetDriverRoutByDriverIdRoutId(routDriver.DriverId, routDriver.RoutId);
                obj.DriverRoutId = findDriverRoutTbl.Id;
                _logRoutDriver.AddNewLogRoutDriver(obj);
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
            LogRoutDriverTbl logRoutDriver = _logRoutDriver.GetLogRoutDriver(id);

            if (logRoutDriver == null)
            {
                return HttpNotFound();
            }
            var obj = BaseMapper<LogRoutDriverViewModel, LogRoutDriverTbl>.Map(logRoutDriver);
            obj.DriverTblList = _driver.GetAllDrivers();
            obj.RoutTblList = _rout.GetAllRouts();
            obj.DriverRoutTbl = _driverRout.GetDriverRout(obj.DriverRoutId);
            obj.DriverTbl = _driver.GetDriver(obj.DriverRoutTbl.DriverId);
            obj.RoutTbl = _rout.GetRout(obj.DriverRoutTbl.RoutId);
            obj.DriverFullName = obj.DriverRoutTbl.DriverTbl.FullName;
            obj.RoutName = obj.DriverRoutTbl.RoutTbl.Name;
            obj.DoDateString = obj.DoDate.ToPersianDateString(); 
            if (obj.WorkDoneEnum == WorkDoneEnum.Done == Convert.ToBoolean(WorkDoneEnum.Done))
            {
                obj.WorkDoneEnum = WorkDoneEnum.Done;
            }
            else
            {
                obj.WorkDoneEnum = WorkDoneEnum.Done;
            }
            return View(obj);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LogRoutDriverViewModel logRoutDriver)
        {
            if (ModelState.IsValid)
            {
                logRoutDriver.LFDate = DateTime.Now;
                logRoutDriver.IsActive = false;
                _logRoutDriver.Delete(logRoutDriver.Id);
                var obj = BaseMapper<LogRoutDriverViewModel, LogRoutDriverTbl>.Map(logRoutDriver);
                obj.CFDate = DateTime.Now;
                obj.LFDate = DateTime.Now;
                obj.CDate = DateTime.Now;
                obj.LDate = DateTime.Now;
                obj.DoDate = logRoutDriver.DoDateString.ToGeorgianDate();
                obj.IsActive = true;
                obj.IsDone = Convert.ToBoolean(logRoutDriver.WorkDoneEnum);
                obj.IsTemporary = (int) logRoutDriver.WorkTemporaryEnum;
                //var findDriver = _driver.GetDriverByName(logRoutDriver.DriverFullName);
                //var findRout = _rout.GetRoutByName(logRoutDriver.RoutName);
                obj.DriverRoutId = _driverRout.GetDriverRoutByDriverIdRoutId(logRoutDriver.DriverId, logRoutDriver.RoutId).Id;

                _logRoutDriver.AddNewLogRoutDriver(obj);
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
            LogRoutDriverTbl logRoutDriver = _logRoutDriver.GetLogRoutDriver(id);
            if (logRoutDriver == null)
            {
                return HttpNotFound();
            }
            var obj = BaseMapper<LogRoutDriverViewModel, LogRoutDriverTbl>.Map(logRoutDriver);
            obj.DriverRoutTbl = _driverRout.GetDriverRout(obj.DriverRoutId);
            obj.DriverRoutTbl.DriverTbl = _driver.GetDriver(obj.DriverRoutTbl.DriverId);
            obj.DriverRoutTbl.RoutTbl = _rout.GetRout(obj.DriverRoutTbl.RoutId);
            obj.DriverFullName = obj.DriverRoutTbl.DriverTbl.FullName;
            obj.RoutName = obj.DriverRoutTbl.RoutTbl.Name;
            obj.DoDateString = obj.DoDate.ToPersianDateString();
            return View(obj);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _logRoutDriver.Delete(id);
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
