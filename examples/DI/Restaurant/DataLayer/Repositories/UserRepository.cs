using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantApp.Core;

namespace RestaurantApp.Infrastructure.Repositories
{
    public class UserRepository : RestaurantApp.Core.Interfaces.IUserRepository
    {
        private VitualRestaurantContext _context;
        public UserRepository(VitualRestaurantContext context)
        {
            _context = context;
        }

        public User GetById(int id)
        {
            var result = (from u in _context.User
                          where u.Id == id
                          select u).FirstOrDefault();
            return result;
        }

        public IEnumerable<User> GetUsers(int page, int pageSize)
        {
            IEnumerable<User> list = (from u in _context.User
                                      orderby u.Name ascending
                                      select u)
                                      .Skip(page * pageSize)
                                      .Take(pageSize);
            return list;
        }

        public bool Remove(int id)
        {
            if (id <= 0) return false;

            User user = _context.Set<User>().Find(id);
            if (user == null) return false;
            _context.Set<User>().Remove(user);
            _context.SaveChanges();
            return true;

        }

        public void Update(User user)
        {
            if (user == null) throw new Exception("Data can not be null");
            if (user.Id > 0)
            {
                _context.SetModified(user);
            }
            else
            {
                _context.User.Add(user);
            }
            _context.SaveChanges();
        }
    }
}
