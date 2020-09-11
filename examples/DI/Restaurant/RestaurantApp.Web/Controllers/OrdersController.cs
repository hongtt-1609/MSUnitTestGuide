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
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _repo;
        int pageSize = 10;
        public OrdersController(IOrderRepository repo)
        {
            _repo = repo;
        }

        // GET: Orders
        public ActionResult Index(int? page)
        {
            int currentPage = page == null ? 0 : Convert.ToInt32(page);
            return View(_repo.GetOrders(currentPage, pageSize).ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _repo.GetById(Convert.ToInt32(id));
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CartId,Total,Description,Status,CreatedAt,UpdatedAt")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.CreatedAt = order.UpdatedAt = DateTime.Now;
                _repo.Update(order);
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _repo.GetById(Convert.ToInt32(id));
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CartId,Total,Description,Status,CreatedAt,UpdatedAt")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.UpdatedAt = DateTime.Now;
                _repo.Update(order);
                return RedirectToAction("Index");
            }
            return View(order);
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
