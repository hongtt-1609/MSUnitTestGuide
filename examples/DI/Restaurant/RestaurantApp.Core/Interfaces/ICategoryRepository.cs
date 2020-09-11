using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Core.Interfaces
{
    public interface ICategoryRepository
    {
        void Update(Category cat);
        bool Remove(int id);
        Category GetById(int id);
        IEnumerable<Category> GetCategories(int page, int pageSize);
    }
}
