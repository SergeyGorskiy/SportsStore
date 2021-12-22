using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsStore.Web.EF;
using SportsStore.Web.Models;

namespace SportsStore.Web.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class ContentController : ControllerBase
    {
        private readonly DataContext _context;

        public ContentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("string")]
        public string GetString() => "This is a string response";

        [HttpGet("object/{format?}")]
        [FormatFilter]
        [Produces("application/json", "application/xml")]
        public async Task<ProductBindingTarget> GetObject()
        {
            Product p = await _context.Products.FirstAsync();

            return new ProductBindingTarget
            {
                Name = p.Name, Price = p.Price, CategoryId = p.CategoryId, SupplierId = p.SupplierId
            };
        }

        [HttpPost]
        [Consumes("application/json")]
        public string SaveProductJson(ProductBindingTarget product)
        {
            return $"JSON: {product.Name}";
        }

        //[HttpPost]
        //[Consumes("application/xml")]
        //public string SaveProductXml(ProductBindingTarget product)
        //{
        //    return $"XML: {product.Name}";                                                                         
        //}
    }
}
