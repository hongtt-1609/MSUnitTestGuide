using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantApp.Core;

namespace RestaurantApp.Infrastructure.Repositories
{
    public class CategoryRepository : RestaurantApp.Core.Interfaces.ICategoryRepository
    {
        private VitualRestaurantContext _context;
        public CategoryRepository(VitualRestaurantContext context)
        {
            _context = context;
        }

        public Category GetById(int id)
        {
            var result = (from c in _context.Category
                          where c.Id == id
                          select c).FirstOrDefault();
            return result;
        }

        public IEnumerable<Category> GetCategories(int page, int pageSize)
        {
            IEnumerable<Category> list = (from c in _context.Category
                                          orderby c.Name ascending
                                          select c
                                         )
                                         .Skip(page * pageSize)
                                         .Take(pageSize)
                                         .ToList();
            return list;
        }

        public bool Remove(int id)
        {
            if (id <= 0) return false;
            Category cat = _context.Set<Category>().Find(id);
            if (cat == null) return false;
            _context.Set<Category>().Remove(cat);
            _context.SaveChanges();
            return true;
        }

        public void Update(Category cat)
        {
            if (cat == null) throw new Exception("data can not be null");
            if (cat.Id > 0)
            {
                _context.SetModified(cat);
            }
            else
            {
                _context.Category.Add(cat);
            }
            _context.SaveChanges();
        }
    }
}
