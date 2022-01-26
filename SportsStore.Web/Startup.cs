using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStore.Web.EF;

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
            services.Configure<AntiforgeryOptions>(opts => { opts.HeaderName = "X-XSRF-TOKEN"; });
            services.Configure<MvcOptions>(opts =>
                opts.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(value => "Please enter a value"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext context, IAntiforgery antiforgery)
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

            app.Use(async (context, next) =>
            {
                if (!context.Request.Path.StartsWithSegments("/api"))
                {
                    context.Response.Cookies.Append
                    ("XSRF-TOKEN", antiforgery.GetAndStoreTokens(context).RequestToken,
                        new CookieOptions {HttpOnly = false});
                }
                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute("forms", "controllers/{controller=Home}/{action=index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                
            });

            SeedData.SeedDatabase(context);
        }
    }
}
