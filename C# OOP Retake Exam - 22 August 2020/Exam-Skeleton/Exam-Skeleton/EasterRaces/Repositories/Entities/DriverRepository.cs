

using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Repositories.Contracts;
using System.Linq;

namespace EasterRaces.Repositories.Entities
{
    public class DriverRepository : Repository<IDriver>,IRepository<IDriver>
    {
        public override IDriver GetByName(string name)
        {
            IDriver result = this.GetAll().FirstOrDefault(d => d.Name == name);
            return result;
        }
    }
}
