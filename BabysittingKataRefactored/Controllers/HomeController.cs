using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BabysittingKataRefactored.Models;

namespace BabysittingKataRefactored.Controllers
{
    public class HomeController : Controller
    {
        [TempData]
        public string message { get; set; }
        [TempData]
        public int total { get; set; }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string startTime, string endTime, string familyLetter)
        {
            DateTime start = DateTime.Parse(startTime);
            DateTime end = DateTime.Parse(endTime);
            if(start >= end)
            {
                message = "That time is invalid, please select a valid time.";
                return RedirectToAction("Index");
            }
            TimeSpan timeDifference = TimeDifference(start, end);
            //int costOfAppointment = 0;
            if(familyLetter == "A")
            {
                total = CalculateFamilyATotal(start, end);
            }
            else if (familyLetter == "B")
            {
                total = CalculateFamilyBTotal(start, end);
            }
            else if (familyLetter == "C")
            {
                total = CalculateFamilyCTotal(start, end);
            }
            return RedirectToAction("Result");
        }
        public IActionResult Result(int costOfAppointment)
        {
            return View(costOfAppointment);
        }
        public TimeSpan TimeDifference(DateTime startTime, DateTime endTime)
        {
            TimeSpan differenceBetweenStartAndEndTime = endTime.Subtract(startTime);
            return differenceBetweenStartAndEndTime;
        }
        public string FamilyLetterVerify(string familyLetter)
        {
            if(familyLetter == "A" || familyLetter == "B" || familyLetter == "C")
            {
                return familyLetter;
            }
            else
            {
                familyLetter = "Error";
            }
            return familyLetter;
        }
        public int CalculateFamilyATotal(DateTime start, DateTime end)
        {
            int total = 0;
            DateTime firstCutOffTime = DateTime.Parse("2019-10-10T23:00");
            if (start < firstCutOffTime && end > firstCutOffTime)
            {
                int differenceBetweenStartAndEndOfFirstCutOff = Convert.ToInt32(firstCutOffTime.Subtract(start).TotalHours);
                if (differenceBetweenStartAndEndOfFirstCutOff < 0)
                {
                    differenceBetweenStartAndEndOfFirstCutOff = 0;
                }
                total += differenceBetweenStartAndEndOfFirstCutOff * 15;
                int differenceBetweenEndTimeAndEndOfFirstCutOff = Convert.ToInt32(end.Subtract(firstCutOffTime).TotalHours);
                if (differenceBetweenEndTimeAndEndOfFirstCutOff < 0)
                {
                    differenceBetweenEndTimeAndEndOfFirstCutOff = 0;
                }
                total += differenceBetweenEndTimeAndEndOfFirstCutOff * 20;
            }
            else if(start >= firstCutOffTime)
            {
                total += Convert.ToInt32(end.Subtract(start).TotalHours) * 20;
            }
            else if(start < end && end <= firstCutOffTime)
            {
                total += Convert.ToInt32(end.Subtract(start).TotalHours) * 15;
            }
            return total;
        }
        public int CalculateFamilyBTotal(DateTime start, DateTime end)
        {
            int total = 0;
            DateTime firstCutOffTime = DateTime.Parse("2019-10-10T22:00");
            DateTime secondCutOffTime = DateTime.Parse("2019-10-11T00:00");
            if(start < firstCutOffTime && end > secondCutOffTime)
            {
                int differenceBetweenStartAndFirstCutOff = Convert.ToInt32(firstCutOffTime.Subtract(start).TotalHours);
                total += differenceBetweenStartAndFirstCutOff * 12;
                int differenceBetweenTheCutOffTimes = Convert.ToInt32(secondCutOffTime.Subtract(firstCutOffTime).TotalHours);
                total += differenceBetweenTheCutOffTimes * 8;
                int differenceBetweenSecondCutOffAndEnd = Convert.ToInt32(end.Subtract(secondCutOffTime).TotalHours);
                total += differenceBetweenSecondCutOffAndEnd * 16;
            }
            else if(end <= firstCutOffTime)
            {
                int differenceBetweenEndandStart = Convert.ToInt32(end.Subtract(start).TotalHours);
                total += differenceBetweenEndandStart * 12;
            }
            else if(start < firstCutOffTime && end <= secondCutOffTime)
            {
                int differenceBetweenFirstCutOffAndStart = Convert.ToInt32(firstCutOffTime.Subtract(start).TotalHours);
                total += differenceBetweenFirstCutOffAndStart * 12;
                int differenceBetweenEndAndFirstCutOff = Convert.ToInt32(end.Subtract(firstCutOffTime).TotalHours);
                total += differenceBetweenEndAndFirstCutOff * 8;
            }
            else if(start >= firstCutOffTime && start < secondCutOffTime && end > secondCutOffTime)
            {
                int differenceBetweenStartAndSecondCutOff = Convert.ToInt32(secondCutOffTime.Subtract(start).TotalHours);
                total += differenceBetweenStartAndSecondCutOff * 8;
                int differenceBetweenEndAndSecondCutOff = Convert.ToInt32(end.Subtract(secondCutOffTime).TotalHours);
                total += differenceBetweenEndAndSecondCutOff * 16;
            }
            else if(start >= firstCutOffTime && end <= secondCutOffTime)
            {
                int differenceBetweenStartAndEnd = Convert.ToInt32(end.Subtract(start).TotalHours);
                total += differenceBetweenStartAndEnd * 8;
            }
            else if(start>=secondCutOffTime)
            {
                int differenceBetweenStartAndEnd = Convert.ToInt32(end.Subtract(start).TotalHours);
                total += differenceBetweenStartAndEnd * 16;
            }
            return total;
        }
        public int CalculateFamilyCTotal(DateTime start, DateTime end)
        {
            int total = 0;
            DateTime firstCutOffTime = DateTime.Parse("2019-10-10T23:00");
            if (start < firstCutOffTime && end > firstCutOffTime)
            {
                int differenceBetweenStartAndFirstCutOff = Convert.ToInt32(firstCutOffTime.Subtract(start).TotalHours);
                if (differenceBetweenStartAndFirstCutOff < 0)
                {
                    differenceBetweenStartAndFirstCutOff = 0;
                }
                total += differenceBetweenStartAndFirstCutOff * 21;
                int differenceBetweenEndTimeAndFirstCutOff = Convert.ToInt32(end.Subtract(firstCutOffTime).TotalHours);
                if (differenceBetweenEndTimeAndFirstCutOff < 0)
                {
                    differenceBetweenEndTimeAndFirstCutOff = 0;
                }
                total += differenceBetweenEndTimeAndFirstCutOff * 15;
            }
            else if (start >= firstCutOffTime)
            {
                total += Convert.ToInt32(end.Subtract(start).TotalHours) * 15;
            }
            else if (start < end && end <= firstCutOffTime)
            {
                total += Convert.ToInt32(end.Subtract(start).TotalHours) * 21;
            }
            return total;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
