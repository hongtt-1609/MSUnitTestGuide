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
    public class CategoriesController : Controller
    {
        private ICategoryRepository _repo;
        public int pageSize = 10;

        public CategoriesController(ICategoryRepository repo)
        {
            _repo = repo;
        }
        // GET: Categories
        public ActionResult Index(int? page)
        {
            int currentPage = page == null ? 0 : Convert.ToInt32(page);
            return View(_repo.GetCategories(currentPage, pageSize).ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _repo.GetById(Convert.ToInt32(id));
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CreatedAt,UpdatedAt")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.CreatedAt = category.UpdatedAt = DateTime.Now;
                _repo.Update(category);
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _repo.GetById(Convert.ToInt32(id));
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CreatedAt,UpdatedAt")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.UpdatedAt = DateTime.Now;
                _repo.Update(category);
                return RedirectToAction("Index");
            }
            return View(category);
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
