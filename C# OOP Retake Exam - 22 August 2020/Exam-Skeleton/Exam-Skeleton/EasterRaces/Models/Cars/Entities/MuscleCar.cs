
using EasterRaces.Models.Cars.Contracts;

namespace EasterRaces.Models.Cars.Entities
{
    public class MuscleCar : Car, ICar
    {
        private const double MuscleCarCubicCentimeters = 5000;
        private const int MuscleMinHorsePower = 400;
        private const int MuscleMaxHorsePower = 600;
        public MuscleCar(string model, int horsePower)
            : base(model, horsePower, MuscleCarCubicCentimeters, MuscleMinHorsePower, MuscleMaxHorsePower)
        {
        }
    }
}
