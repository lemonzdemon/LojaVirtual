using BiaBraga.Repository.Context;
using BiaBraga.Repository.Interfaces;
using BiaBraga.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BiaBraga.Repository
{
    public class Settings
    {
        private readonly string ConnectionString = "server=localhost;userid=root;password=123456;database=biabraga";

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BiaBragaDbContext>(x =>
           x.UseMySql(ConnectionString, builder => builder.MigrationsAssembly("BiaBraga.Repository")));

            services.AddScoped<IBiaBragaRepository, BiaBragaRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<StartDefaultRepository>();
        }
    }
}
