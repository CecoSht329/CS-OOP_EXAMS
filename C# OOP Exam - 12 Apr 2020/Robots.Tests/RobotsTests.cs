namespace Robots.Tests
{
    using NUnit.Framework;
    using System;

    public class RobotsTests
    {

        Robot robot;
        RobotManager robotManager;
        private const int RobotManagerPositiveTestCapacityValue = 1;
        private const int RobotManagerNegativeTestCapacityValue = -1;
        [SetUp]
        public void SetUp()
        {
            robot = new Robot("Pesho", 100);
            robotManager = new RobotManager(RobotManagerPositiveTestCapacityValue);

        }

        [Test]
        public void TestIfRobotManagerConstructorWorksProperly()
        {
            int expectedCapacity = RobotManagerPositiveTestCapacityValue;
            int actualCapacity = robotManager.Capacity;
            Assert.AreEqual(expectedCapacity, actualCapacity);
        }


        [Test]
        public void TestIfCapacitySetterThrowsExceptionIfCapcityIsNegative()
        {
            Assert.That(() =>
            {
                robotManager = new RobotManager(RobotManagerNegativeTestCapacityValue);
            }, Throws.ArgumentException.With.Message
            .EqualTo("Invalid capacity!"));
        }


        [Test]
        public void CountPropertyShouldReturnProperCount()
        {
            int expectedCount = 0;
            int actualCount = robotManager.Count;
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void AddMethodShouldAddToRobotManagerCollection()
        {
            robotManager.Add(robot);

            Robot expectedRobot = new Robot("Pesho", 100);
            Robot actualRobot = robot;
            Assert.AreEqual(expectedRobot.Name, actualRobot.Name);
            Assert.AreEqual(expectedRobot.Battery, actualRobot.Battery);
        }

        [Test]
        public void AddMethodShouldThrowExceptionWhenRobotAddedExists()
        {
            robotManager.Add(robot);

            Robot otherRobot = new Robot("Pesho", 100);
            Assert.That(() =>
            {
                robotManager.Add(otherRobot);
            }, Throws.InvalidOperationException
            .With.Message.EqualTo($"There is already a robot with name {otherRobot.Name}!"));
        }

        [Test]
        public void AddMethodShouldThrowExceptionWhenCapacityFull()
        {
            robotManager.Add(robot);

            Robot otherRobot = new Robot("Ivan", 100);
            Assert.That(() =>
            {
                robotManager.Add(otherRobot);
            }, Throws.InvalidOperationException
            .With.Message.EqualTo("Not enough capacity!"));
        }

        [Test]
        public void RemoveMethodShouldRemoveRobotByName()
        {
            robotManager.Add(robot);

            robotManager.Remove("Pesho");

            int expectedCount = 0;
            int actualCount = robotManager.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void RemoveMethodThrowsExceptionIfRobotIsNull()
        {
            robotManager.Add(robot);

            Assert.That(() =>
            {
                robotManager.Remove("Ivan");
            }, Throws.InvalidOperationException
            .With.Message.EqualTo($"Robot with the name Ivan doesn't exist!"));
        }

        [Test]
        public void WorkMethodShouldDrainBatteryIfBatteryUsageIsValid()
        {
            robotManager.Add(robot);

            robotManager.Work(robot.Name, "Clean", 100);

            int expectedBattery = 0;
            int actualBattery = robot.Battery;

            Assert.AreEqual(expectedBattery, actualBattery);
        }

        [Test]
        public void WorkMethodThrowsExceptionIfRobotIsNull()
        {
            robotManager.Add(robot);

            Assert.That(() =>
            {
                robotManager.Work("Ivan", "Clean", 100);
            }, Throws.InvalidOperationException
            .With.Message.EqualTo($"Robot with the name Ivan doesn't exist!"));
        }


        [Test]
        public void WorkMethodThrowsexceptionIfBateryUsageIsBiggerThanBattery()
        {
            robotManager.Add(robot);


            Assert.That(() =>
            {
                robotManager.Work(robot.Name, "Clean", 101);
            }, Throws.InvalidOperationException
             .With.Message.EqualTo($"{robot.Name} doesn't have enough battery!"));
        }

        [Test]
        public void ChargeMethodShouldChargeBatteryToMaximum()
        {
            robotManager.Add(robot);

            robotManager.Charge(robot.Name);

            int expected = robot.Battery;
            int actual = robot.MaximumBattery;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ChargeMethodShouldThrowExceptionIfRobotNull()
        {
            robotManager.Add(robot);
            Assert.That(() =>
            {
                robotManager.Charge("Ivan");
            }, Throws.InvalidOperationException
            .With.Message.EqualTo($"Robot with the name Ivan doesn't exist!"));
        }

        [Test]
        public void RobotConstructorShouldWorkProperly()
        {
            string expectedRobotName = "Pesho";
            string actualRobotName = robot.Name;
            int expectedMaxBat = 100;
            int actualMaxBat = robot.MaximumBattery;
            int expectedBat = 100;
            int actualBat = robot.MaximumBattery;
            Assert.AreEqual(expectedRobotName, actualRobotName);
            Assert.AreEqual(expectedMaxBat, actualMaxBat);
            Assert.AreEqual(expectedBat, actualBat);
        }
    }
}
