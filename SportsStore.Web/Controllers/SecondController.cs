using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Web.Controllers
{
    public class SecondController : Controller
    {
        public IActionResult Index()
        {
            return View("Common");
        }
    }
}
