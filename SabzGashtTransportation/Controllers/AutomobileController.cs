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
    public class AutomobileController : Controller
    {
        readonly IAutomobileService _automobile;
        readonly IUnitOfWork _uow;
        public AutomobileController(IUnitOfWork uow, IAutomobileService automobile)
        {
            _automobile= automobile;
            _uow = uow;
        }

        // GET: Automobile
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Number = String.IsNullOrEmpty(sortOrder) ? "number_desc" : "";
            ViewBag.Shasi= sortOrder == "Shasi" ? "shasi_desc" : "shasi";
            ViewBag.AutomobileType = sortOrder == "AutomobileType" ? "automobileType_desc" : "automobileType";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var list = _automobile.GetAllAutomobiles();
            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(s => s.Number.Contains(searchString)
                                       || s.Shasi.Contains(searchString)
                                       || s.AutomobileTypeId.ToString().Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "number_desc":
                    list = list.OrderByDescending(s => s.Number).ToList();
                    break;
                case "shasi":
                    list = list.OrderBy(s => s.Shasi).ToList();
                    break;
                case "shasi_desc":
                    list = list.OrderByDescending(s => s.Shasi).ToList();
                    break;
                case "automobileType":
                    list = list.OrderBy(s => s.AutomobileTypeId).ToList();
                    break;
                case "automobileType_desc":
                    list = list.OrderByDescending(s => s.AutomobileTypeId).ToList();
                    break;
                default:
                    list = list.OrderBy(s => s.Number).ToList();
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
            AutomobileTbl automobile= _automobile.GetAutomobile(id);

            if (automobile== null)
            {
                return HttpNotFound();
            }
            return View(automobile);
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
        public ActionResult Create(AutomobileTbl automobile)
        {
            if (ModelState.IsValid)
            {
                automobile.IsActive = true;
                automobile.CFDate = DateTime.Now;
                automobile.LFDate = DateTime.Now;

                _automobile.AddNewAutomobile(automobile);
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
            AutomobileTbl automobile = _automobile.GetAutomobile(id);

            if (automobile == null)
            {
                return HttpNotFound();
            }
            return View(automobile);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AutomobileTbl automobile)
        {
            if (ModelState.IsValid)
            {
                _automobile.Delete(automobile.AutoId);
                automobile.LFDate = DateTime.Now;
                _automobile.AddNewAutomobile(automobile);
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
            AutomobileTbl automobile= _automobile.GetAutomobile(id);
            if (automobile == null)
            {
                return HttpNotFound();
            }
            return View(automobile);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _automobile.Delete(id);
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
