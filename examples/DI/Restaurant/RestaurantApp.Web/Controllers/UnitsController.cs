using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestaurantApp.Core;
using RestaurantApp.Core.Interfaces;
using RestaurantApp.Infrastructure;

namespace RestaurantApp.Web.Controllers
{
    public class UnitsController : Controller
    {
        private readonly  IUnitRepository _repo;
        private int pageSize = 10;
        public UnitsController(IUnitRepository repo)
        {
            _repo = repo;
        }

        // GET: Units
        public ActionResult Index(int? page)
        {
            int currentPage = page == null ? 0 : Convert.ToInt32(page);
            return View(_repo.GetUnits(currentPage,pageSize).ToList());
        }

        // GET: Units/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = _repo.GetById(Convert.ToInt32(id));
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        // GET: Units/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Units/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CreatedAt,UpdatedAt")] Unit unit)
        {
            if (ModelState.IsValid)
            {
                unit.CreatedAt = unit.UpdatedAt = DateTime.Now;
                _repo.Update(unit);
                return RedirectToAction("Index");
            }

            return View(unit);
        }

        // GET: Units/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = _repo.GetById(Convert.ToInt32(id));
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        // POST: Units/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CreatedAt,UpdatedAt")] Unit unit)
        {
            if (ModelState.IsValid)
            {
                unit.UpdatedAt = DateTime.Now;
                _repo.Update(unit);
                return RedirectToAction("Index");
            }
            return View(unit);
        }

     
        public JsonResult Remove(int id)
        {
            
            bool result = _repo.Remove(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               
            }
            base.Dispose(disposing);
        }
    }
}
