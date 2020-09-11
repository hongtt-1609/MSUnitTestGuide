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
    public class CartsController : Controller
    {
        private ICartRepository _repo;
        public int pageSize = 10;
        public CartsController(ICartRepository repo)
        {
            _repo = repo;
        }
        // GET: Carts
        public ActionResult Index(int? page)
        {
            return View();
        }

        // GET: Carts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = _repo.GetById(Convert.ToInt32(id));
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // GET: Carts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Status,Description,CreatedAt,UpdatedAt")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                cart.CreatedAt = cart.UpdatedAt = DateTime.Now;
                _repo.Update(cart);
                return RedirectToAction("Index");
            }

            return View(cart);
        }

        // GET: Carts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = _repo.GetById(Convert.ToInt32(id));
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Status,Description,CreatedAt,UpdatedAt")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                cart.UpdatedAt = DateTime.Now;
                _repo.Update(cart);
                return RedirectToAction("Index");
            }
            return View(cart);
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
