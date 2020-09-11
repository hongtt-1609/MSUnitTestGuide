using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantApp.Core;
namespace RestaurantApp.Infrastructure.Repositories
{
    public class OrderRepository : RestaurantApp.Core.Interfaces.IOrderRepository
    {
        private VitualRestaurantContext _context;
        public OrderRepository(VitualRestaurantContext context)
        {
            _context = context;
        }


        public Order GetById(int id)
        {
            var result = (from o in _context.Order
                          where o.Id == id
                          select o)
                          .FirstOrDefault();
            return result;
        }

        public IEnumerable<Order> GetOrders(int page, int pageSize)
        {
            IEnumerable<Order> list = (from o in _context.Order
                                       orderby o.Id descending
                                       select o)
                                       .Skip(page * pageSize)
                                       .Take(pageSize)
                                       .ToList();
            return list;

        }

        public bool Remove(int id)
        {
            if (id <= 0) return false;

            var order = _context.Set<Order>().Find(id);
            if (order == null) return false;
            _context.Set<Order>().Remove(order);
            _context.SaveChanges();
            return true;

        }

        public void Update(Order order)
        {
            if (order == null) throw new Exception("Data can not be null");
            if (order.Id > 0)
            {
                _context.SetModified(order);
            }
            else
            {
                _context.Order.Add(order);
            }
            _context.SaveChanges();
        }
    }
}
