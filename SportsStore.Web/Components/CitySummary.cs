using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Web.EF;

namespace SportsStore.Web.Components
{
    public class CitySummary : ViewComponent
    {
        private readonly CitiesData _data;

        public CitySummary(CitiesData data)
        {
            _data = data;
        }

        public string Invoke()
        {
            return $"{_data.Cities.Count()} cities, {_data.Cities.Sum(c => c.Population)} people";
        }
    }
}