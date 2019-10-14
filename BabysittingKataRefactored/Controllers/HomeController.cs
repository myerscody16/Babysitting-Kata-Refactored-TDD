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

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string startTime, string endTime, string familyLetter)
        {
            DateTime start = DateTime.Parse(startTime);
            DateTime end = DateTime.Parse(endTime);
            TimeSpan timeDifference = TimeDifference(start, end);
            int costOfAppointment = 0;
            if(familyLetter == "A")
            {
                costOfAppointment = CalculateFamilyATotal(start, end);
            }
            else if (familyLetter == "B")
            {
                costOfAppointment = CalculateFamilyATotal(start, end);
            }
            else if (familyLetter == "C")
            {
                costOfAppointment = CalculateFamilyATotal(start, end);
            }
            return RedirectToAction("Index",costOfAppointment);
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
        //public int CalculateFamilyBTotal(DateTime start, DateTime end)
        //{
        //    int total = 0;
        //    int firstHourlyTotal = 0;
        //    int secondHourlyTotal = 0;
        //    DateTime firstCutOffTime = DateTime.Parse("2019-10-10T23:00Z");
        //    DateTime secondCutOffTime = DateTime.Parse("2019-11-10T04:00Z");
        //    if (start < firstCutOffTime && end > firstCutOffTime)
        //    {
        //        firstHourlyTotal = Convert.ToInt32(firstCutOffTime.Subtract(start).TotalHours);
        //        secondHourlyTotal = Convert.ToInt32(end.Subtract(firstCutOffTime).TotalHours);
        //        total += firstHourlyTotal * 15;
        //        total += secondHourlyTotal * 20;
        //    }
        //    else if(start)

        //    return total;
        //}
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
