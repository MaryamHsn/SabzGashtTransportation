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
using Sabz.ServiceLayer.Utils;
using Sabz.ServiceLayer.ViewModel;
using SabzGashtTransportation.ViewModel;

namespace SabzGashtTransportation.Controllers
{
    public class RoutController : Controller
    {
        readonly IRoutService _rout;
        readonly IRegionService _region;
        readonly IAutomobileTypeService _automobileType;
        readonly IDriverRoutService _driverRout;

        readonly IUnitOfWork _uow;
        private RoutViewModel common { get; set; }
        private List<RoutViewModel> commonList { get; set; }

        public RoutController(IUnitOfWork uow, IRoutService rout, IRegionService region, IAutomobileTypeService automobileType, IDriverRoutService driverRout)
        {
            _driverRout = driverRout;
            _automobileType = automobileType;
            _region = region;
            _rout = rout;
            _uow = uow;
        }

        // GET: Automobile
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            commonList= new List<RoutViewModel>();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ShiftType = String.IsNullOrEmpty(sortOrder) ? "shiftType_desc" : "";
            ViewBag.Name = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.EnterTime = sortOrder == "enterTime" ? "enterTime_desc" : "enterTime";
            ViewBag.ExitTime = sortOrder == "exitTime" ? "exitTime_desc" : "exitTime";
            ViewBag.RegionId = sortOrder == "region" ? "region_desc" : "region";
            ViewBag.AutomobileTypeBus = sortOrder == "automobileTypeBus" ? "automobileTypeBus_desc" : "automobileTypeBus";
            ViewBag.AutomobileTypeCooler = sortOrder == "automobileTypeCooler" ? "automobileTypeCooler_desc" : "automobileTypeCooler";
            ViewBag.Count = sortOrder == "count" ? "count_desc" : "count";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var list = _rout.GetAllRouts();
            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(s => s.ShiftType.ToString().Contains(searchString)
                                       || s.EnterTime.ToString().Contains(searchString)
                                       || s.Name.ToString().Contains(searchString)
                                       || s.ExitTime.ToString().Contains(searchString)
                                       || s.RegionTbl.RegionName.Contains(searchString)
                                       || s.AutomobileTypeTbl.HasCooler.ToString().Contains(searchString)
                                       || s.AutomobileTypeTbl.IsBus.ToString().Contains(searchString)
                                       || s.Count.ToString().Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "shiftType_desc":
                    list = list.OrderByDescending(s => s.ShiftType).ToList();
                    break;
                case "enterTime":
                    list = list.OrderBy(s => s.EnterTime).ToList();
                    break;
                case "enterTime_desc":
                    list = list.OrderByDescending(s => s.EnterTime).ToList();
                    break;
                case "exitTime":
                    list = list.OrderBy(s => s.ExitTime).ToList();
                    break;
                case "exitTime_desc":
                    list = list.OrderByDescending(s => s.ExitTime).ToList();
                    break;
                case "region":
                    list = list.OrderBy(s => s.RegionTbl.RegionName).ToList();
                    break;
                case "region_desc":
                    list = list.OrderByDescending(s => s.RegionTbl.RegionName).ToList();
                    break;
                case "automobileTypeBus":
                    list = list.OrderBy(s => s.AutomobileTypeTbl.IsBus).ToList();
                    break;
                case "automobileTypeBus_desc":
                    list = list.OrderByDescending(s => s.AutomobileTypeTbl.IsBus).ToList();
                    break;
                case "automobileTypeCooler":
                    list = list.OrderBy(s => s.AutomobileTypeTbl.HasCooler).ToList();
                    break;
                case "automobileTypeCooler_desc":
                    list = list.OrderByDescending(s => s.AutomobileTypeTbl.HasCooler).ToList();
                    break;
                case "count":
                    list = list.OrderBy(s => s.Count).ToList();
                    break;
                case "count_desc":
                    list = list.OrderByDescending(s => s.Count).ToList();
                    break;
                case "name":
                    list = list.OrderBy(s => s.Name).ToList();
                    break;
                case "name_desc":
                    list = list.OrderByDescending(s => s.Name).ToList();
                    break;
                default:
                    list = list.OrderBy(s => s.ShiftType).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var allRegion= _region.GetAllRegions();
            var allAutomobileType = _automobileType.GetAllAutomobileTypes();
            foreach (var item in list)
            {
                var element = BaseMapper<RoutViewModel, RoutTbl>.Map(item);
                element.RegionName = allRegion.Where(x => x.RegionId== element.RegionId).FirstOrDefault().RegionName;
                element.AutomobileTypeTbl = allAutomobileType.Where(x => x.AutoTypeId == item.AutomobileTypeId).FirstOrDefault();
                //element.AutomobileTypeTbl = _automobileType.GetAutomobileTypeByCoolerBus(item.AutomobileTypeTbl.HasCooler, (int)item.AutomobileTypeTbl.IsBus);
                element.Allocate = _driverRout.GetDriverRoutByRoutId(item.RoutID).Count;
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
            RoutTbl rout= _rout.GetRout(id);
            if (rout == null)
            {
                return HttpNotFound();
            }
            common = new RoutViewModel();
            common = BaseMapper<RoutViewModel, RoutTbl>.Map(rout);
            common.CreatedDateString = rout.CreatedDate.ToPersianDateString();
            common.ModifiedDateString = ((DateTime)rout.ModifiedDate).ToPersianDateString();
            common.StartDateString = rout.EndDate != null ? ConvertDate.ToPersianDateString((DateTime)rout.StartDate) : "";
            common.EndDateString = rout.EndDate!= null?ConvertDate.ToPersianDateString((DateTime)rout.EndDate) :"";
            common.AutomobileTypeTbl = _automobileType.GetAutomobileType(common.AutomobileTypeId);
            common.RegionName = _region.GetRegion(common.RegionId).RegionName;
            return View(common);
        }

        // GET: Drivers/Create
        public ActionResult Create()
        {
            common = new RoutViewModel()
            {
                RegionTblList = _region.GetAllRegions() 
            };
            return View(common);
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoutViewModel rout)
        {
            if (ModelState.IsValid)
            {
                rout.AutomobileTypeId = _automobileType.GetAutomobileTypeByCoolerBus(rout.HasCooler, rout.IsBus).AutoTypeId;
                rout.StartDate = rout.StartDateString.ToGeorgianDate();
                rout.EndDate = rout.EndDateString.ToGeorgianDate();
                rout.IsActive = true;
                rout.CreatedDate = DateTime.Now;
                rout.ModifiedDate = DateTime.Now;
                var obj = BaseMapper<RoutTbl, RoutViewModel>.Map(rout);
                _rout.AddNewRout(obj);
                
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
            RoutTbl rout= _rout.GetRout(id);

            if (rout == null)
            {
                return HttpNotFound();
            }
            var obj = BaseMapper<RoutViewModel, RoutTbl>.Map(rout);
            obj.RegionTblList = _region.GetAllRegions();
            obj.StartDateString = obj.StartDate.ToPersianDateString();
            obj.EndDateString= obj.EndDate.ToPersianDateString();
            obj.AutomobileTypeTbl = _automobileType.GetAutomobileType(obj.AutomobileTypeId);
            if (obj.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasCooler))
            {
                obj.HasCoolerEnum = HasCoolerEnum.HasCooler;
            }
            else
            {
                obj.HasCoolerEnum = HasCoolerEnum.HasNotCooler;
            }
            if (obj.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.Bus)
            {
                obj.IsBusEnum = AutomobileTypeEnum.Bus;
            }
            else
            {
                obj.IsBusEnum = AutomobileTypeEnum.MiniBus;
            }


            return View(obj);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoutViewModel rout)
        {
            if (ModelState.IsValid)
            {
                rout.ModifiedDate = DateTime.Now;
                rout.IsActive = false;
                _rout.Delete(rout.RoutID);
                var obj = BaseMapper<RoutTbl, RoutViewModel>.Map(rout);
                obj.CreatedDate = DateTime.Now;
                obj.ModifiedDate = DateTime.Now;
                obj.StartDate = rout.StartDateString.ToGeorgianDate();
                obj.EndDate= rout.EndDateString.ToGeorgianDate();
                obj.IsActive = true;
                obj.AutomobileTypeId = _automobileType.GetAutomobileTypeByCoolerBus((int)rout.HasCoolerEnum,(int) rout.IsBusEnum).AutoTypeId;

                _rout.AddNewRout(obj);
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
            RoutTbl rout = _rout.GetRout(id);
            if (rout == null)
            {
                return HttpNotFound();
            }
            var obj = BaseMapper<RoutTbl, RoutViewModel>.Map(rout);
            obj.StartDateString = obj.StartDate != null ? ConvertDate.ToPersianDateString((DateTime) obj.StartDate) : "";
            obj.EndDateString = obj.EndDate != null ? ConvertDate.ToPersianDateString((DateTime)obj.EndDate) : "";
            obj.RegionName = rout.RegionTbl != null
                ? rout.RegionTbl.RegionName : _region.GetRegion(rout.RegionId).RegionName;
            return View(obj);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _rout.Delete(id);
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
