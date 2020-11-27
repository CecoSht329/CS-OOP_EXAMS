

using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Repositories.Contracts;
using System.Linq;

namespace EasterRaces.Repositories.Entities
{
    public class CarRepository : Repository<ICar>, IRepository<ICar>
    {
        public override ICar GetByName(string name)
        {
            ICar result = this.GetAll().FirstOrDefault(c => c.Model == name);
            return result;
        }
    }
}
