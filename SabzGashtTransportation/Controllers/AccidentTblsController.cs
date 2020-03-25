using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
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
    public class AccidentTblsController : Controller
    {
        readonly IAccidentService _accident;
        private readonly IDriverService _driver;
        private readonly IAutomobileService _automobile;

        private AccidentViewModel common { get; set; }
        private List<AccidentViewModel> commonList { get; set; }

        readonly IUnitOfWork _uow;
        public AccidentTblsController(IUnitOfWork uow, IAccidentService accident, IDriverService driver, IAutomobileService automobile)
        {
            _automobile = automobile;
            _driver = driver;
            _accident = accident;
            _uow = uow;
        }

        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            commonList = new List<AccidentViewModel>();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Driver = String.IsNullOrEmpty(sortOrder) ? "driver_desc" : "";
            ViewBag.Automobile = sortOrder == "Auto" ? "auto_desc" : "auto";
            ViewBag.Cost = sortOrder == "cost" ? "cost_desc" : "cost";
            ViewBag.Insurance = sortOrder == "Insurance" ? "insurance_desc" : "insurance";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var accident = _accident.GetAllAccidents().ToList();
            var driver = _driver.GetAllDrivers().ToList();
            var autombile = _automobile.GetAllAutomobiles().ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                accident = accident.Where(s => s.DriverTbl.FirstName.Contains(searchString)
                                               || s.DriverTbl.LastName.Contains(searchString)
                                               || s.AutomobileTbl.Number.ToString().Contains(searchString)
                                               || s.Cost.ToString().Contains(searchString)
                                               || s.UseInsurence.ToString().Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "driver_desc":
                    //var driver = _driver.GetAllDrivers().OrderByDescending(s => s.DriverId).ToList();
                    accident = accident.OrderByDescending(s => s.DriverTbl.FatherName).ToList();
                    break;
                case "Auto":
                    accident = accident.OrderBy(s => s.AutomobileId).ToList();
                    break;
                case "auto_desc":
                    accident = accident.OrderByDescending(s => s.AutomobileId).ToList();
                    break;
                case "cost":
                    accident = accident.OrderBy(s => s.Cost).ToList();
                    break;
                case "cost_desc":
                    accident = accident.OrderByDescending(s => s.Cost).ToList();
                    break;
                case "insurance":
                    accident = accident.OrderBy(s => s.UseInsurence).ToList();
                    break;
                case "insurance_desc":
                    accident = accident.OrderByDescending(s => s.UseInsurence).ToList();
                    break;
                default:
                    accident = accident.OrderBy(s => s.DriverId).ToList();
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            foreach (var item in accident)
            {
                //var element = new AccidentViewModel();
                var element = BaseMapper<AccidentViewModel, AccidentTbl>.Map(item);
                element.DriverFirstName = driver.Where(x => x.Id == item.DriverId).SingleOrDefault() != null ? driver.Where(x => x.Id == item.DriverId).SingleOrDefault().FirstName : "--";
                element.DriverLastName = driver.Where(x => x.Id == item.DriverId).SingleOrDefault() != null ? driver.Where(x => x.Id == item.DriverId).SingleOrDefault().LastName : "--";
                element.DriverFullName = element.DriverFirstName + " " + element.DriverLastName;
                element.AutomobileNumber = autombile.Where(x => x.Id == item.AutomobileId).SingleOrDefault() != null ? autombile.Where(x => x.Id == item.AutomobileId).SingleOrDefault().Number : "--";
                element.Cost = accident.Where(x => x.Id == item.Id).SingleOrDefault() != null ? accident.Where(x => x.Id == item.Id).SingleOrDefault().Cost : 0;
                element.Description = accident.Where(x => x.Id == item.Id).SingleOrDefault() != null ? accident.Where(x => x.Id == item.Id).SingleOrDefault().Description : "";
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
            common = new AccidentViewModel();
            AccidentTbl accident = _accident.GetAccident(id);
            common = BaseMapper<AccidentViewModel, AccidentTbl>.Map(accident);
            common.DriverFullName = accident.DriverTbl != null ? accident.DriverTbl.FirstName + " " + accident.DriverTbl.LastName : _driver.GetDriver(accident.DriverId).FirstName + " " + _driver.GetDriver(accident.DriverId).LastName;
            common.AutomobileNumber = accident.AutomobileTbl != null ? accident.AutomobileTbl.Number : _automobile.GetAutomobile(accident.AutomobileId).Number;
            common.CreateDateString = accident.CreatedDate.ToPersianDateString();
            common.ModifiedDateString = ((DateTime)accident.ModifiedDate).ToPersianDateString();

            if (common == null)
            {
                return HttpNotFound();
            }
            return View(common);
        }

        // GET: Drivers/Create
        public ActionResult Create()
        {
            common = new AccidentViewModel()
            {
                Drivers = _driver.GetAllDrivers(),
                Automobiles = _automobile.GetAllAutomobiles()
            };
            List<SelectListItem> insuranceListItems = new List<SelectListItem>();
            insuranceListItems.Add(new SelectListItem() { Text = "استفاده شده", Value = "1" });
            insuranceListItems.Add(new SelectListItem() { Text = "استفاده نشده", Value = "0" });
            ViewBag.insurance = insuranceListItems;
            return View(common);
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccidentViewModel accident)
        {
            if (ModelState.IsValid)
            {
                var obj = BaseMapper<AccidentTbl, AccidentViewModel>.Map(accident);
                obj.IsActive = true;
                obj.CreatedDate = DateTime.Now;
                obj.ModifiedDate = DateTime.Now;
                obj.AutomobileId = _driver.GetDriver(obj.DriverId).AutomobileId;
                _accident.AddNewAccident(obj);
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
            AccidentTbl accident = _accident.GetAccident(id);

            if (accident == null)
            {
                return HttpNotFound();
            }
            var element = BaseMapper<AccidentViewModel, AccidentTbl>.Map(accident);
            element.DriverFirstName = accident.DriverTbl != null ? accident.DriverTbl.FirstName : "--";//driver.Where(x => x.DriverId == accident.DriverId).SingleOrDefault() != null ? driver.Where(x => x.DriverId == accident.DriverId).SingleOrDefault().FirstName : "--";
            element.DriverLastName = accident.DriverTbl != null ? accident.DriverTbl.LastName : "--";//driver.Where(x => x.DriverId == accident.DriverId).SingleOrDefault() != null ? driver.Where(x => x.DriverId == accident.DriverId).SingleOrDefault().LastName : "--";
            element.DriverFullName = element.DriverFirstName + " " + element.DriverLastName;
            element.AutomobileNumber = accident.AutomobileTbl != null ? accident.AutomobileTbl.Number : "--";// autombile.Where(x => x.AutoId == accident.AutomobileId).SingleOrDefault() != null ? autombile.Where(x => x.AutoId == accident.AutomobileId).SingleOrDefault().Number : "--";
            element.Cost = accident.Cost;
            element.Description = accident.Description;
            element.Drivers = _driver.GetAllDrivers().ToList();
            List<SelectListItem> insuranceListItems = new List<SelectListItem>();
            insuranceListItems.Add(new SelectListItem() { Text = "استفاده شده", Value = "1" });
            insuranceListItems.Add(new SelectListItem() { Text = "استفاده نشده", Value = "0" });
            ViewBag.insurance = insuranceListItems;
            return View(element);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccidentViewModel accident)
        {
            if (ModelState.IsValid)
            {
                accident.ModifiedDate = DateTime.Now;
                accident.IsActive = false;
                _accident.Delete(accident.AccidentId);
                var obj = BaseMapper<AccidentTbl, AccidentViewModel>.Map(accident);
                obj.AutomobileId = _driver.GetDriver(accident.DriverId).AutomobileId;
                obj.CreatedDate = DateTime.Now;
                obj.ModifiedDate = DateTime.Now;
                obj.IsActive = true;
                _accident.AddNewAccident(obj);
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
            AccidentTbl accident = _accident.GetAccident(id);

            if (accident == null)
            {
                return HttpNotFound();
            }
            var obj = BaseMapper<AccidentTbl, AccidentViewModel>.Map(accident);
            obj.DriverFullName = accident.DriverTbl != null
                ? accident.DriverTbl.FirstName + " " + accident.DriverTbl.LastName
                : _driver.GetDriver(accident.DriverId).FullName;
            obj.AutomobileNumber = accident.AutomobileTbl != null
                ? accident.AutomobileTbl.Number : _automobile.GetAutomobile(accident.AutomobileId).Number;
            if (obj.UseInsurence == 1)
            {
                ViewBag.insurance ="استفاده شده";
            }
            else
            {
                ViewBag.insurance = "استفاده نشده";
            }

            return View(obj);

        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _accident.Delete(id);
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
