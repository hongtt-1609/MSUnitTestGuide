using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantApp.Core;

namespace RestaurantApp.Infrastructure.Repositories
{
    public class CartRepository : RestaurantApp.Core.Interfaces.ICartRepository
    {
        private VitualRestaurantContext _context;
        public CartRepository(VitualRestaurantContext context)
        {
            _context = context;
        }

        public Cart GetById(int id)
        {
            var result = (from c in _context.Cart
                          where c.Id == id
                          select c)
                          .FirstOrDefault();
            return result;
        }

        public bool Remove(int id)
        {
            if (id <= 0) return false;
            Cart item = _context.Set<Cart>().Find(id);
            if (item == null) return false;
            _context.Set<Cart>().Remove(item);
            _context.SaveChanges();
            return true;
        }

        public void Update(Cart cart)
        {
            if (cart.Id > 0)
            {
                _context.SetModified(cart);
            }
            else
            {
                _context.Cart.Add(cart);
            }
            _context.SaveChanges();
        }
    }
}
