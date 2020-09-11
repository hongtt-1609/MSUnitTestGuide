using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantApp.Core;
namespace RestaurantApp.Infrastructure.Repositories
{
    public class FoodRepository : RestaurantApp.Core.Interfaces.IFoodRepository
    {
        private VitualRestaurantContext _context;
        public FoodRepository(VitualRestaurantContext context)
        {
            _context = context;

        }
        public IEnumerable<Food> Filter(string name, int type, int cat, int page, int pageSize)
        {
            string sql = "Select * from Food ";
            string where = string.Empty;
            string orderBy = " order by Name Asc";
            List<string> conditions = new List<string>();
            if (!string.IsNullOrEmpty(name) || type > 0 || cat > 0)
            {
                where = " Where ";
            }
            if (!string.IsNullOrEmpty(name))
            {
                conditions.Add(string.Format("Name like %{0}%", name));
            }
            if (type > 0)
            {
                conditions.Add(string.Format("Type = {0}", type));
            }
            if (cat > 0)
            {
                conditions.Add(string.Format("CatId = {0}", cat));
            }
            sql = sql + where + string.Join(" and ", conditions) + orderBy;
            IEnumerable<Food> foods = _context.Food.SqlQuery(sql).Skip(page * pageSize).Take(pageSize).ToList<Food>();
            return foods;

        }

        public Food GetById(int id)
        {
            var result = (from f in _context.Food
                          where f.Id == id
                          select f).FirstOrDefault();

            return result;
        }

        public IEnumerable<Food> GetFoods(int type, int page, int pageSize)
        {
            IEnumerable<Food> list = (from f in _context.Food
                                      where f.Type == type
                                      orderby f.Name ascending
                                      select f)
                                      .Skip(pageSize * page)
                                      .Take(pageSize)
                                      .ToList();
            return list;
        }

        public IEnumerable<Food> GetFoods(int page, int pageSize)
        {
            IEnumerable<Food> list = (from f in _context.Food
                                      orderby f.Name ascending
                                      select f)
                                      .Skip(pageSize * page)
                                      .Take(pageSize)
                                     .ToList();
            return list;
        }

        public bool Remove(int id)
        {
            if (id <= 0) return false;
            Food food = _context.Set<Food>().Find(id);
            if (food == null) return false;
            _context.Set<Food>().Remove(food);
            _context.SaveChanges();
            return true;
        }
        public void Update(Food food)
        {
            if (food == null) throw new Exception("Data can not be null");
            if (food.Id > 0)
            {
                //update
                _context.SetModified(food);
            }
            else
            {
                //create
                _context.Food.Add(food);
            }
            _context.SaveChanges();
        }

    }
}
