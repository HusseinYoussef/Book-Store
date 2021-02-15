using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.WebApp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BookStore.WebApp.Models;

namespace BookStore.WebApp
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration config, IWebHostEnvironment env)
        {
            _config = config;
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookStoreDbContext>(options => 
                    options.UseNpgsql(_config.GetConnectionString("BookStoreDb")));
            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<BookStoreDbContext>();
            services.AddScoped<IBookRepository, SqlBookRepository>();
            services.AddScoped<ILanguageRepository, SqlLanguageRepository>();
            services.AddScoped<ICategoryRepository, SqlCategoryRepository>();
            services.AddScoped<IUserRepository, SqlUserRepository>();
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddAutoMapper(typeof(Startup).Assembly);
            if(_env.IsDevelopment())
            {
                services.AddRazorPages().AddRazorRuntimeCompilation();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints => 
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}"
                );
            });
        }
    }
}
