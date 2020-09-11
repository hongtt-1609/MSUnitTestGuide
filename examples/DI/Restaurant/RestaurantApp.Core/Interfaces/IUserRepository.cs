using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Core.Interfaces
{
    public interface IUserRepository
    {
        void Update(User user);
        bool Remove(int id);
        User GetById(int id);
        IEnumerable<User> GetUsers(int page, int pageSize);
    }
}
