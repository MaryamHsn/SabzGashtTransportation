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
    public class DriverRoutController : Controller
    {
        readonly IDriverRoutService _driverRout;
        readonly IUnitOfWork _uow;
        public DriverRoutController(IUnitOfWork uow, IDriverRoutService driverRout)
        {
            _driverRout = driverRout;
            _uow = uow;
        }

        // GET: Automobile
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Driver = String.IsNullOrEmpty(sortOrder) ? "driver_desc" : "";
            ViewBag.Rout = sortOrder == "Rout" ? "rout_desc" : "rout";
            ViewBag.IsTemporary = sortOrder == "IsTemporary" ? "isTemporary_desc" : "isTemporary";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var list = _driverRout.GetAllDriverRouts();
            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(s => s.DriverId.ToString().Contains(searchString)
                                       || s.RoutId.ToString().Contains(searchString)
                                       || s.IsTemporary.ToString().Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "driver_desc":
                    list = list.OrderByDescending(s => s.DriverId).ToList();
                    break;
                case "rout":
                    list = list.OrderBy(s => s.RoutId).ToList();
                    break;
                case "rout_desc":
                    list = list.OrderByDescending(s => s.RoutId).ToList();
                    break;
                case "isTemporary":
                    list = list.OrderBy(s => s.IsTemporary).ToList();
                    break;
                case "isTemporary_desc":
                    list = list.OrderByDescending(s => s.IsTemporary).ToList();
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
            DriverRoutTbl driverRout= _driverRout.GetDriverRout(id);

            if (driverRout == null)
            {
                return HttpNotFound();
            }
            return View(driverRout);
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
        public ActionResult Create(DriverRoutTbl rout)
        {
            if (ModelState.IsValid)
            {
                rout.IsActive = true;
                rout.CFDate = DateTime.Now;
                rout.LFDate = DateTime.Now;

                _driverRout.AddNewDriverRout(rout);
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
            DriverRoutTbl driverRout = _driverRout.GetDriverRout(id);

            if (driverRout == null)
            {
                return HttpNotFound();
            }
            return View(driverRout);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DriverRoutTbl driverRout)
        {
            if (ModelState.IsValid)
            {
                _driverRout.Delete(driverRout.Id);
                driverRout.LFDate = DateTime.Now;
                _driverRout.AddNewDriverRout(driverRout);
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
            DriverRoutTbl rout = _driverRout.GetDriverRout(id);
            if (rout == null)
            {
                return HttpNotFound();
            }
            return View(rout);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _driverRout.Delete(id);
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
