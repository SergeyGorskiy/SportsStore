using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStore.Web.EF;
using SportsStore.Web.TagHelpers;

namespace SportsStore.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opts =>
            {
                opts.UseSqlServer(Configuration["ConnectionStrings:SportsStoreProductConnection"]);
                opts.EnableSensitiveDataLogging(true);
            });
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddSingleton<CitiesData>();
            //services.AddTransient<ITagHelperComponent, TimeTagHelperComponent>();
            //services.AddTransient<ITagHelperComponent, TableFooterTagHelperComponent>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
                
            });

            SeedData.SeedDatabase(context);
        }
    }
}
