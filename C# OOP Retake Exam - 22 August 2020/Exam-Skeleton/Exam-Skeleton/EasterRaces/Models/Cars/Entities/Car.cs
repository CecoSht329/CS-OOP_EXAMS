
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Utilities.Messages;
using EasterRaces.Utilities.Validators;
using System;

namespace EasterRaces.Models.Cars.Entities
{
    public abstract class Car : ICar
    {
        private const int MinModelLegth = 4;

        private string model;
        private int horsePower;
        private int minHorsePower;
        private int maxHorsePower;
        private double cubicCentimeters;

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
                SettterValidator
                .ValidateName(value, MinModelLegth, string.Format(ExceptionMessages.InvalidModel, value, MinModelLegth));
                model = value;
            }
        }


        public int HorsePower
        {
            get => horsePower;
            private set
            {
                if (value < minHorsePower || value > maxHorsePower)
                {
                    throw new ArgumentException(
                        string.Format(ExceptionMessages.InvalidHorsePower, value));
                }
                horsePower = value;
            }
        }
        public double CubicCentimeters => cubicCentimeters;


        public double CalculateRacePoints(int laps)
        {
            double result = CubicCentimeters / HorsePower * laps;
            return result;
        }
       
    }
}
