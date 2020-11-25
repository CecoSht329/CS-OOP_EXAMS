
using EasterRaces.Repositories.Contracts;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace EasterRaces.Repositories.Entities
{
    public abstract class Repository<T> : IRepository<T>
    {
      private readonly  List<T> models;
        public Repository()
        {
            models = new List<T>();
        }


        public abstract T GetByName(string name);//TODO
       

        public IReadOnlyCollection<T> GetAll()
        {
            return models.AsReadOnly();
        }

        public void Add(T model)
        {
            models.Add(model);
        }

        public bool Remove(T model)
       => models.Remove(model);
    }
}
