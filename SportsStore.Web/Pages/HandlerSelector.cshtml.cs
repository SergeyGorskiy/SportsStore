using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SportsStore.Web.EF;
using SportsStore.Web.Models;

namespace SportsStore.Web.Pages
{
    public class HandlerSelectorModel : PageModel
    {
        private readonly DataContext _context;

        public Product Product { get; set; }

        public HandlerSelectorModel(DataContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(long id = 1)
        {
            Product = await _context.Products.FindAsync(id);
        }

        public async Task OnGetRelatedAsync(long id = 1)
        {
            Product = await _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            Product.Supplier.Products = null;
            Product.Category.Products = null;
        }
    }
}
