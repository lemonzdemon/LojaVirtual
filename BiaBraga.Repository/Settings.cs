using BiaBraga.Business.Classes;
using BiaBraga.Repository.Context;
using BiaBraga.Repository.Interfaces;
using BiaBraga.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BiaBraga.Repository
{
    public class Settings
    {
        public static readonly string UrlAPI = "https://localhost:44346";
        public static readonly string UrlAdmin = "https://localhost:44391";
        public static string SecretKey { get; private set; }
        public static string ConnectionString;

        public virtual void ConfigureServices(IServiceCollection services)
        {
            var configs = new GetConfigs().GetModelConfig();
            ConnectionString = configs.ConnectionString;
            SecretKey = configs.SecretKey;

            services.AddDbContext<BiaBragaDbContext>(x =>
           x.UseMySql(ConnectionString, builder => builder.MigrationsAssembly("BiaBraga.Repository")));

            services.AddScoped<IBiaBragaRepository, BiaBragaRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<StartDefaultRepository>();
        }
    }
}
