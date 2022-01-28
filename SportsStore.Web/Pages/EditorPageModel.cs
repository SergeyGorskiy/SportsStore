using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Web.EF;
using SportsStore.Web.Models;
using SportsStore.Web.ViewModels;

namespace SportsStore.Web.Pages
{
    public class EditorPageModel : PageModel
    {
        public EditorPageModel(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public DataContext DataContext { get; set; }

        public ProductViewModel ViewModel { get; set; }

        public IEnumerable<Category> Categories => DataContext.Categories;

        public IEnumerable<Supplier> Suppliers => DataContext.Suppliers;

        protected async Task CheckNewCategory(Product product)
        {
            if (product.CategoryId == -1 && !string.IsNullOrEmpty(product.Category?.Name))
            {
                DataContext.Categories.Add(product.Category);
                await DataContext.SaveChangesAsync();
                product.CategoryId = product.Category.CategoryId;
                ModelState.Clear();
                TryValidateModel(product);
            }
        }
    }
}
