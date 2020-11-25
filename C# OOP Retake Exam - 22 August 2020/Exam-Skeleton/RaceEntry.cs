namespace TheRace
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RaceEntry
    {
        private const string ExistingDriver = "Driver {0} is already added.";
        private const string DriverInvalid = "Driver cannot be null.";
        private const string DriverAdded = "Driver {0} added in race.";
        private const int MinParticipants = 2;
        private const string RaceInvalid = "The race cannot start with less than {0} participants.";

        private Dictionary<string, UnitDriver> driver;
        //Test-done
        public RaceEntry()
        {
            this.driver = new Dictionary<string, UnitDriver>();
        }
        //Test-done
        public int Counter
            => this.driver.Count;
        //Test-done
        public string AddDriver(UnitDriver driver)
        {
            //Test-done
            if (driver == null)
            {
                throw new InvalidOperationException(DriverInvalid);
            }
            //Test-done
            if (this.driver.ContainsKey(driver.Name))
            {
                throw new InvalidOperationException(string.Format(ExistingDriver, driver.Name));
            }

            this.driver.Add(driver.Name, driver);

            string result = string.Format(DriverAdded, driver.Name);

            return result;
        }
        //Test-done
        public double CalculateAverageHorsePower()
        {
            //Test-
            if (this.driver.Count < MinParticipants)
            {
                throw new InvalidOperationException(string.Format(RaceInvalid, MinParticipants));
            }

            double averageHorsePower = this.driver
                .Values
                .Select(x => x.Car.HorsePower)
                .Average();

            return averageHorsePower;
        }
    }
}