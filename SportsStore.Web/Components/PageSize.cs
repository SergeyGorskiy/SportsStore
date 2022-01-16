
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Web.Components
{
    public class PageSize : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            HttpClient _client = new HttpClient();

            HttpResponseMessage response = await _client.GetAsync("http://apress.com");

            return View(response.Content.Headers.ContentLength);
        }
    }
}
