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
    public class UsersController : Controller
    {
        private readonly IUserRepository _repo;
        private int pageSize = 10;
        public UsersController(IUserRepository repo)
        {
            _repo = repo;
        }

        // GET: Users
        public ActionResult Index(int? page)
        {
            int currentPage = page == null ? 0 : Convert.ToInt32(page);
            return View(_repo.GetUsers(currentPage, pageSize).ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _repo.GetById(Convert.ToInt32(id));
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Password,Permission,Age,Gender,image,CreatedAt,UpdatedAt")] User user)
        {
            if (ModelState.IsValid)
            {
                user.CreatedAt = user.UpdatedAt = DateTime.Now;
                _repo.Update(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _repo.GetById(Convert.ToInt32(id));
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Password,Permission,Age,Gender,image,CreatedAt,UpdatedAt")] User user)
        {
            if (ModelState.IsValid)
            {
                user.UpdatedAt = DateTime.Now;
                _repo.Update(user);
                return RedirectToAction("Index");
            }
            return View(user);
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
