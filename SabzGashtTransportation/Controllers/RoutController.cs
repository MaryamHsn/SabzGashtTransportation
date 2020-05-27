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
using Sabz.ServiceLayer.Extension;
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

        // [Authorize(Roles = "admin , SuperViser")]
        [HttpGet]
        public ActionResult Index(RoutFullViewModel fullRout)//string SearchRout, string currentFilter, string searchString
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                    {
                        var allRegion = _region.GetAllRegions();
                        fullRout.Regions = allRegion;
                        if (ModelState.IsValid)
                        {
                            commonList = new List<RoutViewModel>();
                            int pageSize = 1000;
                            int pageNumber = (fullRout.page ?? 1);
                            var list = new List<RoutTbl>();
                            DateTime startDate = DateTime.Now.Date;
                            DateTime endDate = DateTime.Now.AddDays(1).Date;
                            ViewBag.CurrentSort = fullRout.sortOrder;
                            ViewBag.ShiftType = String.IsNullOrEmpty(fullRout.sortOrder) ? "shiftType_desc" : "";
                            ViewBag.RegionId = fullRout.sortOrder == "region" ? "region_desc" : "region";
                            ViewBag.Date = fullRout.sortOrder == "date" ? "date_desc" : "date";
                            ViewBag.EnterTime = fullRout.sortOrder == "enterTime" ? "enterTime_desc" : "enterTime";
                            ViewBag.TransactionType = fullRout.sortOrder == "transactionType" ? "transactionType_desc" : "transactionType";
                            ViewBag.AutomobileTypeBus = fullRout.sortOrder == "automobileTypeBus" ? "automobileTypeBus_desc" : "automobileTypeBus";
                            ViewBag.AutomobileTypeCooler = fullRout.sortOrder == "automobileTypeCooler" ? "automobileTypeCooler_desc" : "automobileTypeCooler";
                            ViewBag.Count = fullRout.sortOrder == "count" ? "count_desc" : "count";


                            //if (!string.IsNullOrWhiteSpace(searchDate))
                            //{
                            //    list = _rout.GetAllRoutsByDateByRegionId((DateTime)searchDate.ToGeorgianDate(), int.Parse(dropRegionId)).ToList();
                            //}
                            if (!string.IsNullOrWhiteSpace(fullRout.SearchDateFrom) && !string.IsNullOrWhiteSpace(fullRout.SearchDateTo))
                            {
                                list = _rout.GetAllRoutsByDateFromByDateToByRegionId((DateTime)fullRout.SearchDateFrom.ToGeorgianDate(), (DateTime)fullRout.SearchDateTo.ToGeorgianDate(), (int)fullRout.RegionId).ToList();
                            }
                            else if (!string.IsNullOrWhiteSpace(fullRout.SearchDateFrom) && string.IsNullOrWhiteSpace(fullRout.SearchDateTo))
                            {
                                list = _rout.GetAllRoutsByDateFromByDateToByRegionId((DateTime)fullRout.SearchDateFrom.ToGeorgianDate(), endDate, (int)fullRout.RegionId).ToList();
                            }
                            else if (string.IsNullOrWhiteSpace(fullRout.SearchDateFrom))
                            {
                                list = _rout.GetAllRoutsByDateFromByDateToByRegionId(startDate, endDate, fullRout.RegionId).ToList();
                            }
                            else
                            {
                                list = _rout.GetAllRouts().Where(x => x.RegionId == fullRout.RegionId).ToList();
                            }
                            ViewBag.TotalRoutCountBus = list.Where(x => x.AutomobileTypeTbl.IsBus == 1).Sum(x => x.Count);
                            ViewBag.TotalRoutCountMiniBus = list.Where(x => x.AutomobileTypeTbl.IsBus == 0).Sum(x => x.Count);
                            switch (fullRout.sortOrder)
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
                            var allocateRoutCountBus = 0;
                            var allocateRoutCountMiniBus = 0;
                            foreach (var item in list)
                            {
                                var element = BaseMapper<RoutViewModel, RoutTbl>.Map(item);
                                element.RegionName = allRegion.Where(x => x.Id == element.RegionId).FirstOrDefault().RegionName;
                                element.AutomobileTypeTbl = allAutomobileType.Where(x => x.Id == item.AutomobileTypeId).FirstOrDefault();
                                element.StartDateString = item.StartDate.ToPersianDateString();
                                element.Allocate = _driverRout.GetDriverRoutByRoutId(item.Id).Count;
                                element.RemainAllocate = element.Count - element.Allocate;
                                element.RoutID = item.Id;
                                element.IsBus = (int)item.AutomobileTypeTbl.IsBus;
                                commonList.Add(element);
                                if (element.IsBus == 1)
                                {
                                    allocateRoutCountBus += (int)element.Allocate;
                                }
                                else
                                {
                                    allocateRoutCountMiniBus += (int)element.Allocate;
                                }
                            }
                            ViewBag.AllocateRoutCountBus = allocateRoutCountBus;
                            ViewBag.AllocateRoutCountMiniBus = allocateRoutCountMiniBus;
                            fullRout.RoutViewModels = commonList.OrderBy(x => x.StartDate).ThenBy(x => x.EnterTime).ToPagedList(pageNumber, pageSize);
                            return View(fullRout);
                        }
                        return View(fullRout);
                    }
                }
                return RedirectToAction("login", "Account");
            }
            catch (Exception)
            {
                return View();
            }
        }

        // [Authorize(Roles = "admin , SuperViser")]
        public ActionResult Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
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
            }
            return RedirectToAction("login", "Account");
        }

        // [Authorize(Roles = "admin , SuperViser")]
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    common = new RoutViewModel()
                    {
                        RegionTblList = _region.GetAllRegions()
                    };
                    return View(common);
                }
            }
            return RedirectToAction("login", "Account");
        }

        // [Authorize(Roles = "admin , SuperViser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoutViewModel rout)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                    {
                        if (ModelState.IsValid)
                        {
                            rout.AutomobileTypeId = _automobileType.GetAutomobileTypeByCoolerBus((int)rout.HasCoolerEnum, (int)rout.IsBusEnum).Id;
                            if (rout.RegionId == 0)
                            {
                                return RedirectToAction("Index");
                            }
                            if (rout.StartDateString == null)
                                return RedirectToAction("Index");
                            else
                            {
                                rout.StartDate = rout.StartDateString.ToGeorgianDate();
                            }
                            //rout.EndDate = rout.EndDateString.ToGeorgianDate();
                            rout.IsActive = true;
                            var obj = BaseMapper<RoutTbl, RoutViewModel>.Map(rout);
                            //if (rout.RoutID != 0)
                            //    obj.Id = rout.RoutID;
                            obj.RoutTransactionType = (int)rout.RoutTransactionTypeEnum;
                            obj.ShiftType = (int)rout.ShiftTypeEnum;
                            _rout.AddNewRout(obj);
                            _uow.SaveAllChanges();
                            return RedirectToAction("Index");
                        }
                        return View();
                    }
                }
                return RedirectToAction("login", "Account");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        //[Authorize(Roles = "admin , SuperViser")]
        public ActionResult Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
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
                    obj.StartDateString = ((DateTime)(obj.StartDate)).ToPersianDateString();
                    obj.AutomobileTypeTbl = _automobileType.GetAutomobileType(obj.AutomobileTypeId);
                    obj.RoutID = rout.Id;
                    obj.ShiftType = rout.ShiftType;
                    if (obj.ShiftType == (int)ShiftTypeEnum.Enter)
                    {
                        obj.ShiftTypeEnum = ShiftTypeEnum.Enter;
                    }
                    else if (obj.ShiftType == (int)ShiftTypeEnum.Exit)
                    {
                        obj.ShiftTypeEnum = ShiftTypeEnum.Exit;
                    }
                    if (obj.RoutTransactionType == (int)RoutTransactionTypeEnum.Single)
                    {
                        obj.RoutTransactionTypeEnum = RoutTransactionTypeEnum.Single;
                    }
                    else if (obj.RoutTransactionType == (int)RoutTransactionTypeEnum.Regular)
                    {
                        obj.RoutTransactionTypeEnum = RoutTransactionTypeEnum.Regular;
                    }
                    else if (obj.RoutTransactionType == (int)RoutTransactionTypeEnum.ThereeFour)
                    {
                        obj.RoutTransactionTypeEnum = RoutTransactionTypeEnum.ThereeFour;
                    }
                    else if (obj.RoutTransactionType == (int)RoutTransactionTypeEnum.FiveSeven)
                    {
                        obj.RoutTransactionTypeEnum = RoutTransactionTypeEnum.FiveSeven;
                    }
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
            }
            return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoutViewModel rout)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    rout.RegionTblList = _region.GetAllRegions();
                    if (ModelState.IsValid)
                    {
                        var obj = BaseMapper<RoutTbl, RoutViewModel>.Map(rout);
                        if (rout.StartDateString != null)
                            obj.StartDate = rout.StartDateString.ToGeorgianDate();
                        obj.RoutTransactionType = (int)rout.RoutTransactionTypeEnum;
                        obj.ShiftType = (int)rout.ShiftTypeEnum;
                        obj.IsActive = true;
                        obj.AutomobileTypeId = _automobileType.GetAutomobileTypeByCoolerBus((int)rout.HasCoolerEnum, (int)rout.IsBusEnum).Id;
                        if (rout.RoutID != 0)
                            obj.Id = rout.RoutID;
                        _rout.UpdateRout(obj);
                        _uow.SaveAllChanges();
                        return RedirectToAction("Index");
                    }
                    return View(rout);
                }
            }
            return RedirectToAction("login", "Account");
        }

        // [Authorize(Roles = "admin , SuperViser")]
        public ActionResult Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
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
            }
            return RedirectToAction("login", "Account");
        }

        // [Authorize(Roles = "admin , SuperViser")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    _rout.Delete(id);
                    _uow.SaveAllChanges();
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("login", "Account");
        }

        [HttpPost]
        //[Authorize(Roles = "admin , SuperViser")]
        [ValidateAntiForgeryToken]
        public ActionResult SearchByDate(DateTime? fromDate)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    if (fromDate == null)
                    {
                        fromDate = DateTime.Now.Date;
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
            }
            return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        //public ActionResult FullInformation(string searchDateFrom, string searchDateTo, string dropRegionId, int? page)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        if (User.IsInRole("Admin"))
        //        {
        //            commonList = new List<RoutViewModel>();
        //            int pageSize = 10;
        //            int pageNumber = (page ?? 1);

        //            IEnumerable<SelectListItem> regionItems = _region.GetAllRegions().Select(c => new SelectListItem
        //            {
        //                Value = c.Id.ToString(),
        //                Text = c.RegionName
        //            });
        //            if (!string.IsNullOrWhiteSpace(dropRegionId))
        //            {
        //                ViewBag.Region = regionItems.Where(x => x.Value == dropRegionId).FirstOrDefault().Text;
        //                // TempData["Region"] = regionItems.Where(x => x.Value == dropRegionId).FirstOrDefault().Text;
        //            }
        //            else
        //            {
        //                dropRegionId = ViewBag.Region != null ? ViewBag.Rgion : "0";
        //                //dropRegionId = TempData["Region"] != null ? TempData["Region"].ToString() : "0";
        //            }
        //            ViewBag.RegionItems = regionItems;
        //            DateTime startDate = DateTime.Now.Date;
        //            DateTime endDate = DateTime.Now.AddDays(1).Date;
        //            var allRegion = _region.GetAllRegions();
        //            var routs = new List<RoutTbl>();

        //            if (!string.IsNullOrWhiteSpace(searchDateFrom) && string.IsNullOrWhiteSpace(searchDateTo))
        //            {
        //                routs = _rout.GetAllRoutsByDateFromByDateToByRegionId((DateTime)searchDateFrom.ToGeorgianDate(), endDate, int.Parse(dropRegionId)).ToList();
        //            }
        //            else if (string.IsNullOrWhiteSpace(searchDateFrom))
        //            {
        //                routs = _rout.GetAllRoutsByDateFromByDateToByRegionId(startDate, endDate, int.Parse(dropRegionId)).ToList();
        //            }
        //            else if (!string.IsNullOrWhiteSpace(searchDateFrom) && !string.IsNullOrWhiteSpace(searchDateTo))
        //            {
        //                routs = _rout.GetAllRoutsByDateFromByDateToByRegionId((DateTime)searchDateFrom.ToGeorgianDate(), (DateTime)searchDateTo.ToGeorgianDate(), int.Parse(dropRegionId)).ToList();
        //            }
        //            if (routs == null)
        //            {
        //                return HttpNotFound();
        //            } 
        //            foreach (var item in routs.GroupBy(x => x.StartDate.Date))
        //            {
        //                common = new RoutViewModel();
        //                common.StartDateString = item.Select(x => x.StartDate).FirstOrDefault().ToPersianDateString();
        //                common.StartDate = item.Select(x => x.StartDate).FirstOrDefault();
        //                common.RoutTransactionSingleBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.Single && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasNotCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.Bus).Sum(x => x.Count);
        //                common.RoutTransactionSingleMiniBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.Single && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasNotCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.MiniBus).Sum(x => x.Count);
        //                common.RoutTransactionSingleHasCoolerBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.Single && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.Bus).Sum(x => x.Count);
        //                common.RoutTransactionSingleHasCoolerMiniBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.Single && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.MiniBus).Sum(x => x.Count);
        //                common.RoutTransactionRegularBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.Regular && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasNotCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.Bus).Sum(x => x.Count);
        //                common.RoutTransactionRegularMiniBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.Regular && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasNotCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.MiniBus).Sum(x => x.Count);
        //                common.RoutTransactionRegularHasCoolerBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.Regular && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.Bus).Sum(x => x.Count);
        //                common.RoutTransactionRegularHasCoolerMiniBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.Regular && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.MiniBus).Sum(x => x.Count);
        //                common.RoutTransactionThereeFourBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.ThereeFour && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasNotCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.Bus).Sum(x => x.Count);
        //                common.RoutTransactionThereeFourMiniBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.ThereeFour && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasNotCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.MiniBus).Sum(x => x.Count);
        //                common.RoutTransactionThereeFourHasCoolerBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.ThereeFour && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.Bus).Sum(x => x.Count);
        //                common.RoutTransactionThereeFourHasCoolerMiniBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.ThereeFour && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.MiniBus).Sum(x => x.Count);
        //                common.RoutTransactionFiveSevenBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.FiveSeven && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasNotCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.Bus).Sum(x => x.Count);
        //                common.RoutTransactionFiveSevenMiniBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.FiveSeven && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasNotCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.MiniBus).Sum(x => x.Count);
        //                common.RoutTransactionFiveSevenHasCoolerBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.FiveSeven && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.Bus).Sum(x => x.Count);
        //                common.RoutTransactionFiveSevenHasCoolerMiniBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.FiveSeven && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.MiniBus).Sum(x => x.Count);
        //                commonList.Add(common);

        //            }
        //            return View(commonList.OrderBy(x => x.StartDate).ToPagedList(pageNumber, pageSize));
        //        }
        //    }
        //    return RedirectToAction("login", "Account");
        //}

        public ActionResult FullInformation(RoutFullViewModel fullRout)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                    {
                        var allRegion = _region.GetAllRegions();
                        fullRout.Regions = allRegion;
                        if (ModelState.IsValid)
                        {
                            commonList = new List<RoutViewModel>();
                            int pageSize = 1000;
                            int pageNumber = (fullRout.page ?? 1);
                            DateTime startDate = DateTime.Now.Date;
                            DateTime endDate = DateTime.Now.AddDays(1).Date;
                            var routs = new List<RoutTbl>();
                            if (!string.IsNullOrWhiteSpace(fullRout.SearchDateFrom) && string.IsNullOrWhiteSpace(fullRout.SearchDateTo))
                            {
                                routs = _rout.GetAllRoutsByDateFromByDateToByRegionId((DateTime)fullRout.SearchDateFrom.ToGeorgianDate(), endDate, fullRout.RegionId).ToList();
                            }
                            else if (string.IsNullOrWhiteSpace(fullRout.SearchDateFrom))
                            {
                                routs = _rout.GetAllRoutsByDateFromByDateToByRegionId(startDate, endDate, fullRout.RegionId).ToList();
                            }
                            else if (!string.IsNullOrWhiteSpace(fullRout.SearchDateFrom) && !string.IsNullOrWhiteSpace(fullRout.SearchDateTo))
                            {
                                routs = _rout.GetAllRoutsByDateFromByDateToByRegionId((DateTime)fullRout.SearchDateFrom.ToGeorgianDate(), (DateTime)fullRout.SearchDateTo.ToGeorgianDate(), fullRout.RegionId).ToList();
                            }
                            if (routs == null)
                            {
                                return HttpNotFound();
                            }
                            foreach (var item in routs.GroupBy(x => x.StartDate.Date))
                            {
                                common = new RoutViewModel();
                                common.StartDateString = item.Select(x => x.StartDate).FirstOrDefault().ToPersianDateString();
                                common.StartDate = item.Select(x => x.StartDate).FirstOrDefault();
                                common.RoutTransactionSingleBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.Single && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasNotCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.Bus).Sum(x => x.Count);
                                common.RoutTransactionSingleMiniBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.Single && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasNotCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.MiniBus).Sum(x => x.Count);
                                common.RoutTransactionSingleHasCoolerBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.Single && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.Bus).Sum(x => x.Count);
                                common.RoutTransactionSingleHasCoolerMiniBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.Single && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.MiniBus).Sum(x => x.Count);
                                common.RoutTransactionRegularBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.Regular && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasNotCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.Bus).Sum(x => x.Count);
                                common.RoutTransactionRegularMiniBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.Regular && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasNotCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.MiniBus).Sum(x => x.Count);
                                common.RoutTransactionRegularHasCoolerBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.Regular && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.Bus).Sum(x => x.Count);
                                common.RoutTransactionRegularHasCoolerMiniBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.Regular && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.MiniBus).Sum(x => x.Count);
                                common.RoutTransactionThereeFourBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.ThereeFour && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasNotCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.Bus).Sum(x => x.Count);
                                common.RoutTransactionThereeFourMiniBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.ThereeFour && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasNotCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.MiniBus).Sum(x => x.Count);
                                common.RoutTransactionThereeFourHasCoolerBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.ThereeFour && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.Bus).Sum(x => x.Count);
                                common.RoutTransactionThereeFourHasCoolerMiniBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.ThereeFour && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.MiniBus).Sum(x => x.Count);
                                common.RoutTransactionFiveSevenBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.FiveSeven && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasNotCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.Bus).Sum(x => x.Count);
                                common.RoutTransactionFiveSevenMiniBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.FiveSeven && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasNotCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.MiniBus).Sum(x => x.Count);
                                common.RoutTransactionFiveSevenHasCoolerBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.FiveSeven && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.Bus).Sum(x => x.Count);
                                common.RoutTransactionFiveSevenHasCoolerMiniBus = item.Where(x => x.RoutTransactionType == (int)RoutTransactionTypeEnum.FiveSeven && x.AutomobileTypeTbl.HasCooler == Convert.ToBoolean(HasCoolerEnum.HasCooler) && x.AutomobileTypeTbl.IsBus == (int)AutomobileTypeEnum.MiniBus).Sum(x => x.Count);
                                commonList.Add(common);

                            }
                             fullRout.RoutViewModels = commonList.OrderBy(x => x.StartDate).ThenBy(x => x.EnterTime).ToPagedList(pageNumber, pageSize);
                        }
                        return View(fullRout);
                    }
                }
                return RedirectToAction("login", "Account");
            }
            catch (Exception)
            {

                throw;
            }
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
