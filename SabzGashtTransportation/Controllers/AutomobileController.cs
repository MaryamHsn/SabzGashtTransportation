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
using SabzGashtTransportation.ViewModel;

namespace SabzGashtTransportation.Controllers
{
    public class AutomobileController : Controller
    {
        readonly IAutomobileService _automobile;
        readonly IAutomobileTypeService _automobileType;
        readonly IDriverService _driver;
        private AutomobileViewModel common { get; set; }
        private List<AutomobileViewModel> commonList { get; set; }

        readonly IUnitOfWork _uow;
        public AutomobileController(IUnitOfWork uow, IAutomobileService automobile, IDriverService driver, IAutomobileTypeService automobileType)
        {
            _automobileType = automobileType;
            _driver = driver;
            _automobile = automobile;
            _uow = uow;
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                    {
                            commonList = new List<AutomobileViewModel>();
                        ViewBag.CurrentSort = sortOrder;
                        ViewBag.Number = String.IsNullOrEmpty(sortOrder) ? "number_desc" : ""; 
                        ViewBag.AutomobileType = sortOrder == "automobileType" ? "automobileType_desc" : "automobileType";
                        ViewBag.AutomobileTypeCooler = sortOrder == "automobileTypeCooler" ? "automobileTypeCooler_desc" : "automobileTypeCooler";
                        int pageSize = 1000;
                        int pageNumber = (page ?? 1);
                        if (searchString != null)
                            page = 1;
                        else
                            searchString = currentFilter;
                        ViewBag.CurrentFilter = searchString;
                        var list = _automobile.GetAllAutomobiles();
                        if (!String.IsNullOrEmpty(searchString))
                        {
                            list = list.Where(s => s.Number.Contains(searchString)
                                                   || s.AutomobileTypeId.ToString().Contains(searchString)).ToList();
                        }
                        switch (sortOrder)
                        {
                            case "number_desc":
                                list = list.OrderByDescending(s => s.Number).ToList();
                                break;
                            case "automobileType":
                                list = list.OrderBy(s => s.AutomobileTypeId).ToList();
                                break;
                            case "automobileType_desc":
                                list = list.OrderByDescending(s => s.AutomobileTypeId).ToList();
                                break;
                            case "automobileTypeCooler":
                                list = list.OrderBy(s => s.AutomobileTypeId).ToList();
                                break;
                            case "automobileTypeCooler_desc":
                                list = list.OrderByDescending(s => s.AutomobileTypeId).ToList();
                                break;
                            default:
                                list = list.OrderBy(s => s.Number).ToList();
                                break;
                        }

                        var AutomobileTypes = _automobileType.GetAllAutomobileTypes();
                        foreach (var item in list)
                        {
                            var element = BaseMapper<AutomobileViewModel, AutomobileTbl>.Map(item);
                            element.AutomobileType =
                                AutomobileTypes.Where(x => x.Id == item.AutomobileTypeId).FirstOrDefault();
                            element.AutoId = item.Id;
                            element.AutomobileTypeId = item.AutomobileTypeId;
                            commonList.Add(element);
                        }
                        return View(commonList.ToPagedList(pageNumber, pageSize));
                    }
                }
                return RedirectToAction("login", "Account");
            }
            catch (Exception)
            {
                return View();
            }
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
                    AutomobileTbl automobile = _automobile.GetAutomobile(id);
                    if (automobile.AutomobileTypeTbl != null)
                    {
                        automobile.AutomobileTypeTbl = _automobileType.GetAutomobileType(automobile.AutomobileTypeId);
                    }
                    common = new AutomobileViewModel();
                    common = BaseMapper<AutomobileViewModel, AutomobileTbl>.Map(automobile);
                    common.AutomobileType = _automobileType.GetAutomobileType(automobile.AutomobileTypeId);
                    //common.CreatedDateString = automobile.CreatedDate.ToPersianDateString();
                    //common.ModifiedDateString = ((DateTime)automobile.ModifiedDate).ToPersianDateString();
                    if (common == null)
                    {
                        return HttpNotFound();
                    }
                    return View(common);
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
                    return View();
                }
            }
            return RedirectToAction("login", "Account");
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "admin , SuperViser")]
        public ActionResult Create(AutomobileViewModel automobile)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                    {
                        if (ModelState.IsValid)
                        {
                            var obj = BaseMapper<AutomobileViewModel, AutomobileTbl>.Map(automobile);
                            if (automobile.Number == null)
                                obj.Number = "";
                            if (automobile.CreateYear == null)
                                obj.CreateYear = "";
                            obj.IsActive = true;
                            obj.CreatedDate = DateTime.Now;
                            obj.ModifiedDate = DateTime.Now;
                            obj.AutomobileTypeTbl = _automobileType.GetAutomobileTypeByCoolerBus((int)automobile.HasCoolerEnum, (int)automobile.IsBusEnum);
                            if (obj.AutomobileTypeTbl != null)
                                obj.AutomobileTypeId = obj.AutomobileTypeTbl.Id;
                            _automobile.AddNewAutomobile(obj);
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
                return View(automobile);
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
                    AutomobileTbl automobile = _automobile.GetAutomobile(id);
                    if (automobile == null)
                    {
                        return HttpNotFound();
                    }
                    var obj = BaseMapper<AutomobileViewModel, AutomobileTbl>.Map(automobile);
                    obj.AutoId = automobile.Id;
                    obj.AutomobileType = _automobileType.GetAutomobileType(obj.AutomobileTypeId);
                    return View(obj);
                }
            }
            return RedirectToAction("login", "Account");
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "admin , SuperViser")]
        public ActionResult Edit(AutomobileViewModel automobile)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                    {
                        if (ModelState.IsValid)
                        {
                            automobile.IsBus = (int)automobile.IsBusEnum;
                            automobile.HasCooler = (int)automobile.HasCoolerEnum;
                            var obj = BaseMapper<AutomobileTbl, AutomobileViewModel>.Map(automobile);
                            if (automobile.AutoId != 0)
                                obj.Id = automobile.AutoId;
                            obj.IsActive = true;
                            var autoType = _automobileType.GetAutomobileTypeByCoolerBus((int)automobile.HasCooler, (int)automobile.IsBus);
                            if (autoType != null)
                            {
                                obj.AutomobileTypeId = autoType.Id;
                            }
                            _automobile.UpdateAutomobile(obj);
                            _uow.SaveAllChanges();
                        }
                        return View();
                    }
                }
                return RedirectToAction("login", "Account");
            }
            catch (Exception)
            {
                return View();
            }
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
                    AutomobileTbl automobile = _automobile.GetAutomobile(id);
                    if (automobile == null)
                    {
                        return HttpNotFound();
                    }
                    var obj = BaseMapper<AutomobileTbl, AutomobileViewModel>.Map(automobile);
                    obj.AutomobileType = _automobileType.GetAutomobileType(automobile.AutomobileTypeId);
                    if (obj.AutomobileType.IsBus == (int)AutomobileTypeEnum.Bus)
                    {
                        ViewBag.AutomobileType = AutomobileTypeEnum.Bus;
                    }
                    else
                    {
                        ViewBag.AutomobileType = AutomobileTypeEnum.MiniBus;
                    }

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
                    _automobile.Delete(id);
                    _uow.SaveAllChanges();
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        public JsonResult FetchAutomobileList(int id)
        {
            var AutomobileList = _driver.GetAllDrivers().Where(x => x.Id == id);
            return Json(AutomobileList, JsonRequestBehavior.AllowGet);
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
