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
        public ActionResult Index(string sortOrder, string searchDateFrom,string searchDateTo,string dropRegionId, int? page)//string SearchRout, string currentFilter, string searchString
        {
            commonList = new List<RoutViewModel>();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            IEnumerable<SelectListItem> regionItems = _region.GetAllRegions().Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.RegionName

            });
            ViewBag.RegionItems = regionItems;
            ViewBag.JobTitle = regionItems;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ShiftType = String.IsNullOrEmpty(sortOrder) ? "shiftType_desc" : "";
            //ViewBag.Name = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.RegionId = sortOrder == "region" ? "region_desc" : "region";
            ViewBag.Date = sortOrder == "date" ? "date_desc" : "date";
            ViewBag.EnterTime = sortOrder == "enterTime" ? "enterTime_desc" : "enterTime";
            ViewBag.TransactionType = sortOrder == "transactionType" ? "transactionType_desc" : "transactionType";
            ViewBag.AutomobileTypeBus = sortOrder == "automobileTypeBus" ? "automobileTypeBus_desc" : "automobileTypeBus";
            ViewBag.AutomobileTypeCooler = sortOrder == "automobileTypeCooler" ? "automobileTypeCooler_desc" : "automobileTypeCooler";
            ViewBag.Count = sortOrder == "count" ? "count_desc" : "count";
            var allRegion = _region.GetAllRegions();
            if (string.IsNullOrEmpty(dropRegionId))
            {
                //dropRegionId = allRegion.FirstOrDefault().Id.ToString();
                dropRegionId = "0";
            }
            var list = _rout.GetAllRouts().Where(x=>x.RegionId== int.Parse(dropRegionId));
            //if (!string.IsNullOrWhiteSpace(searchDate))
            //{
            //    list = _rout.GetAllRoutsByDateByRegionId((DateTime)searchDate.ToGeorgianDate(), int.Parse(dropRegionId)).ToList();
            //}
            if (!string.IsNullOrWhiteSpace(searchDateFrom) && !string.IsNullOrWhiteSpace(searchDateTo))
            {
                list = _rout.GetAllRoutsByDateFromByDateToByRegionId((DateTime)searchDateFrom.ToGeorgianDate(), (DateTime)searchDateTo.ToGeorgianDate(), int.Parse(dropRegionId)).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(searchDateFrom) && string.IsNullOrWhiteSpace(searchDateTo))
            {
                list = _rout.GetAllRoutsByDateFromByDateToByRegionId((DateTime)searchDateFrom.ToGeorgianDate(), DateTime.Now, int.Parse(dropRegionId)).ToList();
            } 

            ViewBag.TotalRoutCount = list.Sum(x => x.Count);
            #region
            //if (!string.IsNullOrWhiteSpace(SearchDate) && !string.IsNullOrWhiteSpace(SearchRout))
            //{
            //    //list = _rout.GetAllRoutsByDateByRoutName(SearchDate.ToGeorgianDate(), SearchRout).ToList();
            //}
            //else if (!string.IsNullOrWhiteSpace(SearchDate) && string.IsNullOrWhiteSpace(SearchRout))
            //{
            //    list = _rout.GetAllRoutsByDate((DateTime)SearchDate.ToGeorgianDate()).ToList();

            //}
            //else if (string.IsNullOrWhiteSpace(SearchDate) && !string.IsNullOrWhiteSpace(SearchRout))
            //{
            //    //list = _rout.GetAllRoutsByRoutName(SearchRout).ToList();

            //}
            //if (searchString != null)
            //{
            //    page = 1;
            //}
            //else
            //{
            //    searchString = currentFilter;
            //}

            //ViewBag.CurrentFilter = searchString;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    list = list.Where(s => s.ShiftType.ToString().Contains(searchString)
            //                           || s.EnterTime.ToString().Contains(searchString)
            //                           || s.Name.ToString().Contains(searchString)
            //                           //|| s.ExitTime.ToString().Contains(searchString)
            //                           //|| s.StartDate.ToString().Contains(searchString)
            //                           || s.RegionTbl.RegionName.Contains(searchString)
            //                           || s.AutomobileTypeTbl.HasCooler.ToString().Contains(searchString)
            //                           || s.AutomobileTypeTbl.IsBus.ToString().Contains(searchString)
            //                           || s.Count.ToString().Contains(searchString)).ToList();
            //}
            #endregion
            switch (sortOrder)
            {
                case "shiftType_desc":
                    list = list.OrderByDescending(s => s.ShiftType).ToList();
                    break;
                case "region":
                    list = list.OrderBy(s => s.RegionTbl.RegionName).ToList();
                    break;
                case "region_desc":
                    list = list.OrderByDescending(s => s.RegionTbl.RegionName).ToList();
                    break;
                case "date":
                    list = list.OrderBy(s => s.StartDate).ToList();
                    break;
                case "date_desc":
                    list = list.OrderByDescending(s => s.StartDate).ToList();
                    break;
                case "enterTime":
                    list = list.OrderBy(s => s.EnterTime).ToList();
                    break;
                case "enterTime_desc":
                    list = list.OrderByDescending(s => s.EnterTime).ToList();
                    break;
                case "transactionType":
                    list = list.OrderBy(s => s.RoutTransactionType).ToList();
                    break;
                case "transactionType_desc":
                    list = list.OrderByDescending(s => s.RoutTransactionType).ToList();
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
                default:
                    list = list.OrderBy(s => s.Id).ToList();
                    break;
            }
            
            var allAutomobileType = _automobileType.GetAllAutomobileTypes();
            var allocateRoutCount = 0;
            foreach (var item in list)
            {
                var element = BaseMapper<RoutViewModel, RoutTbl>.Map(item);
                element.RegionName = allRegion.Where(x => x.Id == element.RegionId).FirstOrDefault().RegionName;
                element.AutomobileTypeTbl = allAutomobileType.Where(x => x.Id == item.AutomobileTypeId).FirstOrDefault();
                //element.AutomobileTypeTbl = _automobileType.GetAutomobileTypeByCoolerBus(item.AutomobileTypeTbl.HasCooler, (int)item.AutomobileTypeTbl.IsBus);
                element.StartDateString = item.StartDate.ToPersianDateString();
                element.Allocate = _driverRout.GetDriverRoutByRoutId(item.Id).Count;
                element.RoutID = item.Id;
                commonList.Add(element);
                allocateRoutCount += element.Allocate;

            }
            ViewBag.AllocateRoutCount = allocateRoutCount;
            return View(commonList.ToPagedList(pageNumber, pageSize));
        }

        // GET: Drivers/Details/5
        public ActionResult Details(int? id)
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
            common = new RoutViewModel();
            common = BaseMapper<RoutViewModel, RoutTbl>.Map(rout);
            //common.CreatedDateString = rout.CreatedDate.ToPersianDateString();
            //common.ModifiedDateString = ((DateTime)rout.ModifiedDate).ToPersianDateString();
            common.StartDateString = ConvertDate.ToPersianDateString((DateTime)rout.StartDate);
            common.AutomobileTypeTbl = _automobileType.GetAutomobileType(common.AutomobileTypeId);
            common.RegionName = _region.GetRegion(common.RegionId).RegionName;
            common.RoutID = rout.Id;
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
                rout.AutomobileTypeId = _automobileType.GetAutomobileTypeByCoolerBus(rout.HasCooler, rout.IsBus).Id;
                rout.StartDate = rout.StartDateString.ToGeorgianDate();
                //rout.EndDate = rout.EndDateString.ToGeorgianDate();
                rout.IsActive = true;
                var obj = BaseMapper<RoutTbl, RoutViewModel>.Map(rout);
                obj.Id = rout.RoutID;
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
            RoutTbl rout = _rout.GetRout(id);
            if (rout == null)
            {
                return HttpNotFound();
            }
            var obj = BaseMapper<RoutViewModel, RoutTbl>.Map(rout);
            obj.RegionTblList = _region.GetAllRegions();
            obj.StartDateString = obj.StartDate.ToPersianDateString();
            obj.AutomobileTypeTbl = _automobileType.GetAutomobileType(obj.AutomobileTypeId);
            obj.ShiftType = rout.ShiftType;
            obj.RoutID = rout.Id;
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
                var obj = BaseMapper<RoutTbl, RoutViewModel>.Map(rout);
                obj.StartDate = rout.StartDateString.ToGeorgianDate();
                //obj.EndDate= rout.EndDateString.ToGeorgianDate();
                obj.IsActive = true;
                obj.AutomobileTypeId = _automobileType.GetAutomobileTypeByCoolerBus((int)rout.HasCoolerEnum, (int)rout.IsBusEnum).Id;
                obj.Id = rout.RoutID;
                _rout.UpdateRout(obj);
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
            obj.StartDateString = obj.StartDate != null ? ConvertDate.ToPersianDateString((DateTime)obj.StartDate) : "";
            //obj.EndDateString = obj.EndDate != null ? ConvertDate.ToPersianDateString((DateTime)obj.EndDate) : "";
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchByDate(DateTime? fromDate)

        {
            if (fromDate == null)
            {
                fromDate = DateTime.Now;
            }
            //if (toDate == null)
            //{
            //    toDate = fromDate;
            //}
            var rout = _rout.GetAllRoutsByDate(fromDate);
            if (rout == null)
            {
                return HttpNotFound();
            }
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
