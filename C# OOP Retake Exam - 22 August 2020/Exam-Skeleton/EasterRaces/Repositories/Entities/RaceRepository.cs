
using EasterRaces.Models.Races.Contracts;
using System.Linq;

namespace EasterRaces.Repositories.Entities
{
    public class RaceRepository : Repository<IRace>
    {
        public override IRace GetByName(string name)
        {
            return this.GetAll().FirstOrDefault(c => c.Name == name);
        }
    }
}
