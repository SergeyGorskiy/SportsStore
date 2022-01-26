using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsStore.Web.EF;
using SportsStore.Web.Filters;
using SportsStore.Web.Models;
using SportsStore.Web.ViewModels;

namespace SportsStore.Web.Controllers
{
    //[HttpsOnly]
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        

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

        // Создание приложений с формами

        private readonly DataContext _context;

        private IEnumerable<Category> Categories => _context.Categories;
        private IEnumerable<Supplier> Suppliers => _context.Suppliers;

        public IActionResult List()
        {
            return View(_context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier));
        }

        public async Task<IActionResult> Details(long id)
        {
            Product p = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            ProductViewModel model = ViewModelFactory.Details(p);

            return View("ProductEditor", model);
        }

        public IActionResult Create()
        {
            return View("ProductEditor", ViewModelFactory.Create(new Product(), Categories, Suppliers));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Product product)
        {
            if (ModelState.IsValid)
            {
                product.ProductId = default;
                product.Category = default;
                product.Supplier = default;
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            return View("ProductEditor", ViewModelFactory.Create(product, Categories, Suppliers));
        }

        public async Task<IActionResult> Edit(long id)
        {
            Product p = await _context.Products.FindAsync(id);
            ProductViewModel model = ViewModelFactory.Edit(p, Categories, Suppliers);
            return View("ProductEditor", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Category = default;
                product.Supplier = default;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            return View("ProductEditor", ViewModelFactory.Edit(product, Categories, Suppliers));
        }

        public async Task<IActionResult> Delete(long id)
        {
            ProductViewModel model =
                ViewModelFactory.Delete(await _context.Products.FindAsync(id), Categories, Suppliers);
            return View("ProductEditor", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
    }
}
