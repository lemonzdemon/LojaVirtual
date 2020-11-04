using BiaBraga.Repository.Context;
using BiaBraga.Repository.Interfaces;
using BiaBraga.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BiaBraga.Repository.Classes
{
    public class Settings
    {
        public static readonly string UrlAPI = "";
        public static readonly string UrlAdmin = "https://localhost:44391/";

        private readonly string ConnectionString = "server=localhost;userid=root;password=root;database=biabraga";

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BiaBragaDbContext>(x =>
           x.UseMySql(ConnectionString, builder => builder.MigrationsAssembly("BiaBraga.Repository")));

            services.AddScoped<IBiaBragaRepository, BiaBragaRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<StartDefaultRepository>();
        }
    }
}
