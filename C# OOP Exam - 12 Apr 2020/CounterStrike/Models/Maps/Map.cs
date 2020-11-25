namespace CounterStrike.Models.Maps
{
    using CounterStrike.IO;
    using CounterStrike.Models.Maps.Contracts;
    using CounterStrike.Models.Players;
    using CounterStrike.Models.Players.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class Map : IMap
    {
        public string Start(ICollection<IPlayer> players)
        {
            var terrorists = players.Where(t => t.GetType() == typeof(Terrorist)).ToList();
            var counterTerrorists = players.Where(ct => ct.GetType() == typeof(CounterTerrorist)).ToList();


            while (terrorists.Any(t => t.IsAlive) && counterTerrorists.Any(p => p.IsAlive))
            {
                Attack(terrorists, counterTerrorists);

                Attack(counterTerrorists, terrorists);
            }
            string result = terrorists.Any(p => p.IsAlive) ? "Terrorist wins!"
                : "Counter Terrorist wins!";
            return result;
        }

        //TO DO FIND A BETTER WAY OF ATTACKING EACHO OHTER
        private static void Attack(List<IPlayer> attckers, List<IPlayer> attackedGroup)
        {
            foreach (var attacker in attckers.Where(a => a.IsAlive))
            {
                foreach (var attacked in attackedGroup.Where(a => a.IsAlive))
                {
                    int damage = attacker.Gun.Fire();
                    attacked.TakeDamage(damage);
                }
            }
        }
    }
}
