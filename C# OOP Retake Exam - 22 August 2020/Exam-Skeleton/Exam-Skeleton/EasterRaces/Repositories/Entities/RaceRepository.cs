

using EasterRaces.Models.Races.Contracts;
using EasterRaces.Repositories.Contracts;
using System.Linq;

namespace EasterRaces.Repositories.Entities
{
    public class RaceRepository : Repository<IRace>, IRepository<IRace>
    {
        public override IRace GetByName(string name)
        {
            IRace result = this.GetAll().FirstOrDefault(d => d.Name == name);
            return result;
        }
    }
}
