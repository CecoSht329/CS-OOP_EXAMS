using System;
using System.Linq;
using System.Text;
using EasterRaces.Core.Contracts;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Cars.Entities;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Drivers.Entities;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Models.Races.Entities;
using EasterRaces.Repositories.Entities;
using EasterRaces.Utilities.Enums;
using EasterRaces.Utilities.Messages;
using Microsoft.VisualBasic.FileIO;

namespace EasterRaces.Core.Entities
{
    public class ChampionshipController : IChampionshipController
    {
        DriverRepository driverRepository;
        CarRepository carRepository;
        RaceRepository raceRepository;
        public ChampionshipController()
        {
            driverRepository = new DriverRepository();
            carRepository = new CarRepository();
            raceRepository = new RaceRepository();
        }
        public string CreateDriver(string driverName)
        {
            IDriver driver = new Driver(driverName);

            if (driverRepository.GetAll().Any(d => d.Name == driverName))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.DriversExists, driverName));
            }

            driverRepository.Add(driver);

            string result = string.Format(OutputMessages.DriverCreated, driver.Name);
            return result;
        }

        public string CreateCar(string type, string model, int horsePower)
        {
            CarType carType;
            ICar car = null;
            if (Enum.TryParse(type, out carType))
            {
                switch (carType)
                {
                    case CarType.Muscle:
                        car = new MuscleCar(model, horsePower);
                        break;
                    case CarType.Sports:
                        car = new SportsCar(model, horsePower);
                        break;
                }
                if (!carRepository.GetAll().Any(c => c.Model == model /*&& c.HorsePower == horsePower*/))
                {
                    carRepository.Add(car);
                    string result = string.Format(OutputMessages.CarCreated, car.GetType().Name, car.Model);

                    return result;
                }

            }

            throw new ArgumentException(string.Format(ExceptionMessages.CarExists, car.Model));
        }




        public string AddCarToDriver(string driverName, string carModel)
        {

            //TODO - too much copy/paste
            if (!carRepository.GetAll().Any(c => c.Model == carModel))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarNotFound, carModel));
            }
            if (!driverRepository.GetAll().Any(d => d.Name == driverName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));
            }
            IDriver driver = driverRepository.GetByName(driverName);
            ICar car = carRepository.GetByName(carModel);

            driver.AddCar(car);

            var result = string.Format(OutputMessages.CarAdded, driver.Name, car.Model);
            return result;
        }


        public string AddDriverToRace(string raceName, string driverName)
        {

            if (!driverRepository.GetAll().Any(r => r.Name == driverName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));
            }
            if (!raceRepository.GetAll().Any(r => r.Name == raceName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }


            IDriver driver = driverRepository.GetByName(driverName);
            IRace race = raceRepository.GetByName(raceName);

            race.AddDriver(driver);

            var result = string.Format(OutputMessages.DriverAdded, driver.Name, race.Name);
            return result;
        }


        public string CreateRace(string name, int laps)
        {
            //TODO - maybe have error in logic error in other methods wih similar logic
            IRace race = new Race(name, laps);
            if (raceRepository.GetAll().Any(r => r.Name == name))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExists, name));
            }

            raceRepository.Add(race);

            var result = string.Format(OutputMessages.RaceCreated, race.Name);
            return result;
        }


        public string StartRace(string raceName)
        {
            //This method is the big deal.If everything is valid, you should arrange all Drivers and then return the three fastest Drivers.To do this you should sort all Drivers in descending order by the result of CalculateRacePoints method in the Car object.At the end, if everything is valid remove this Race from the race repository.


            if (!raceRepository.GetAll().Any(r => r.Name == raceName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }

            IRace race = raceRepository
                .GetByName(raceName);
            int minParticipantCount = 3;
            if (race.Drivers.Count < minParticipantCount)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceInvalid, raceName, minParticipantCount));
            }
            raceRepository.Remove(race);
            var FirstThree = race.Drivers.OrderByDescending(d => d.Car.CalculateRacePoints(race.Laps)).ToList();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format(OutputMessages.DriverFirstPosition, FirstThree[0].Name, race.Name));
            sb.AppendLine(string.Format(OutputMessages.DriverSecondPosition, FirstThree[1].Name, race.Name));
            sb.AppendLine(string.Format(OutputMessages.DriverThirdPosition, FirstThree[2].Name, race.Name));
            //•	"Driver {first driver name} wins {race name} race."
            //"Driver {second driver name} is second in {race name} race."
            //"Driver {third driver name} is third in {race name} race."
            return sb.ToString().TrimEnd();
        }
    }
}
