using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using RestaurantApp.Core;
namespace RestaurantApp.Infrastructure.Repositories
{
    public class UnitRepository : RestaurantApp.Core.Interfaces.IUnitRepository
    {
        private VitualRestaurantContext _context;
        public UnitRepository(VitualRestaurantContext context)
        {
            _context = context;
        }


        public Unit GetById(int id)
        {
            var result = (from u in _context.Unit
                          where u.Id == id
                          select u)
                          .FirstOrDefault();
            return result;
        }

        public IEnumerable<Unit> GetUnits(int page, int pageSize)
        {
            IEnumerable<Unit> list = (from u in _context.Unit
                                      orderby u.Id descending
                                      select u).Skip(page*pageSize).Take(pageSize)
                                      .ToList<Unit>();
            return list;

        }

        public bool Remove(int id)
        {

            if (id > 0)
            {
                Unit item = _context.Set<Unit>().Find(id);
                if (item != null)
                {
                    _context.Set<Unit>().Remove(item);
                    _context.SaveChanges();
                    return true;
                }
                else return false;
            }
            else return false;
        }

        public void Update(Unit unit)
        {
            if (unit.Id > 0)
            {
                _context.SetModified(unit);
            }
            else
            {
                _context.Unit.Add(unit); 
            }
            _context.SaveChanges();
        }
    }
}
