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

        readonly IUnitOfWork _uow;
        private LogDriverRoutViewModel common { get; set; }
        private List<LogDriverRoutViewModel> commonList { get; set; }

        public LogDriverRoutController(IUnitOfWork uow, ILogDriverRoutService LogDriverRout, IRoutService rout,
            IDriverService driver, IDriverRoutService driverRout)
        {
            _driver = driver;
            _rout = rout;
            _driverRout = driverRout;
            _LogDriverRout = LogDriverRout;
            _uow = uow;
        }

        // GET: Automobile
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Driver = String.IsNullOrEmpty(sortOrder) ? "driver_desc" : "";
            ViewBag.Rout = sortOrder == "rout" ? "rout_desc" : "rout";
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
            commonList = new List<LogDriverRoutViewModel>();
            //ViewBag.CurrentFilter = searchString;
            var log = _LogDriverRout.GetAllLogDriverRouts();
            var driverRout = _driverRout.GetAllDriverRouts();
            var routs = _rout.GetAllRouts();
            var drivers = _driver.GetAllDrivers(); 
            var list = _LogDriverRout.GetAllLogDriverRouts();
            foreach (var item in list)
            {
                item.DriverRoutTbl = driverRout.Where(x => x.Id == item.DriverRoutId).FirstOrDefault();
                item.DriverRoutTbl.RoutTbl = routs.Where(x => x.Id == item.DriverRoutTbl.RoutId).FirstOrDefault();
                item.DriverRoutTbl.DriverTbl = drivers.Where(x => x.Id == item.DriverRoutTbl.DriverId).FirstOrDefault();

            }
            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(s => s.DriverRoutTbl.DriverTbl.FullName.ToString().Contains(searchString)
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
                var element = BaseMapper<LogDriverRoutViewModel, LogDriverRoutTbl>.Map(item);
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
            LogDriverRoutTbl routDriver = _LogDriverRout.GetLogDriverRout(id);
            if (routDriver == null)
            {
                return HttpNotFound();
            }
            common = new LogDriverRoutViewModel();
            common = BaseMapper<LogDriverRoutViewModel, LogDriverRoutTbl>.Map(routDriver);
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
            common = new LogDriverRoutViewModel();
            common.DriverTblList = _driver.GetAllDrivers();
            common.RoutTblList = _rout.GetAllRouts();
            return View(common);
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LogDriverRoutViewModel routDriver)
        {
            if (ModelState.IsValid)
            {
                // common = new LogDriverRoutViewModel();
                var obj = BaseMapper<LogDriverRoutViewModel, LogDriverRoutTbl>.Map(routDriver);
                obj.DoDate = routDriver.DoDateString.ToGeorgianDate();
                obj.IsDone = Convert.ToBoolean(routDriver.WorkDoneEnum);
                var findDriverRoutTbl = _driverRout.GetDriverRoutByDriverIdRoutId(routDriver.DriverId, routDriver.RoutId);
                if (findDriverRoutTbl!=null)
                {
                    obj.DriverRoutId = findDriverRoutTbl.Id;
                    _LogDriverRout.AddNewLogDriverRout(obj);
                    _uow.SaveAllChanges();
                }
               
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
            obj.RoutName = obj.DriverRoutTbl.RoutTbl.Name;
            obj.DoDateString = obj.DoDate.ToPersianDateString();
            obj.DriverId = obj.DriverTbl.Id;
            obj.RoutId= obj.RoutTbl.Id;
            if (obj.IsDone==Convert.ToBoolean(WorkDoneEnum.Done))
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
            obj.RoutName = obj.DriverRoutTbl.RoutTbl.Name;
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
