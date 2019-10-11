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
    public class Tests
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
    }
}