using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BabysittingKataRefactored.Controllers;
using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;


namespace Tests
{
    public class InformationPassingTests
    {

        [SetUp]
        public void Setup()
        {
           
        }

        [TestCase("2019-10-10 17:00:00", "2019-10-10 18:00:00")]
        [Test]
        public void TimeDifferenceTest1hr(DateTime start, DateTime end)
        {
            TimeSpan differenceBetweenStartAndEndTime = TimeSpan.Parse("01:00:00");
            var homeController = new HomeController();
            TimeSpan testedValue = homeController.TimeDifference(start, end);
            Assert.AreEqual(differenceBetweenStartAndEndTime, testedValue);
        }
        [TestCase("2019-10-10 17:00:00", "2019-10-10 19:00:00")]
        [Test]
        public void TimeDifferenceTest2hr(DateTime start, DateTime end)
        {
            TimeSpan differenceBetweenStartAndEndTime = TimeSpan.Parse("02:00:00");
            var homeController = new HomeController();
            TimeSpan testedValue = homeController.TimeDifference(start, end);
            Assert.AreEqual(differenceBetweenStartAndEndTime, testedValue);
        }
        [TestCase("2019-10-10 17:00:00", "2019-10-10 20:00:00")]
        [Test]
        public void TimeDifferenceTest3hr(DateTime start, DateTime end)
        {
            TimeSpan differenceBetweenStartAndEndTime = TimeSpan.Parse("03:00:00");
            var homeController = new HomeController();
            TimeSpan testedValue = homeController.TimeDifference(start, end);
            Assert.AreEqual(differenceBetweenStartAndEndTime, testedValue);
        }
        [TestCase("A")]
        [TestCase("B")]
        [TestCase("C")]
        [Test]
        public void TestFamilyChoice(string familyLetter)
        {
            var homeController = new HomeController();
            string familyLetterVerified = homeController.FamilyLetterVerify(familyLetter);
            Assert.AreEqual(familyLetter, familyLetterVerified);
        }
    }
    public class CalculationTests
    {
        [SetUp]
        public void Setup()
        {

        }
        #region Family A Calculations
        [TestCase("2019-10-10T17:00", "2019-10-11T04:00", ExpectedResult = 190)]
        [TestCase("2019-10-10T19:00", "2019-10-11T00:00", ExpectedResult = 80)]
        [Test]
        public int FamilyATestCalculationWithStartBeforeCutOffAndEndAfterCutOff(DateTime start, DateTime end)
        {
            int total = 0;
            var homeController = new HomeController();
            total = homeController.CalculateFamilyATotal(start, end);
            return total;
        }
        [TestCase("2019-10-10T23:00", "2019-10-11T04:00", ExpectedResult = 100)]
        [TestCase("2019-10-11T00:00", "2019-10-11T03:00", ExpectedResult = 60)]
        [Test]
        public int FamilyATestCalculationWithStartAfterCutOffAndEndAfterCutOff(DateTime start, DateTime end)
        {
            int total = 0;
            var homeController = new HomeController();
            total = homeController.CalculateFamilyATotal(start, end);
            return total;
        }
        [TestCase("2019-10-10T17:00", "2019-10-10T23:00", ExpectedResult = 90)]
        [TestCase("2019-10-10T19:00", "2019-10-10T21:00", ExpectedResult = 30)]
        [Test]
        public int FamilyATestCalculationWithStartBeforeCutOffAndEndBeforeCutOff(DateTime start, DateTime end)
        {
            int total = 0;
            var homeController = new HomeController();
            total = homeController.CalculateFamilyATotal(start, end);
            return total;
        }
        #endregion
        #region Family C Calculations
        [TestCase("2019-10-10T17:00", "2019-10-11T04:00", ExpectedResult = 126+75)]
        [TestCase("2019-10-10T19:00", "2019-10-11T00:00", ExpectedResult = 84+15)]
        [Test]
        public int FamilyCTestCalculationWithStartBeforeCutOffAndEndAfterCutOff(DateTime start, DateTime end)
        {
            int total = 0;
            var homeController = new HomeController();
            total = homeController.CalculateFamilyCTotal(start, end);
            return total;
        }
        [TestCase("2019-10-10T23:00", "2019-10-11T04:00", ExpectedResult = 75)]
        [TestCase("2019-10-11T00:00", "2019-10-11T03:00", ExpectedResult = 45)]
        [Test]
        public int FamilyCTestCalculationWithStartAfterCutOffAndEndAfterCutOff(DateTime start, DateTime end)
        {
            int total = 0;
            var homeController = new HomeController();
            total = homeController.CalculateFamilyCTotal(start, end);
            return total;
        }
        [TestCase("2019-10-10T17:00", "2019-10-10T23:00", ExpectedResult = 126)]
        [TestCase("2019-10-10T19:00", "2019-10-10T21:00", ExpectedResult = 42)]
        [Test]
        public int FamilyCTestCalculationWithStartBeforeCutOffAndEndBeforeCutOff(DateTime start, DateTime end)
        {
            int total = 0;
            var homeController = new HomeController();
            total = homeController.CalculateFamilyCTotal(start, end);
            return total;
        }
#endregion
        #region Family B Calculations
        [TestCase("2019-10-10T17:00", "2019-10-11T04:00", ExpectedResult = 60+16+64)]
        [TestCase("2019-10-10T19:00", "2019-10-11T02:00", ExpectedResult = 16+36+32)]
        [Test]
        public int FamilyBCalculationWithStartBeforeFirstCutOffAndEndAfterSecondCutOff(DateTime start, DateTime end)
        {
            int total = 0;
            var homeController = new HomeController();
            total = homeController.CalculateFamilyBTotal(start, end);
            return total;
        }
        [TestCase("2019-10-10T23:00", "2019-10-11T04:00", ExpectedResult =  8 + 64)]
        [TestCase("2019-10-10T22:00", "2019-10-11T02:00", ExpectedResult =  16 + 32)]
        [Test]
        public int FamilyBCalculationWithStartAfterFirstCutOffAndEndAfterSecondCutOff(DateTime start, DateTime end)
        {
            int total = 0;
            var homeController = new HomeController();
            total = homeController.CalculateFamilyBTotal(start, end);
            return total;
        }
        [TestCase("2019-10-10T22:00", "2019-10-11T00:00", ExpectedResult = 16)]
        [TestCase("2019-10-10T23:00", "2019-10-11T00:00", ExpectedResult = 8)]
        [TestCase("2019-10-10T22:00", "2019-10-10T23:00", ExpectedResult = 8)]
        [Test]
        public int FamilyBCalculationWithStartAfterFirstCutOffAndEndBeforeSecondCutOff(DateTime start, DateTime end)
        {
            int total = 0;
            var homeController = new HomeController();
            total = homeController.CalculateFamilyBTotal(start, end);
            return total;
        }
        [TestCase("2019-10-11T00:00", "2019-10-11T04:00", ExpectedResult = 64)]
        [TestCase("2019-10-11T02:00", "2019-10-11T03:00", ExpectedResult = 16)]
        [Test]
        public int FamilyBCalculationWithStartAfterSecondCutOffAndEndAfterSecondCutOff(DateTime start, DateTime end)
        {
            int total = 0;
            var homeController = new HomeController();
            total = homeController.CalculateFamilyBTotal(start, end);
            return total;
        }
        [TestCase("2019-10-10T17:00", "2019-10-10T21:00", ExpectedResult = 48)]
        [TestCase("2019-10-10T19:00", "2019-10-10T20:00", ExpectedResult = 12)]
        [TestCase("2019-10-10T17:00", "2019-10-10T22:00", ExpectedResult = 60)]

        [Test]
        public int FamilyBCalculationWithStartBeforeFirstCutOffAndEndBeforeFirstCutOff(DateTime start, DateTime end)
        {
            int total = 0;
            var homeController = new HomeController();
            total = homeController.CalculateFamilyBTotal(start, end);
            return total;
        }
        [TestCase("2019-10-10T17:00", "2019-10-10T23:00", ExpectedResult = 68)]
        [TestCase("2019-10-10T19:00", "2019-10-10T23:00", ExpectedResult = 44)]
        [TestCase("2019-10-10T19:00", "2019-10-11T00:00", ExpectedResult = 52)]
        [Test]
        public int FamilyBCalculationWithStartBeforeFirstCutOffAndEndBeforeSecondCutOff(DateTime start, DateTime end)
        {
            int total = 0;
            var homeController = new HomeController();
            total = homeController.CalculateFamilyBTotal(start, end);
            return total;
        }
        #endregion
    }
}