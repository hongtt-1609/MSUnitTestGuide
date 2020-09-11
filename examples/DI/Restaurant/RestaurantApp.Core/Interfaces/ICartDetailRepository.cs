using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Core.Interfaces
{
   public interface ICartDetailRepository
    {
        void Update(CartDetail cartDetail);
        bool Remove(int id);
        CartDetail GetById(int id);
        IEnumerable<CartDetail> GetDetails(int cartId);
    }
}
