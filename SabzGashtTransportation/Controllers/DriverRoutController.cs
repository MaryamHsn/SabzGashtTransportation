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
    public class DriverRoutController : Controller
    {
        readonly IDriverRoutService _driverRout;
        readonly IDriverService _driver;
        readonly IRoutService _rout;
        readonly IRegionService _region;

        private DriverRoutViewModel common;
        private List<DriverRoutViewModel> commonList;

        readonly IUnitOfWork _uow;
        public DriverRoutController(IUnitOfWork uow, IDriverRoutService driverRout, IDriverService driver, IRoutService rout, IRegionService region)
        {
            _region = region;
            _driver = driver;
            _rout = rout;
            _driverRout = driverRout;
            _uow = uow;
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, string SearchDriver, string dropRegionId, string SearchDateFrom, string SearchDateTo, int? page)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    commonList = new List<DriverRoutViewModel>();
                    IEnumerable<SelectListItem> regionItems = _region.GetAllRegions().Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.RegionName
                    });

                    if (!string.IsNullOrWhiteSpace(dropRegionId))
                    {
                        //if (TempData["Region"] == null)
                        //{
                        //    TempData["Region"] = regionItems.Where(x => x.Value == dropRegionId).FirstOrDefault().Text;
                        //}
                        ViewBag.Region = regionItems.Where(x => x.Value == dropRegionId).FirstOrDefault().Text;
                        // TempData["Region"] = regionItems.Where(x => x.Value == dropRegionId).FirstOrDefault().Text;

                    }
                    else
                    {
                        //dropRegionId = allRegion.FirstOrDefault().Id.ToString();
                        dropRegionId = ViewBag.Region != null ? ViewBag.Rgion : "0";
                        // dropRegionId = TempData["Region"] != null ? TempData["Region"].ToString() : "0";
                    }
                    ViewBag.RegionItems = regionItems;
                    ViewBag.CurrentSort = sortOrder;
                    ViewBag.Driver = String.IsNullOrEmpty(sortOrder) ? "driver_desc" : "";
                    ViewBag.RoutTransaction = sortOrder == "routTransaction" ? "routTransaction_desc" : "routTransaction";
                    ViewBag.RoutShiftType = sortOrder == "routShiftType" ? "routShiftType_desc" : "routShiftType";
                    ViewBag.RoutRegion = sortOrder == "routRegion" ? "routRegion_desc" : "routRegion";
                    ViewBag.RoutTime = sortOrder == "routTime" ? "routTime_desc" : "routTime";
                    ViewBag.RoutDate = sortOrder == "routDate" ? "routDate_desc" : "routDate";
                    //  ViewBag.IsTemporary = sortOrder == "isTemporary" ? "isTemporary_desc" : "isTemporary";

                    //if (searchString != null)
                    //{
                    //    page = 1;
                    //}
                    //else
                    //{
                    //    searchString = currentFilter;
                    //}
                    //ViewBag.CurrentFilter = searchString;
                    DateTime startDate = DateTime.Now.Date;
                    DateTime endDate = DateTime.Now.AddDays(1).Date;
                    var list = new List<DriverRoutTbl>();
                    var routs = _rout.GetAllRouts();
                    var regions = _region.GetAllRegions();
                    if (!string.IsNullOrWhiteSpace(SearchDateFrom) && !string.IsNullOrWhiteSpace(SearchDriver))
                    {
                        if (string.IsNullOrWhiteSpace(SearchDateTo))
                        {
                            list = _driverRout.GetDriverRoutByDateByDriverNameByRegionId(SearchDateFrom.ToGeorgianDate(), endDate, SearchDriver, int.Parse(dropRegionId));
                        }
                        else
                        {
                            list = _driverRout.GetDriverRoutByDateByDriverNameByRegionId(SearchDateFrom.ToGeorgianDate(), SearchDateTo.ToGeorgianDate(), SearchDriver, int.Parse(dropRegionId));
                        }
                    }
                    else if (!string.IsNullOrWhiteSpace(SearchDateFrom) && string.IsNullOrWhiteSpace(SearchDriver))
                    {
                        if (string.IsNullOrWhiteSpace(SearchDateTo))
                        {
                            list = _driverRout.GetDriverRoutByDateByRegionId(SearchDateFrom.ToGeorgianDate(), endDate, int.Parse(dropRegionId));
                        }
                        else
                        {
                            list = _driverRout.GetDriverRoutByDateByRegionId(SearchDateFrom.ToGeorgianDate(), SearchDateTo.ToGeorgianDate(), int.Parse(dropRegionId));
                        }
                    }
                    else if (string.IsNullOrWhiteSpace(SearchDateFrom) && !string.IsNullOrWhiteSpace(SearchDriver))
                    {
                        list = _driverRout.GetDriverRoutByDateByDriverNameByRegionId(startDate, endDate, SearchDriver, int.Parse(dropRegionId));
                    }
                    else if (string.IsNullOrWhiteSpace(SearchDateFrom) && string.IsNullOrWhiteSpace(SearchDriver))
                    {
                        list = _driverRout.GetDriverRoutByDateByRegionId(startDate, endDate, int.Parse(dropRegionId));
                    }
                    else
                    {
                        list = _driverRout.GetAllDriverRouts().ToList();
                    }
                    foreach (var item in list)
                    {
                        //var t= regions.Where(x=>x.RoutTbls.)
                        item.RoutTbl = routs.Where(x => x.Id == item.RoutId).FirstOrDefault();
                        item.RoutTbl.RegionTbl = regions.Where(x => x.Id == item.RoutTbl.RegionId).FirstOrDefault();
                    }
                    if (!String.IsNullOrEmpty(searchString))
                    {
                        list = list.Where(s => s.DriverTbl.FullName.ToString().Contains(searchString)).ToList();
                    }
                    switch (sortOrder)
                    {
                        case "driver_desc":
                            list = list.OrderByDescending(s => s.DriverTbl.FullName).ToList();
                            break;
                        case "routRegion":
                            list = list.OrderBy(s => s.RoutTbl.RegionTbl.RegionName).ToList();
                            break;
                        case "routRegion_desc":
                            list = list.OrderByDescending(s => s.RoutTbl.RegionTbl.RegionName).ToList();
                            break;
                        case "routTransaction":
                            list = list.OrderBy(s => s.RoutTbl.RoutTransactionType).ToList();
                            break;
                        case "routTransaction_desc":
                            list = list.OrderByDescending(s => s.RoutTbl.RoutTransactionType).ToList();
                            break;
                        case "routShiftType":
                            list = list.OrderBy(s => s.RoutTbl.ShiftType).ToList();
                            break;
                        case "routShiftType_desc":
                            list = list.OrderByDescending(s => s.RoutTbl.ShiftType).ToList();
                            break;
                        case "routTime":
                            list = list.OrderBy(s => s.RoutTbl.EnterTime).ToList();
                            break;
                        case "routTime_desc":
                            list = list.OrderByDescending(s => s.RoutTbl.EnterTime).ToList();
                            break;
                        case "routDate":
                            list = list.OrderBy(s => s.RoutTbl.StartDate).ToList();
                            break;
                        case "routDate_desc":
                            list = list.OrderByDescending(s => s.RoutTbl.StartDate).ToList();
                            break;
                        //case "isTemporary":
                        //    list = list.OrderBy(s => s.IsTemporary).ToList();
                        //    break;
                        //case "isTemporary_desc":
                        //    list = list.OrderByDescending(s => s.IsTemporary).ToList();
                        //    break;
                        default:
                            list = list.OrderBy(s => s.Id).OrderByDescending(x => x.Id).ToList();
                            break;
                    }
                    int pageSize = 10;
                    int pageNumber = (page ?? 1);
                    var allDrivers = _driver.GetAllDrivers();
                    foreach (var item in list)
                    {
                        var element = BaseMapper<DriverRoutViewModel, DriverRoutTbl>.Map(item);
                        element.Driver = BaseMapper<DriverTbl, DriverViewModel>.Map(allDrivers.Where(x => x.Id == item.DriverId).FirstOrDefault());
                        element.DriverFullName = element.Driver.FullName;
                        element.RoutEnterTimeString = item.RoutTbl.EnterTime.ToString();
                        element.Rout = item.RoutTbl;
                        element.Rout.EnterTime = item.RoutTbl.EnterTime;
                        element.RoutStartDate = item.RoutTbl.StartDate;
                        element.RoutStartDateString = item.RoutTbl.StartDate.ToPersianDateString();
                        element.RoutRegionName = item.RoutTbl.RegionTbl.RegionName;
                        element.RoutShiftType = _rout.GetRout(element.RoutId).ShiftType;
                        element.RoutTransactionType = _rout.GetRout(element.RoutId).RoutTransactionType;
                        commonList.Add(element);
                    }

                    // return View(commonList.OrderBy(x=> new { x.RoutStartDate, x.Rout.EnterTime }).ToPagedList(pageNumber, pageSize));
                    return View(commonList.OrderBy(x => x.RoutStartDate).ThenBy(x => x.Rout.EnterTime).ToPagedList(pageNumber, pageSize));
                }
            }
            return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
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
                    DriverRoutTbl driverRout = _driverRout.GetDriverRout(id);
                    if (driverRout == null)
                    {
                        return HttpNotFound();
                    }
                    var obj = BaseMapper<DriverRoutViewModel, DriverRoutTbl>.Map(driverRout);
                    obj.RoutRegionName = _rout.GetRout(driverRout.RoutId).RegionTbl.RegionName;
                    obj.RoutEnterTimeString = _rout.GetRout(driverRout.RoutId).EnterTime.ToString();
                    obj.RoutStartDateString = _rout.GetRout(driverRout.RoutId).StartDate.ToPersianDateString();
                    obj.Phone1 = _driver.GetDriver(driverRout.DriverId).Phone1;
                    obj.RoutShiftType = _rout.GetRout(driverRout.RoutId).ShiftType;
                    obj.RoutShiftType = _rout.GetRout(driverRout.RoutId).ShiftType;
                    obj.DriverFullName = _driver.GetDriver(driverRout.DriverId).FullName;
                    return View(obj);
                }
            }
            return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    common = new DriverRoutViewModel();
                    common.RoutList = _rout.GetAllRouts();
                    common.DriverList = _driver.GetAllDrivers().Select(BaseMapper<DriverTbl, DriverViewModel>.Map).ToList();
                    return View(common);
                }
            }
            return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DriverRoutTbl rout)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        _driverRout.AddNewDriverRout(rout);
                        _uow.SaveAllChanges();
                    }
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("login", "Account");
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
                    DriverRoutTbl driverRout = _driverRout.GetDriverRout(id);
                    if (driverRout == null)
                    {
                        return HttpNotFound();
                    }
                    var obj = BaseMapper<DriverRoutViewModel, DriverRoutTbl>.Map(driverRout);
                    obj.Driver = BaseMapper<DriverViewModel, DriverTbl>.Map(_driver.GetDriver(driverRout.DriverId));
                    obj.Rout = _rout.GetRout(driverRout.RoutId);
                    obj.DriverList = _driver.GetAllDrivers().Select(BaseMapper<DriverViewModel, DriverTbl>.Map).ToList();
                    obj.RoutList = _rout.GetAllRouts();
                    return View(obj);
                }
            }
            return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DriverRoutViewModel driverRout)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        var obj = BaseMapper<DriverRoutTbl, DriverRoutViewModel>.Map(driverRout);
                        obj.IsActive = true;
                        _driverRout.UpdateDriverRout(obj);
                        _uow.SaveAllChanges();
                    }
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
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
                    DriverRoutTbl driverRout = _driverRout.GetDriverRout(id);
                    if (driverRout == null)
                    {
                        return HttpNotFound();
                    }
                    var obj = BaseMapper<DriverRoutTbl, DriverRoutViewModel>.Map(driverRout);
                    obj.DriverFullName = _driver.GetDriver(driverRout.DriverId).FullName;
                    obj.RoutRegionName = _rout.GetRout(driverRout.RoutId).RegionTbl.RegionName;
                    obj.RoutEnterTimeString = _rout.GetRout(driverRout.RoutId).EnterTime.ToString();
                    obj.RoutStartDateString = _rout.GetRout(driverRout.RoutId).StartDate.ToPersianDateString();
                    obj.Phone1 = _driver.GetDriver(driverRout.DriverId).Phone1;
                    obj.RoutShiftType = _rout.GetRout(driverRout.RoutId).ShiftType;
                    obj.RoutShiftType = _rout.GetRout(driverRout.RoutId).ShiftType;
                    //if (obj.IsTemporary == (int)RoutTypeEnum.Always)
                    //{
                    //    ViewBag.IsTemporary = RoutTypeEnum.Always;
                    //}
                    //else
                    //{
                    //    ViewBag.IsTemporary = RoutTypeEnum.Temporary;
                    //}
                    return View(obj);
                }
            }
            return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    _driverRout.Delete(id);
                    _uow.SaveAllChanges();
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        public ActionResult Allocate(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    common = new DriverRoutViewModel();
                    var driverRouts = _driverRout.GetDriverRoutByRoutId((int)id);
                    if (driverRouts == null)
                    {
                        return HttpNotFound();
                    }
                    var rout = _rout.GetRout(id);
                    var allDrivers = _driver.GetAllDrivers();
                    var allocateDriversId = _driverRout.GetDriverRoutByRoutId((int)id).Select(x => x.DriverId).ToList();
                    var AllocateDriverList = _driver.GetAllDriversByIds(allocateDriversId);
                    var remainDrivers = _driver.GetOtherDriversByIds(allocateDriversId);
                    var driverListTemp = new List<DriverViewModel>();
                    foreach (var item in allDrivers)
                    {
                        var obj = BaseMapper<DriverTbl, DriverViewModel>.Map(item);
                        obj.DriverId = item.Id;
                        obj.BirthDateString = "";
                        driverListTemp.Add(obj);
                    }
                    common.DriverList = driverListTemp;
                    //common.AllocateDriverList = AllocateDriverList.Select(BaseMapper<DriverTbl, DriverViewModel>.Map).ToList();
                    //common.AllocateDriverList.Select(x => { x.IsSelected = true; return x; }).ToList();
                    var allocateDriverListTemp = new List<DriverViewModel>();
                    foreach (var item in AllocateDriverList)
                    {
                        var obj = BaseMapper<DriverTbl, DriverViewModel>.Map(item);
                        obj.DriverId = item.Id;
                        obj.IsSelected = true;
                        allocateDriverListTemp.Add(obj);
                    }
                    common.AllocateDriverList = allocateDriverListTemp;
                    var remainDriversTemp = new List<DriverViewModel>();
                    foreach (var item in remainDrivers)
                    {
                        var obj = BaseMapper<DriverTbl, DriverViewModel>.Map(item);
                        obj.DriverId = item.Id;
                        obj.IsSelected = false;
                        remainDriversTemp.Add(obj);
                    }
                    common.RemailDriverList = remainDriversTemp;
                    common.RoutId = (int)id;
                    return View(common);
                }
            }
            return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Allocate(DriverRoutViewModel driverRout)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        var obj = BaseMapper<DriverRoutTbl, DriverRoutViewModel>.Map(driverRout);
                        //ViewBag.Region = driverRouts.Rout.RegionId;
                        if (driverRout.RemailDriverList != null)
                        {
                            foreach (var item in driverRout.RemailDriverList)
                            {
                                if (item.IsSelected)
                                {
                                    var entity = new DriverRoutTbl();
                                    entity.DriverId = item.DriverId;
                                    entity.RoutId = driverRout.RoutId;
                                    _driverRout.AddNewDriverRout(entity);
                                }
                            }
                        }
                        if (driverRout.AllocateDriverList != null)
                        {
                            foreach (var item in driverRout.AllocateDriverList)
                            {
                                if (!item.IsSelected)
                                {
                                    var entity = new DriverRoutTbl();
                                    entity = _driverRout.GetDriverRoutByDriverIdRoutId(item.DriverId, driverRout.RoutId);
                                    _driverRout.Delete(entity.Id);
                                }
                            }
                        }
                        _uow.SaveAllChanges();
                    }
                    return RedirectToAction("Index", "Rout");
                }
            }
            return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        public ActionResult SearchByRout(int? id, int? page, string dropRegionId)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    var driverRoutList = _driverRout.GetDriverRoutByRoutId((int)id);
                    if (driverRoutList == null)
                    {
                        return HttpNotFound();
                    }
                    IEnumerable<SelectListItem> regionItems = _region.GetAllRegions().Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.RegionName
                    });

                    if (!string.IsNullOrWhiteSpace(dropRegionId))
                    {
                        //if (TempData["Region"] == null)
                        //{
                        //    TempData["Region"] = regionItems.Where(x => x.Value == dropRegionId).FirstOrDefault().Text;
                        //}
                        ViewBag.Region = regionItems.Where(x => x.Value == dropRegionId).FirstOrDefault().Text;
                        // TempData["Region"] = regionItems.Where(x => x.Value == dropRegionId).FirstOrDefault().Text;

                    }
                    else
                    {
                        //dropRegionId = allRegion.FirstOrDefault().Id.ToString();
                        dropRegionId = ViewBag.Region != null ? ViewBag.Rgion : "0";
                        // dropRegionId = TempData["Region"] != null ? TempData["Region"].ToString() : "0";
                    }
                    ViewBag.RegionItems = regionItems;

                    commonList = new List<DriverRoutViewModel>();
                    int pageSize = 10;
                    var allDrivers = _driver.GetAllDrivers();
                    var rout = _rout.GetRout(id);
                    int pageNumber = (page ?? 1);
                    foreach (var item in driverRoutList)
                    {
                        var element = BaseMapper<DriverRoutViewModel, DriverRoutTbl>.Map(item);
                        element.Driver = BaseMapper<DriverViewModel, DriverTbl>.Map(allDrivers.Where(x => x.Id == item.DriverId).FirstOrDefault());
                        element.DriverFullName = element.Driver.FullName;
                        //element.Rout = routs.Where(x => x.Id == item.RoutId).FirstOrDefault();
                        element.RoutEnterTimeString = item.RoutTbl.EnterTime.ToString();
                        element.RoutStartDate = item.RoutTbl.StartDate;
                        element.RoutStartDateString = item.RoutTbl.StartDate.ToPersianDateString();
                        element.RoutRegionName = item.RoutTbl.RegionTbl.RegionName;
                        element.RoutShiftType = _rout.GetRout(element.RoutId).ShiftType;
                        commonList.Add(element);
                    }
                    return View("Index", commonList.ToPagedList(pageNumber, pageSize));
                }
            }
            return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpPost]
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
                    var routIds = rout.Select(x => x.Id).ToList();
                    var driverRouts = _driverRout.GetDriverRoutByRoutIds(routIds);
                    if (driverRouts == null)
                    {
                        return HttpNotFound();
                    }
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("login", "Account");
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
