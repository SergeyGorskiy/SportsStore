using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Web.EF;
using SportsStore.Web.Models;

namespace SportsStore.Web.Pages
{
    public class EditorModel : PageModel
    {
        private readonly DataContext _context;

        public Product Product { get; set; }

        public EditorModel(DataContext context)
        {
            _context = context;
        }

        public async Task OnGet(long id)
        {
            Product = await _context.Products.FindAsync(id);
        }

        public async Task<IActionResult> OnPostAsync(long id, decimal price)
        {
            Product p = await _context.Products.FindAsync(id);
            p.Price = price;
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
