using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantApp.Core;

namespace RestaurantApp.Infrastructure.Repositories
{
    public class CartDetailRepository : Core.Interfaces.ICartDetailRepository
    {
        private VitualRestaurantContext _context;
        public CartDetailRepository(VitualRestaurantContext context)
        {
            _context = context;
        }

        public CartDetail GetById(int id)
        {
            var result = (from c in _context.CartDetail
                          where c.Id == id
                          select c)
                          .FirstOrDefault();
            return result;
        }

        public IEnumerable<CartDetail> GetDetails(int cartId)
        {
            IEnumerable<CartDetail> list = (from c in _context.CartDetail
                                            where c.CartId == cartId
                                            orderby c.Id descending
                                            select c)
                                            .ToList();
            return list;
        }

        public bool Remove(int id)
        {
            if (id <= 0) return false;
            var item = _context.Set<CartDetail>().Find(id);
            if (item == null) return false;
            _context.Set<CartDetail>().Remove(item);
            _context.SaveChanges();
            return true;
        }

        public void Update(CartDetail cartDetail)
        {
            if (cartDetail.Id > 0)
            {
                _context.SetModified(cartDetail);
            }
            else
            {
                _context.CartDetail.Add(cartDetail);
            }
            _context.SaveChanges();
        }
    }
}
