using NUnit.Framework;
using System;
using TheRace;

namespace TheRace.Tests
{
    public class RaceEntryTests
    {
        UnitCar unitCar;
        UnitDriver unitDriver;
        RaceEntry raceEntry;
        [SetUp]
        public void Setup()
        {
            unitCar = new UnitCar("Opel", 100, 150);
            unitDriver = new UnitDriver("Pesho", unitCar);
            raceEntry = new RaceEntry();
        }

        [Test]
        public void RaceEntryCounterPropertyShouldWorkProperly()
        {
            raceEntry.AddDriver(unitDriver);
            int expected = 1;
            int actual = raceEntry.Counter;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddDriverThrowsExceptionIfDriverNull()
        {
            UnitDriver otherUnitDr = null;

            Assert.Throws<InvalidOperationException>(() => raceEntry.AddDriver(otherUnitDr));
        }

        [Test]
        public void AddDriverThrowsExceptionIfDriverPresent()
        {
            raceEntry.AddDriver(unitDriver);
            UnitDriver otherUnitDr = new UnitDriver("Pesho", unitCar);
            Assert.Throws<InvalidOperationException>(() => raceEntry.AddDriver(otherUnitDr));
        }

        [Test]
        public void AddDriverShouldReturnProperResult()
        {
            string expected = string.Format($"Driver {unitDriver.Name} added in race.", unitDriver.Name);
            string actual = raceEntry.AddDriver(unitDriver);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CalculateAverageHorsePowerThrowsExceptionIFCounterBelowTwo()
        {
            raceEntry.AddDriver(unitDriver);

            Assert.Throws<InvalidOperationException>(() => raceEntry.CalculateAverageHorsePower());
        }

        [Test]
        public void CalculateAverageHorsePowerShouldReturnProperResult()
        {
            raceEntry.AddDriver(unitDriver);
            UnitDriver otherUnitDr = new UnitDriver("Ivan", unitCar);
            raceEntry.AddDriver(otherUnitDr);
            double expected = 100;
            double actual = raceEntry.CalculateAverageHorsePower();
            Assert.AreEqual(expected, actual);
        }
    }
}