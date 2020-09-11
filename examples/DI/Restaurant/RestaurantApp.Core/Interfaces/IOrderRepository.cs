using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Core.Interfaces
{
    public interface IOrderRepository
    {
        void Update(Order order);
        bool Remove(int id);
        Order GetById(int id);
        IEnumerable<Order> GetOrders(int page, int pageSize);
    }
}
