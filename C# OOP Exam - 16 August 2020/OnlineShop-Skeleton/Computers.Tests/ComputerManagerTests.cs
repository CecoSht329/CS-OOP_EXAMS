using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Computers.Tests
{
    public class Tests
    {
        ComputerManager computerManager;
        Computer computer;
        [SetUp]
        public void Setup()
        {
            computerManager = new ComputerManager();
            computer = new Computer("Man", "Onzi", 2.50m);
        }

        [Test]
        public void ConstructorShouldInstantiateACollectionOfComputers()
        {
            Assert.IsNotNull(computerManager.Computers);
        }


        [Test]
        public void CountPropertyShouldReturnTheCountOfComputersInCompManager()
        {
            int expectedCount = 0;
            int actualCount = computerManager.Count;
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void AddComputerMethodShouldAddCompToCollection()
        {
            computerManager.AddComputer(computer);
            int expectedCount = 1;
            int actualCount = computerManager.Count;
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void AddComputerMethodShouldThrowExceptionIfCompExists()
        {
            computerManager.AddComputer(computer);
            Computer anotherComputer = new Computer("Man", "Onzi", 2.40m);
            Assert.That(() =>
            {
                computerManager.AddComputer(anotherComputer);
            }, Throws.ArgumentException.With.Message.EqualTo("This computer already exists."));
        }

        [Test]
        public void RemoveComputerShouldRemoveFromCollection()
        {
            computerManager.AddComputer(computer);
            computerManager.RemoveComputer(computer.Manufacturer, computer.Model);
            CollectionAssert.IsEmpty(computerManager.Computers);
        }

        [Test]
        public void RemoveComputerShouldReturnRemovedComputer()
        {
            computerManager.AddComputer(computer);

            Computer expectedComputer = new Computer("Man", "Onzi", 2.50m);
            Computer actualComputer = computerManager.RemoveComputer(computer.Manufacturer, computer.Model);

            Assert.AreEqual(expectedComputer.Manufacturer, actualComputer.Manufacturer);
            Assert.AreEqual(expectedComputer.Model, actualComputer.Model);
            Assert.AreEqual(expectedComputer.Price, actualComputer.Price);
        }

        [Test]
        public void GetComputerShouldReturnTheProperComputer()
        {
            computerManager.AddComputer(computer);

            Computer expectedComputer = new Computer("Man", "Onzi", 2.50m);
            Computer actualComputer = computerManager.GetComputer(computer.Manufacturer, computer.Model);

            Assert.AreEqual(expectedComputer.Manufacturer, actualComputer.Manufacturer);
            Assert.AreEqual(expectedComputer.Model, actualComputer.Model);
            Assert.AreEqual(expectedComputer.Price, actualComputer.Price);
        }

        [Test]
        public void GetComputerShouldThrowExceptionIfManufacturerNull()
        {
            computerManager.AddComputer(computer);


            Assert.Throws<ArgumentNullException>(() => computerManager.GetComputer(null, computer.Model));
        }
        [Test]
        public void GetComputerShouldThrowExceptionIfModelNull()
        {
            computerManager.AddComputer(computer);


            Assert.Throws<ArgumentNullException>(() => computerManager.GetComputer(computer.Manufacturer, null));
        }

        [Test]
        public void GetComputerShouldThrowExceptionIfComputerIsNull()
        {
            Assert.Throws<ArgumentException>(() => computerManager.GetComputer(computer.Manufacturer, computer.Model));
        }

        [Test]
        public void GetComputersByManufacturerReturnsCompCollectionWithManifct()
        {
            computerManager.AddComputer(computer);
            Computer otherComputer = new Computer("Man", "Tozi", 3.40m);
            computerManager.AddComputer(otherComputer);

            ICollection<Computer> expected = (ICollection<Computer>)computerManager.Computers;
            ICollection<Computer> actual = computerManager.GetComputersByManufacturer("Man");

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetComputersByManufacturerShouldThrowExeptionIfManifactNull()
        {
            Assert.Throws<ArgumentNullException>(() => computerManager.GetComputersByManufacturer(null));
        }
    }
}