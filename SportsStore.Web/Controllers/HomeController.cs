﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsStore.Web.EF;

namespace SportsStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(long id = 1)
        {
            ViewBag.AveragePrice = await _context.Products.AverageAsync(p => p.Price);
            return View(await _context.Products.FindAsync(id));
        }

        public IActionResult List()
        {
            return View(_context.Products);
        }

        public IActionResult Html()
        {
            return View((object) "This is a <h3><i>string</i></h3>");
        }
    }
}
