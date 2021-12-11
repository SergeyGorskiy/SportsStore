using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SportsStore.Web.EF;

namespace SportsStore.Web
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _nextDelegate;

        public TestMiddleware(RequestDelegate nextDelegate)
        {
            _nextDelegate = nextDelegate;
        }

        public async Task Invoke(HttpContext context, DataContext dataContext)
        {
            {
                if (context.Request.Path == "/test")
                {
                    await context.Response.WriteAsync($"There are {dataContext.Products.Count()} products\n");
                    await context.Response.WriteAsync($"There are {dataContext.Categories.Count()} categories\n");
                    await context.Response.WriteAsync($"There are {dataContext.Suppliers.Count()} suppliers\n");
                }
                else
                {
                    await _nextDelegate(context);
                }
            }
        }
    }
}