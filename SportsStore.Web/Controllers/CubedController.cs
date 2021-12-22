using System;
using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Web.Controllers
{
    public class CubedController : Controller
    {
        public IActionResult Index()
        {
            return View("Cubed");
        }

        [TempData] public string Value { get; set; }

        [TempData] public string Result { get; set; }

        public IActionResult Cube(double num)
        {
            Value = num.ToString();
            Result = Math.Pow(num, 3).ToString();

            return RedirectToAction(nameof(Index));
        }
    }
}
