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
    public class CartDetailsController : Controller
    {
        private ICartDetailRepository _repo;
        public int pageSize = 10;
        public CartDetailsController(ICartDetailRepository repo)
        {
            _repo = repo;
        }
        // GET: CartDetails
        public ActionResult Index()
        {
            return View();
        }

        // GET: CartDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartDetail cartDetail = _repo.GetById(Convert.ToInt32(id));
            if (cartDetail == null)
            {
                return HttpNotFound();
            }
            return View(cartDetail);
        }

        // GET: CartDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CartDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CartId,FoodId,Amount,CreatedAt,UpdatedAt")] CartDetail cartDetail)
        {
            if (ModelState.IsValid)
            {
                cartDetail.CreatedAt = cartDetail.UpdatedAt = DateTime.Now;
                _repo.Update(cartDetail);
                return RedirectToAction("Index");
            }

            return View(cartDetail);
        }

        // GET: CartDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartDetail cartDetail = _repo.GetById(Convert.ToInt32(id));
            if (cartDetail == null)
            {
                return HttpNotFound();
            }
            return View(cartDetail);
        }

        // POST: CartDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CartId,FoodId,Amount,CreatedAt,UpdatedAt")] CartDetail cartDetail)
        {
            if (ModelState.IsValid)
            {
                cartDetail.UpdatedAt = DateTime.Now;
                _repo.Update(cartDetail);
                return RedirectToAction("Index");
            }
            return View(cartDetail);
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
