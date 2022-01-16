using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SportsStore.Web.EF;
using SportsStore.Web.ViewModels;

namespace SportsStore.Web.Pages
{
    [ViewComponent(Name = "CitiesPageHybrid")]
    public class CitiesModel : PageModel
    {
        public CitiesData Data { get; set; }

        public CitiesModel(CitiesData cdata)
        {
            Data = cdata;
        }

        [ViewComponentContext] public ViewComponentContext Context { get; set; }

        public IViewComponentResult Invoke()
        {
            return new ViewViewComponentResult()
            {
                ViewData = new ViewDataDictionary<CityViewModel>(Context.ViewData, new CityViewModel
                {
                    Cities = Data.Cities.Count(),
                    Population = Data.Cities.Sum(c => c.Population)
                })
            };
        }
    }
}
