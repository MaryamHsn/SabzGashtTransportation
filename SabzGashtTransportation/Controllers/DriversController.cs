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

namespace SabzGashtTransportation.Controllers
{
    public class DriversController : Controller
    {
        readonly IDriverService _drivers;
        readonly IUnitOfWork _uow;
        public DriversController(IUnitOfWork uow, IDriverService drivers)
        {
            _drivers = drivers;
            _uow = uow;
        }

       // private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Drivers
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FirstName = String.IsNullOrEmpty(sortOrder) ? "firstName_desc" : "";
            ViewBag.LastName = sortOrder == "LastName" ? "lastName_desc" : "lastName";
            ViewBag.Phone = sortOrder == "Phone" ? "phone_desc" : "phone";

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
                case "LastName_desc":
                    list = list.OrderBy(s => s.LastName).ToList();
                    break;
                case "lastName_desc":
                    list = list.OrderByDescending(s => s.LastName).ToList();
                    break;
                case "Phone":
                    list = list.OrderBy(s => s.Phone1).ToList();
                    break;
                case "phone_desc":
                    list = list.OrderByDescending(s => s.Phone1).ToList();
                    break;
                default:
                    list = list.OrderBy(s => s.DriverId).ToList();
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        
        } 

        // GET: Drivers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // DriverTbl driverTbl = db.Drivers.Find(id); 
            DriverTbl driverTbl = _drivers.GetDriver(id);

            if (driverTbl == null)
            {
                return HttpNotFound();
            }
            return View(driverTbl);
        }

        // GET: Drivers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( DriverTbl driver)
        {
            if (ModelState.IsValid)
            {
                //db.Drivers.Add(driverTbl);
                // db.SaveChanges();
                driver.IsActive = true;
                driver.CFDate=DateTime.Now;
                driver.LFDate = DateTime.Now;

                _drivers.AddNewDriver(driver);
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
            //DriverTbl driverTbl = db.Drivers.Find(id);
            DriverTbl driverTbl =_drivers.GetDriver(id);

            if (driverTbl == null)
            {
                return HttpNotFound();
            }
            return View(driverTbl);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( DriverTbl driver)
        {
            if (ModelState.IsValid)
            {
                _drivers.Delete(driver.DriverId);
                driver.LFDate=DateTime.Now;
                _drivers.AddNewDriver(driver);
                _uow.SaveAllChanges();

                // db.Entry(driverTbl).State = EntityState.Modified;
                //   db.SaveChanges();
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
            DriverTbl driverTbl = _drivers.GetDriver(id);

            // DriverTbl driverTbl = db.Drivers.Find(id);
            if (driverTbl == null)
            {
                return HttpNotFound();
            }
            return View(driverTbl);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _drivers.Delete(id);
            _uow.SaveAllChanges();

            //DriverTbl driverTbl = db.Drivers.Find(id);
            //db.Drivers.Remove(driverTbl);
            //db.SaveChanges();
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
