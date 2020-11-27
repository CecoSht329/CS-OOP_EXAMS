

using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Utilities.Messages;
using EasterRaces.Utilities.Validators;
using System;

namespace EasterRaces.Models.Drivers.Entities
{
    public class Driver : IDriver
    {
        private const int MinimumNameLength = 5;
        private string name;

        public Driver(string name)
        {
            Name = name;
        }

        public string Name
        {
            get => name;
            private set
            {
                SettterValidator
                    .ValidateName(value, MinimumNameLength
                    , string.Format(ExceptionMessages.InvalidName, value, MinimumNameLength));
                name = value;
            }
        }

        public ICar Car { get; private set; }

        public int NumberOfWins { get; private set; }

        public bool CanParticipate => Car != null;

        public void AddCar(ICar car)
        {
            if (car == null)
            {
                throw new ArgumentNullException(ExceptionMessages.CarInvalid);
            }
            Car = car;
        }
        public void WinRace()
        {
            NumberOfWins++;
        }

    }
}
