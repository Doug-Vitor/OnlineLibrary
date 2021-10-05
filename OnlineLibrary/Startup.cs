using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineLibrary.Data;
using OnlineLibrary.Extensions;
using OnlineLibrary.Repositories;
using OnlineLibrary.Repositories.Interfaces;
using OnlineLibrary.Services;
using OnlineLibrary.Services.Interfaces;
using System.Collections.Generic;
using System.Globalization;

namespace OnlineLibrary
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
            services.AddControllersWithViews();

            services.ConfigureApplicationCookie(options => options.AccessDeniedPath = "/Home/AccessDenied");
            services.AddHttpContextAccessor().AddSingleton<HttpContextExtensions>();

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration
                .GetConnectionString("Default")));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<SeedingServices>();

            services.AddScoped<IAccountServices, AccountServices>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();

            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookServices, BookServices>();

            services.AddScoped<IImageManagerServices, ImageManagerServices>();

            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IShoppingCartServices, ShoppingCartServices>();
            services.AddScoped<IShoppingCartItemsRepository, ShoppingCartItemsRepository>();

            services.AddScoped<IPurchaseServices, PurchaseServices>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            SeedingServices seedingServices)
        {
            CultureInfo ptBr = new("pt-BR");
            RequestLocalizationOptions localizationOptions = new()
            {
                DefaultRequestCulture = new RequestCulture(ptBr),
                SupportedCultures = new List<CultureInfo> { ptBr },
                SupportedUICultures = new List<CultureInfo> { ptBr },
            };
            app.UseRequestLocalization(localizationOptions);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                seedingServices.SeedDb();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "AreaApplicationUser",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
