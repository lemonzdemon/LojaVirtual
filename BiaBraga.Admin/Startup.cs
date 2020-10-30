using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BiaBraga.Admin.Services;
using BiaBraga.Repository;
using BiaBraga.Repository.Context;
using BiaBraga.Repository.Interfaces;
using BiaBraga.Repository.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BiaBraga.Admin
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
            services.AddMvc(m =>
            {
                m.EnableEndpointRouting = false;
            });

            services.AddDbContext<BiaBragaDbContext>(x =>
            x.UseMySql(Settings.ConnectionString, builder => builder.MigrationsAssembly("BiaBraga.Repository")));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSingleton<Microsoft.AspNetCore.Mvc.Infrastructure.IActionContextAccessor, Microsoft.AspNetCore.Mvc.Infrastructure.ActionContextAccessor>();
            services.AddScoped<AuthEventService>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddCookie(o =>
            {
                o.AccessDeniedPath = new PathString("/Acessos/Restrito");
                o.LoginPath = new PathString("/Users/Login/");
                o.Cookie.Path = "/";
                o.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                o.Cookie.HttpOnly = true;
                o.LogoutPath = new PathString("/Users/Logout/");
                o.EventsType = typeof(AuthEventService);
            });


            services.AddScoped<StartDefaultRepository>();
            services.AddScoped<IBiaBragaRepository, BiaBragaRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, StartDefaultRepository startDefaultRepository)
        {
            var ptBr = new CultureInfo("PT-br");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(ptBr),
                SupportedCultures = new List<CultureInfo> { ptBr },
                SupportedUICultures = new List<CultureInfo> { ptBr }
            };

            app.UseRequestLocalization(localizationOptions);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();

            app.UseMvcWithDefaultRoute();

            startDefaultRepository.CreateDefaultAsync().Wait();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Users}/{action=Login}/{id?}");
            //});
        }
    }
}
