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

        readonly IUnitOfWork _uow;
        private DriverViewModel common { get; set; }
        private List<DriverViewModel> commonList { get; set; }

        public DriversController(IUnitOfWork uow, IDriverService drivers, IAutomobileService automobile)
        {
            _automobile = automobile;
            _drivers = drivers;
            _uow = uow;
        }

        // GET: Drivers
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            commonList = new List<DriverViewModel>();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.FirstName = String.IsNullOrEmpty(sortOrder) ? "firstName_desc" : "";
            ViewBag.LastName = sortOrder == "lastName" ? "lastName_desc" : "lastName";
            ViewBag.Phone = sortOrder == "phone" ? "phone_desc" : "phone";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var list = _drivers.GetAllDrivers();
            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(s => s.FirstName.Contains(searchString)
                                       || s.LastName.Contains(searchString)
                                       || s.Phone1.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "firstName_desc":
                    list = list.OrderByDescending(s => s.FirstName).ToList();
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

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            foreach (var item in list)
            { 
                var element = BaseMapper<DriverViewModel, DriverTbl>.Map(item);
                element.DriverId = item.Id;
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
            DriverTbl driver = _drivers.GetDriver(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            var obj = BaseMapper<DriverViewModel, DriverTbl>.Map(driver);
            obj.BirthDateString = driver.BirthDate != null ? driver.BirthDate.ToPersianDateString() : "";
            obj.DriverId = driver.Id;
            return View(obj);
        }

        // GET: Drivers/Create
        public ActionResult Create()
        {
            common = new DriverViewModel()
            {
                Automobiles = _automobile.GetAllAutomobiles()
            };
            return View(common);
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( DriverViewModel driver)
        {
            if (ModelState.IsValid)
            {
                var obj = BaseMapper<DriverViewModel, DriverTbl>.Map(driver);
                obj.BirthDate = driver.BirthDateString.ToGeorgianDate();
                obj.IsActive = true;
                obj.CreatedDate=DateTime.Now;
                obj.ModifiedDate = DateTime.Now;
                _drivers.AddNewDriver(obj);
                //_uow.SaveAllChanges();
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
            DriverTbl driver =_drivers.GetDriver(id);
            var obj = BaseMapper<DriverViewModel, DriverTbl>.Map(driver);
            obj.Automobiles = _automobile.GetAllAutomobiles();
            obj.BirthDateString = obj.BirthDate != null
                ? obj.BirthDate.ToPersianDateString()
                : driver.BirthDate.ToPersianDateString();
            obj.DriverId = driver.Id;
            if (driver  == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( DriverViewModel driver)
        {
            if (ModelState.IsValid)
            {
                //driver.ModifiedDate=DateTime.Now;
                //_drivers.Delete(driver.DriverId);
                var obj = BaseMapper<DriverViewModel, DriverTbl>.Map(driver);
                obj.BirthDate = driver.BirthDateString.ToGeorgianDate();
                obj.CreatedDate = DateTime.Now;
                obj.ModifiedDate = DateTime.Now;
                obj.IsActive = true;
                //_drivers.AddNewDriver(obj);
                //_uow.SaveAllChanges(); 
                obj.Id = driver.DriverId;
                _drivers.UpdateDriver(obj);
                //_uow.SaveAllChanges();
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
            DriverTbl driver= _drivers.GetDriver(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            var obj = BaseMapper<DriverViewModel, DriverTbl>.Map(driver);
            obj.BirthDateString = driver.BirthDate.ToPersianDateString();
            return View(obj);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _drivers.Delete(id);
          //  _uow.SaveAllChanges(); 
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
