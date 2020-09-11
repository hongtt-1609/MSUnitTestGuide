using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Core.Interfaces
{
    public interface IUnitRepository
    {
        void Update(Unit unit);
        bool Remove(int id);
        Unit GetById(int id);
        IEnumerable<Unit> GetUnits(int page, int pageSize);
    }
}
