using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsStore.Web.EF;
using SportsStore.Web.Models;

namespace SportsStore.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private readonly DataContext _context;

        public SuppliersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<Supplier> GetSupplier(long id)
        {
            Supplier supplier = await _context.Suppliers
                .Include(s => s.Products)
                .FirstAsync(s => s.SupplierId == id);
           
            foreach (Product p in supplier.Products)
            {
                p.Supplier = null;
            }

            return supplier;
        }

        [HttpPatch("{id}")]
        public async Task<Supplier> PatchSupplier(long id, JsonPatchDocument<Supplier> patchDoc)
        {
            Supplier s = await _context.Suppliers.FindAsync(id);

            if (s != null)
            {
                patchDoc.ApplyTo(s);
                await _context.SaveChangesAsync();
            }

            return s;
        }
    }
}
