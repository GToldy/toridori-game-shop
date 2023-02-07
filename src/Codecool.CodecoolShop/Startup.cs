using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Managers;
using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codecool.CodecoolShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddControllersWithViews();

            services.AddSingleton<Cart>();

            // SQL database
            var dbMode = Configuration.GetValue<string>("Mode");
            IProductDbDao productDbDao = null;
            IProductCategoryDbDao productCategoryDbDao = null;
            ISupplierDbDao supplierDbDao = null;
            IUserDbDao userDbDao = null;

            if (dbMode == "SQL")
            {
                productDbDao = ProductDbDao.GetInstance(Configuration.GetValue<string>("ConnectionString"));
                productCategoryDbDao = ProductCategoryDbDao.GetInstance(Configuration.GetValue<string>("ConnectionString"));
                supplierDbDao = SupplierDbDao.GetInstance(Configuration.GetValue<string>("ConnectionString"));
                userDbDao = UserDbDao.GetInstance(Configuration.GetValue<string>("ConnectionString"));
            }

            services.AddSingleton<IProductDbDao>(productDbDao);
            services.AddSingleton<ISupplierDbDao>(supplierDbDao);
            services.AddSingleton<IProductCategoryDbDao>(productCategoryDbDao);
            services.AddSingleton<IUserDbDao>(userDbDao);
            services.AddSingleton<UserDbManager>();
            services.AddSingleton<ProductDbManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Product/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "admin",
                    pattern: "{controller=Admin}/{action=Index}/{id?}");
            });
        }
    }
}

