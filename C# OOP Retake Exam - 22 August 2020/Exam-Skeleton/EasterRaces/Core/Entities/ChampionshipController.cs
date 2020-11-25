
using EasterRaces.Core.Contracts;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Cars.Entities;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Drivers.Entities;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Models.Races.Entities;
using EasterRaces.Repositories.Contracts;
using EasterRaces.Repositories.Entities;
using EasterRaces.Utilities.Enums;
using EasterRaces.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasterRaces.Core.Entities
{
    public class ChampionshipController : IChampionshipController
    {
        DriverRepository drivers = new DriverRepository();
        CarRepository cars = new CarRepository();
        RaceRepository races = new RaceRepository();
        public string CreateDriver(string driverName)
        {
            IDriver driver = new Driver(driverName);


            if (drivers.GetAll().Any(d => d.Name == driverName))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.DriversExists, driverName));
            }
            drivers.Add(driver);
            return string.Format(OutputMessages.DriverCreated, driverName);
        }

        public string CreateCar(string type, string model, int horsePower)
        {
            CarType carType;

            ICar car = null;
            if (Enum.TryParse(type, out carType))
            {
                switch (type)
                {
                    case "Muscle":
                        car = new MuscleCar(model, horsePower);
                        break;
                    case "Sports":
                        car = new SportsCar(model, horsePower);
                        break;
                }
                if (!cars.GetAll().Any(c => c.Model == model))
                {
                    cars.Add(car);
                    return string.Format(OutputMessages.CarCreated, car.GetType().Name, car.Model);
                }
            }

            throw new ArgumentException(string.Format(ExceptionMessages.CarExists, car.Model));
        }

        public string CreateRace(string name, int laps)
        {

            IRace race = new Race(name, laps);
            if (!races.GetAll().Any(r => r.Name == name))
            {
                races.Add(race);
                return string.Format(OutputMessages.RaceCreated, name);
            }
            throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExists, name));
        }

        public string AddDriverToRace(string raceName, string driverName)
        {
            if (!drivers.GetAll().Any(r => r.Name == driverName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));
            }
            IDriver driver = drivers.GetByName(driverName);
            if (!races.GetAll().Any(r => r.Name == raceName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }
            IRace race = races.GetByName(raceName);

            race.AddDriver(driver);

            return string.Format(OutputMessages.DriverAdded, driverName, raceName);
        }

        public string AddCarToDriver(string driverName, string carModel)
        {
            if (!cars.GetAll().Any(c => c.Model == carModel))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarNotFound, carModel));
            }
            ICar car = cars.GetByName(carModel);
            if (!drivers.GetAll().Any(d => d.Name == driverName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));
            }
            IDriver driver = drivers.GetByName(driverName);

            driver.AddCar(car);

            return string.Format(OutputMessages.CarAdded, driverName, carModel);
        }

        public string StartRace(string raceName)
        {
            int minparitcipants = 3;
            if (!races.GetAll().Any(r => r.Name == raceName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }
            IRace race = races.GetByName(raceName);
            if (race.Drivers.Count < minparitcipants)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceInvalid, raceName, minparitcipants));
            }

            int counter = 0;
            StringBuilder sb = new StringBuilder();
            foreach (var driver in race.Drivers.OrderByDescending(d => d.Car.CalculateRacePoints(race.Laps)))
            {
                counter++;
                if (counter == 1)
                {
                    sb.AppendLine(string.Format(OutputMessages.DriverFirstPosition, driver.Name, raceName));
                }
                if (counter == 2)
                {
                    sb.AppendLine(string.Format(OutputMessages.DriverSecondPosition, driver.Name, raceName));
                }
                if (counter == 3)
                {
                    sb.AppendLine(string.Format(OutputMessages.DriverThirdPosition, driver.Name, raceName));
                    break;
                }
            }
            return sb.ToString().TrimEnd();
        }
    }
}
