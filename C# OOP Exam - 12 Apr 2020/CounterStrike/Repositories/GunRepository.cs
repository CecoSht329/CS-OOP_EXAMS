namespace CounterStrike.Repositories
{
    using CounterStrike.Models.Guns;
    using CounterStrike.Models.Guns.Contracts;
    using CounterStrike.Repositories.Contracts;
    using CounterStrike.Utilities.Messages;
    using CounterStrike.Utilities.Validators;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GunRepository : IRepository<IGun>
    {
        private readonly List<IGun> models;
        public GunRepository()
        {
            models = new List<IGun>();
        }
        public IReadOnlyCollection<IGun> Models
            => models;

        public void Add(IGun model)
        {
            DataValidator.ThrowArgumentExceptionIfTryingToAddNullInRepository(model, ExceptionMessages.InvalidGunRepository);
            models.Add(model);
        }

        public IGun FindByName(string name)
            => models.FirstOrDefault(g => g.Name == name);

        public bool Remove(IGun model)
            => models.Remove(model);
    }
}
