using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SportsStore.Web.EF;
using SportsStore.Web.Pages;
using SportsStore.Web.ViewModels;

namespace SportsStore.Web.Controllers
{
    [ViewComponent(Name = "CitiesControllerHybrid")]
    public class CitiesController : Controller
    {
        private readonly CitiesData _data;

        public CitiesController(CitiesData data)
        {
            _data = data;
        }

        public IActionResult Index()
        {
            return View(_data.Cities);
        }

        public IViewComponentResult Invoke()
        {
            return new ViewViewComponentResult()
            {
                ViewData = new ViewDataDictionary<CityViewModel>(ViewData, new  CityViewModel
                {
                    Cities = _data.Cities.Count(),
                    Population = _data.Cities.Sum(c => c.Population)
                })
            };
        }
    }
}
