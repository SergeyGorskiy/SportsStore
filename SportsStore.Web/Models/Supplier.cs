using System.Collections.Generic;
using System.Net.Http.Headers;

namespace SportsStore.Web.Models
{
    public class Supplier
    {
        public long SupplierId { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}