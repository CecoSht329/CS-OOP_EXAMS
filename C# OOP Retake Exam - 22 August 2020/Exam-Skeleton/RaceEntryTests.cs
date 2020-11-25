using NUnit.Framework;
using System;
using TheRace;

namespace TheRace.Tests
{
    public class RaceEntryTests
    {

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void AddDriverMethodShouldWorkCorrectly()
        {
            //Arrange
            UnitCar unitCar = new UnitCar("Mazda", 200, 200);
            UnitDriver unitDriver = new UnitDriver("Pesho", unitCar);
            RaceEntry raceEntry = new RaceEntry();

            //Act
            raceEntry.AddDriver(unitDriver);
            int expectedCount = 1;
            int actualCount = raceEntry.Counter;
            //Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void AddDriverMethodShouldThrowExceptionIfDriverNull()
        {
            //Arrange
            UnitDriver unitDriver = null;
            RaceEntry raceEntry = new RaceEntry();

            //Assert
            Assert.Throws<InvalidOperationException>(() => raceEntry.AddDriver(unitDriver));
        }

        [Test]
        public void AddDriverMethodShouldThrowExceptionIfDriverExists()
        {
            //Arrange
            UnitCar unitCar = new UnitCar("Mazda", 200, 200);
            UnitDriver unitDriver = new UnitDriver("Pesho", unitCar);
            RaceEntry raceEntry = new RaceEntry();
            raceEntry.AddDriver(unitDriver);

            //Assert
            Assert.Throws<InvalidOperationException>(() => raceEntry.AddDriver(unitDriver));
        }


        [Test]
        public void CalculateAverageHorsePowerMethodShouldWorkCorrectly()
        {
            //Arrange
            UnitCar unitCar = new UnitCar("Mazda", 200, 200);
            UnitCar unitCar2 = new UnitCar("Mazna", 200, 200);
            UnitDriver unitDriver = new UnitDriver("Pesho", unitCar);
            UnitDriver unitDriver2 = new UnitDriver("GOsho", unitCar2);
            RaceEntry raceEntry = new RaceEntry();

            //Act
            raceEntry.AddDriver(unitDriver);
            raceEntry.AddDriver(unitDriver2);
            double expectedAvaerage = 200;
            double actualAverage = raceEntry.CalculateAverageHorsePower();
            //Assert
            Assert.AreEqual(expectedAvaerage, actualAverage);
        }

        [Test]
        public void CalculateAverageHorsePowerMethodShouldThrowExceptionIfDriversLessThanTwo()
        {
            //Arrange
            UnitCar unitCar = new UnitCar("Mazda", 200, 200);
            UnitDriver unitDriver = new UnitDriver("Pesho", unitCar);
            RaceEntry raceEntry = new RaceEntry();

            //Act
            raceEntry.AddDriver(unitDriver);
            //Assert
            Assert.Throws<InvalidOperationException>(() => raceEntry.CalculateAverageHorsePower());
        }
    }
}