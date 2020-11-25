namespace EasterRaces.Models.Cars.Entities
{
    using EasterRaces.Models.Cars.Contracts;
    using EasterRaces.Utilities.Messages;
    using System;

    public abstract class Car : ICar
    {
        private const int MinModelLength = 4;
        private string model;
        private int horsePower;
        private double cubicCentimeters;
        private int minHorsePower;
        private int maxHorsePower;
        protected Car(string model, int horsePower, double cubicCentimeters, int minHorsePower, int maxHorsePower)
        {
            this.cubicCentimeters = cubicCentimeters;
            this.minHorsePower = minHorsePower;
            this.maxHorsePower = maxHorsePower;
            Model = model;
            HorsePower = horsePower;

        }

        public string Model
        {
            get => model;
            private set
            {
                if (value.Length < MinModelLength || string.IsNullOrWhiteSpace(value))//TODO-test arg exception
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidModel, value, MinModelLength));
                }
                model = value;
            }
        }
        public int HorsePower
        {
            get => horsePower;
            private set
            {
                if (value < minHorsePower || value > maxHorsePower)//TODO-test arg exception
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidHorsePower, value));
                }
                horsePower = value;
            }
        }
        public double CubicCentimeters => cubicCentimeters;
        public double CalculateRacePoints(int laps)
        {
            double racePoints = CubicCentimeters / HorsePower * laps;
            return racePoints;
        }
    }
}
