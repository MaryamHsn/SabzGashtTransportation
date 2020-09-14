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
using Sabz.ServiceLayer.IService;
using Sabz.ServiceLayer.Mapper;
using Sabz.ServiceLayer.Utils;
using SabzGashtTransportation.ViewModel;

namespace SabzGashtTransportation.Controllers
{
    public class DriversController : Controller
    {
        readonly IDriverService _drivers;
        readonly IAutomobileService _automobile;
        readonly IBankAccountNumberService _bankAccountNumber;
        readonly IRegionService _region;

        readonly IUnitOfWork _uow;
        private DriverViewModel common { get; set; }
        private List<DriverViewModel> commonList { get; set; }

        public DriversController(IUnitOfWork uow, IDriverService drivers, IAutomobileService automobile, IBankAccountNumberService bankAccountNumber, IRegionService region)
        {
            _region = region;
            _bankAccountNumber = bankAccountNumber;
            _automobile = automobile;
            _drivers = drivers;
            _uow = uow;
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpGet]
        public ActionResult Index(DriverFullViewModel model)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                    {
                        commonList = new List<DriverViewModel>();
                        ViewBag.CurrentSort = model.SortOrder;
                        ViewBag.FirstName = String.IsNullOrEmpty(model.SortOrder) ? "firstName_desc" : "";
                        ViewBag.LastName = model.SortOrder == "firstName" ? "firstName_desc" : "firstName";
                        ViewBag.LastName = model.SortOrder == "lastName" ? "lastName_desc" : "lastName";
                        ViewBag.Phone = model.SortOrder == "phone" ? "phone_desc" : "phone";
                        //ViewBag.BankAccount = sortOrder == "bankAccount" ? "bankAccount_desc" : "bankAccount";

                        if (model.SearchString != null)
                        {
                            model.Page = 1;
                        }
                        else
                        {
                            model.SearchString = model.CurrentFilter;
                        }

                        ViewBag.CurrentFilter = model.SearchString;
                        var list = new List<DriverTbl>();

                        if (model.RegionId == 0)
                        {
                            list = _drivers.GetAllDrivers().ToList();
                        }
                        else
                        {
                            list = _drivers.GetAllDriversByRegionId(model.RegionId).ToList();
                        }
                        if (!String.IsNullOrEmpty(model.SearchString))
                        {
                            list = list.Where(s => s.FirstName.Contains(model.SearchString)
                                                   || s.LastName.Contains(model.SearchString)
                                                   || s.BankAccountNumberTbls.Select(x=>x.BankAccountNumber.Contains(model.SearchString)).FirstOrDefault()).ToList();
                        }
                        switch (model.SortOrder)
                        {
                            case "firstName_desc":
                                list = list.OrderByDescending(s => s.FirstName).ToList();
                                break;
                            case "firsName":
                                list = list.OrderBy(s => s.FirstName).ToList();
                                break;
                            case "lastName":
                                list = list.OrderBy(s => s.LastName).ToList();
                                break;
                            case "lastName_desc":
                                list = list.OrderByDescending(s => s.LastName).ToList();
                                break;
                            case "phone":
                                list = list.OrderBy(s => s.Phone1).ToList();
                                break;
                            case "phone_desc":
                                list = list.OrderByDescending(s => s.Phone1).ToList();
                                break;
                            default:
                                list = list.OrderBy(s => s.Id).ToList();
                                break;
                        }

                        int pageSize = 1000;
                        int pageNumber = (model.Page ?? 1);
                        var bankAccount = _bankAccountNumber.GetAllBankAccountNumbers();
                        model.Regions= _region.GetAllRegions();
                        
                        foreach (var item in list)
                        {
                            var element = BaseMapper<DriverViewModel, DriverTbl>.Map(item);
                            element.DriverId = item.Id;
                            element.BankAccountNumbers = bankAccount.Where(x => x.DriverId == item.Id).ToList(); 
                            commonList.Add(element);
                        }
                        model.DriverViewModels= commonList.ToPagedList(pageNumber, pageSize);
                        return View(model); 
                    }
                }
                return RedirectToAction("login", "Account");
            }
            catch (Exception e)
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
                    DriverTbl driver = _drivers.GetDriver(id);
                    if (driver == null)
                    {
                        return HttpNotFound();
                    }
                    var obj = BaseMapper<DriverViewModel, DriverTbl>.Map(driver);
                    obj.BirthDateString = driver.BirthDate != null ? ((DateTime)(driver.BirthDate)).ToPersianDateString() : "";
                    obj.DriverId = driver.Id;
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
                    var Regiones = _region.GetAllRegions().ToList();
                    var regionList = new List<RegionViewModel>();
                    foreach (var item in Regiones)
                    {
                        var obj = new RegionViewModel();
                        obj.RegionId = item.Id;
                        obj.RegionName = item.RegionName;
                        regionList.Add(obj);
                    }

                    common = new DriverViewModel()
                    {
                        Automobiles = _automobile.GetAllAutomobiles().ToList(),
                        Regiones = regionList
                    };
                    return View(common);
                }
            }
            return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DriverViewModel driver)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                    {
                        driver.Automobiles = _automobile.GetAllAutomobiles().ToList();
                        var Regiones = _region.GetAllRegions().ToList();
                        driver.Regiones = Regiones.Select(BaseMapper<RegionViewModel, RegionTbl>.Map).ToList();
                         
                        if (ModelState.IsValid)
                        {
                            var obj = BaseMapper<DriverViewModel, DriverTbl>.Map(driver); 
                            if (driver.BirthDateString != null)
                                obj.BirthDate = driver.BirthDateString.ToGeorgianDate();
                            obj.IsActive = true;
                            obj.CreatedDate = DateTime.Now;
                            obj.ModifiedDate = DateTime.Now;
                            var addnewDriver= _drivers.AddNewDriver(obj);
                            foreach (var item in driver.BankAccountNumbers)
                            {
                                if (item.BankAccountNumber != null)
                                {
                                    var bnkAccount = new BankAccountNumberTbl();
                                    bnkAccount.DriverId = addnewDriver.Id;
                                    bnkAccount.RegionId = item.RegionId;
                                    bnkAccount.BankAccountNumber = item.BankAccountNumber;
                                    _bankAccountNumber.AddNewBankAccountNumber(bnkAccount);

                                }
                            }
                            _uow.SaveAllChanges();
                            return RedirectToAction("Index");

                        }

                        return View(driver);
                    }
                }
                return RedirectToAction("login", "Account");
            }
            catch (Exception e)
            {
                return View(driver);
            }
        }

        //[Authorize(Roles = "admin , SuperViser")]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        DriverTbl driver = _drivers.GetDriver(id);
                        if (driver == null)
                        {
                            return HttpNotFound();
                        }
                        var Regiones = _region.GetAllRegions().ToList();
                        var regionList = new List<RegionViewModel>();
                        foreach (var item in Regiones)
                        {
                            var objRegion = new RegionViewModel();
                            objRegion.RegionId = item.Id;
                            objRegion.RegionName = item.RegionName;
                            regionList.Add(objRegion);
                        }
                        var obj = BaseMapper<DriverViewModel, DriverTbl>.Map(driver);
                        obj.Automobiles = _automobile.GetAllAutomobiles().ToList();
                         
                        obj.Regiones = regionList;
                        obj.BankAccountNumbers = _bankAccountNumber.GetBankAccountNumberByDriverId((int)id);
                        if (driver.BirthDate != null)
                        {
                            obj.BirthDateString = obj.BirthDate != null
                            ? ((DateTime)(obj.BirthDate)).ToPersianDateString()
                            : ((DateTime)(driver.BirthDate)).ToPersianDateString();
                        }
                        obj.DriverId = driver.Id;
                        return View(obj);
                    }
                }
                return RedirectToAction("login", "Account");
            }
            catch (Exception)
            {

                throw;
            }
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DriverViewModel driver)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                    {
                        if (ModelState.IsValid)
                        {
                            //driver.ModifiedDate=DateTime.Now;
                            //_drivers.Delete(driver.DriverId);
                            var obj = BaseMapper<DriverViewModel, DriverTbl>.Map(driver);
                            if (driver.BirthDateString != null)
                                obj.BirthDate = driver.BirthDateString.ToGeorgianDate();
                            obj.CreatedDate = DateTime.Now;
                            obj.ModifiedDate = DateTime.Now;
                            obj.IsActive = true;
                            obj.Id = driver.DriverId;
                            _drivers.UpdateDriver(obj);
                            var bankAccountExist = _bankAccountNumber.GetBankAccountNumberByDriverId(driver.DriverId);
                            foreach (var acnt in bankAccountExist)
                            {
                                _bankAccountNumber.Delete(acnt.Id);
                            }
                            foreach (var item in driver.BankAccountNumbersReserve)
                            {
                                if (item.BankAccountNumber != null)
                                {
                                    var bnkAccount = new BankAccountNumberTbl();
                                    bnkAccount.DriverId = driver.DriverId;
                                    bnkAccount.RegionId = item.RegionId;
                                    bnkAccount.BankAccountNumber = item.BankAccountNumber;
                                    _bankAccountNumber.AddNewBankAccountNumber(bnkAccount);
                                }
                            }
                            _uow.SaveAllChanges();
                        }
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("login", "Account");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
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
                    DriverTbl driver = _drivers.GetDriver(id);
                    if (driver == null)
                    {
                        return HttpNotFound();
                    }
                    var obj = BaseMapper<DriverViewModel, DriverTbl>.Map(driver);
                    obj.BirthDateString = ((DateTime)(driver.BirthDate)).ToPersianDateString();
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
                    _drivers.Delete(id);
                    //  _uow.SaveAllChanges(); 
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
