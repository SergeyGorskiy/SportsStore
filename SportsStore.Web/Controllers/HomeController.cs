using System;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Web.EF;
using SportsStore.Web.Filters;

namespace SportsStore.Web.Controllers
{
    [HttpsOnly]
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View("Message", "This is the Index action on the Home controller");
        }

        public IActionResult Secure()
        {
            return View("Message", "This is the Secure action on the Home controller");
        }

        [ChangeArg]
        public IActionResult Messages(string message1, string message2 = "None")
        {
            return View("Message", $"{message1}, {message2}");
        }

        [RangeException]
        public ViewResult GenerateException(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            else if (id > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            else
            {
                return View("Message", $"The value is {id}");
            }
        }

        public IActionResult List()
        {
            return View(_context.Products);
        }
    }
}
