using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Web.EF;
using SportsStore.Web.Models;

namespace SportsStore.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DataContext _context;

        public IndexModel(DataContext context)
        {
            _context = context;
        }

        public Product Product { get; set; }

        public async Task OnGetAsync(long id = 1)
        {
            Product = await _context.Products.FindAsync(id);
        }
    }
}
