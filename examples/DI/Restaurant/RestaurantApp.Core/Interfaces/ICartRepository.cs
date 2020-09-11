using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Core.Interfaces
{
    public interface ICartRepository
    {

        void Update(Cart cart);
        bool Remove(int id);
        Cart GetById(int id);
    }
}
