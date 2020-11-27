

using EasterRaces.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace EasterRaces.Repositories.Entities
{
    public abstract class Repository<T> : IRepository<T>
    {
        private readonly List<T> collection;
        protected Repository()
        {
            collection = new List<T>();
        }

        public void Add(T model)
        {
            collection.Add(model);
        }

        public bool Remove(T model)
        => collection.Remove(model);

        public abstract T GetByName(string name);

        public IReadOnlyCollection<T> GetAll()
            => collection.AsReadOnly();

    }
}
