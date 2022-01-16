using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Web.EF;
using SportsStore.Web.ViewModels;

namespace SportsStore.Web.Components
{
    public class CitySummary : ViewComponent
    {
        private readonly CitiesData _data;

        public CitySummary(CitiesData data)
        {
            _data = data;
        }

        public IViewComponentResult Invoke(string themeName)
        {
            ViewBag.Theme = themeName;

            return View(new CityViewModel
            {
                Cities = _data.Cities.Count(), 
                Population = _data.Cities.Sum(c => c.Population)
            });
        }
    }
}