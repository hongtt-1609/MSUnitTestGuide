using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Core.Interfaces
{
    public interface IFoodRepository
    {
        void Update(Food food);
        bool Remove(int id);
        IEnumerable<Food> GetFoods(int type, int page, int pageSize);
        IEnumerable<Food> GetFoods(int page, int pageSize);
        Food GetById(int id);
        IEnumerable<Food> Filter(string name, int type, int cat, int page, int pageSize);
    }
}
