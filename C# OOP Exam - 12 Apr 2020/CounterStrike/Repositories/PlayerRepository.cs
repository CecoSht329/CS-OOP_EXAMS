
namespace CounterStrike.Repositories
{
    using CounterStrike.Models.Players.Contracts;
    using CounterStrike.Repositories.Contracts;
    using CounterStrike.Utilities.Messages;
    using CounterStrike.Utilities.Validators;
    using System.Collections.Generic;
    using System.Linq;

    public class PlayerRepository : IRepository<IPlayer>
    {
        private readonly List<IPlayer> models;

        public PlayerRepository()
        {
            models = new List<IPlayer>();
        }

        public IReadOnlyCollection<IPlayer> Models
            => models.AsReadOnly();

        public void Add(IPlayer model)
        {

            DataValidator.ThrowArgumentExceptionIfTryingToAddNullInRepository(model, ExceptionMessages.InvalidPlayerRepository);
            models.Add(model);
        }

        public IPlayer FindByName(string name)
            => models.FirstOrDefault(p => p.Username == name);

        public bool Remove(IPlayer model)
            => models.Remove(model);
    }
}
