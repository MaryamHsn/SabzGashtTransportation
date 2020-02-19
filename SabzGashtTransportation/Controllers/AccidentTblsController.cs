using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using Sabz.DataLayer.Context;
using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.IService;

namespace SabzGashtTransportation.Controllers
{
    public class AccidentTblsController : Controller
    {
        readonly IAccidentService _accident;
        readonly IUnitOfWork _uow;
        public AccidentTblsController(IUnitOfWork uow, IAccidentService accident)
        {
            _accident = accident;
            _uow = uow;
        }

        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Driver = String.IsNullOrEmpty(sortOrder) ? "driver_desc" : "";
            ViewBag.Automobile = sortOrder == "Auto" ? "auto_desc" : "auto";
            ViewBag.Cost= sortOrder == "Cost" ? "cost_desc" : "cost";
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
            var list = _accident.GetAllAccidents();
            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(s => s.DriverId.ToString().Contains(searchString)
                                       || s.AutomobileId.ToString().Contains(searchString)
                                       || s.Cost.ToString().Contains(searchString)
                                       || s.UseInsurence.ToString().Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "driver_desc":
                    list = list.OrderByDescending(s => s.DriverId).ToList();
                    break;
                case "Auto":
                    list = list.OrderBy(s => s.AutomobileId).ToList();
                    break;
                case "auto_desc":
                    list = list.OrderByDescending(s => s.AutomobileId).ToList();
                    break;
                case "Cost":
                    list = list.OrderBy(s => s.Cost).ToList();
                    break;
                case "cost_desc":
                    list = list.OrderByDescending(s => s.Cost).ToList();
                    break;
                case "insurance":
                    list = list.OrderBy(s => s.UseInsurence).ToList();
                    break;
                case "insurance_desc":
                    list = list.OrderByDescending(s => s.UseInsurence).ToList();
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
            AccidentTbl accident= _accident.GetAccident(id);

            if (accident == null)
            {
                return HttpNotFound();
            }
            return View(accident);
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
        public ActionResult Create(AccidentTbl accident)
        {
            if (ModelState.IsValid)
            {
                //db.Drivers.Add(driverTbl);
                // db.SaveChanges();
                accident.IsActive = true;
                accident.CFDate = DateTime.Now;
                accident.LFDate = DateTime.Now;

                _accident.AddNewAccident(accident);
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
            return View(accident);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccidentTbl accident)
        {
            if (ModelState.IsValid)
            {
                _accident.Delete(accident.AccidentId);
                accident.LFDate = DateTime.Now;
                _accident.AddNewAccident(accident);
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
            AccidentTbl accident = _accident.GetAccident(id);

            if (accident == null)
            {
                return HttpNotFound();
            }
            return View(accident);
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
