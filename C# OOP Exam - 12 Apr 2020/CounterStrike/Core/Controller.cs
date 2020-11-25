using CounterStrike.Core.Contracts;
using CounterStrike.Models.Guns;
using CounterStrike.Models.Guns.Contracts;
using CounterStrike.Models.Maps;
using CounterStrike.Models.Maps.Contracts;
using CounterStrike.Models.Players;
using CounterStrike.Models.Players.Contracts;
using CounterStrike.Repositories;
using CounterStrike.Repositories.Contracts;
using CounterStrike.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CounterStrike.Core
{
    public class Controller : IController
    {
        private GunRepository guns;
        private PlayerRepository players;
        private Map map;
        public Controller()
        {
            guns = new GunRepository();
            players = new PlayerRepository();
            map = new Map();
        }

        public string AddGun(string type, string name, int bulletsCount)
        {
            IGun gun = null;
            if (type == nameof(Pistol))
            {
                gun = new Pistol(name, bulletsCount);
            }
            else if (type == nameof(Rifle))
            {
                gun = new Rifle(name, bulletsCount);
            }
            else
            {
                return ExceptionMessages.InvalidGunType;
            }

            guns.Add(gun);
            return string.Format(OutputMessages.SuccessfullyAddedGun, gun.Name);
        }

        public string AddPlayer(string type, string username, int health, int armor, string gunName)
        {
            IPlayer player = null;
            IGun gun = guns.FindByName(gunName);

            if (gun == null)
            {
                throw new ArgumentException(ExceptionMessages.GunCannotBeFound);
            }

            if (type == nameof(Terrorist))
            {
                player = new Terrorist(username, health, armor, gun);
            }
            else if (type == nameof(CounterTerrorist))
            {
                player = new CounterTerrorist(username, health, armor, gun);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidPlayerType);
            }

            players.Add(player);
            return string.Format(OutputMessages.SuccessfullyAddedPlayer, player.Username);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var player in players.Models
                .OrderBy(x => x.GetType().Name)
                .ThenByDescending(x => x.Health)
                .ThenBy(x => x.Username))
            {
                sb.AppendLine(player.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string StartGame()
            => map.Start((ICollection<IPlayer>)players.Models);
    }
}
