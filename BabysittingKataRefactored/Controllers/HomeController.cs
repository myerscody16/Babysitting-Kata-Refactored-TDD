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
        public IActionResult Index(string startTime, string endTime)
        {
            DateTime start = Convert.ToDateTime(startTime);
            DateTime end = Convert.ToDateTime(endTime);
            TimeSpan timeDifference = TimeDifference(start, end);
            return RedirectToActionPermanent("Index");
        }
        public TimeSpan TimeDifference(DateTime startTime, DateTime endTime)
        {
            TimeSpan differenceBetweenStartAndEndTime = endTime.Subtract(startTime);
            return differenceBetweenStartAndEndTime;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
