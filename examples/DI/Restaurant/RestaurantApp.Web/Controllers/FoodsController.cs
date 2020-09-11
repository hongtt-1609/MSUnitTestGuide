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
    public class FoodsController : Controller
    {
        private readonly IFoodRepository _repo;
        public int pageSize = 10;
        public FoodsController(IFoodRepository repo)
        {
            _repo = repo;
        }
        // GET: Foods
        public ActionResult Index(int? type,int? page)
        {
            int currentPage = page == null ? 0 : Convert.ToInt32(page);
            List<Food> list = new List<Food>();
            if (type != null)
            {
                int curType = Convert.ToInt32(type);
                if(curType == (int)FoodType.Food || curType ==(int)FoodType.Drink)
                {
                    list = _repo.GetFoods(curType, currentPage, pageSize).ToList();
                }
            }
            else
            {
                list = _repo.GetFoods(currentPage, pageSize).ToList();
            }
            return View(list);
        }

        // GET: Foods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = _repo.GetById(Convert.ToInt32(id));
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // GET: Foods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CategoryId,Price,UnitId,Point,Amount,Description,Type,Image,CreatedAt,UpdatedAt")] Food food)
        {
            if (ModelState.IsValid)
            {
                food.CreatedAt = food.UpdatedAt = DateTime.Now;
                _repo.Update(food);
                return RedirectToAction("Index");
            }

            return View(food);
        }

        // GET: Foods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = _repo.GetById(Convert.ToInt32(id));
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CategoryId,Price,UnitId,Point,Amount,Description,Type,Image,CreatedAt,UpdatedAt")] Food food)
        {
            if (ModelState.IsValid)
            {
                food.UpdatedAt = DateTime.Now;
                _repo.Update(food);
                return RedirectToAction("Index");
            }
            return View(food);
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
