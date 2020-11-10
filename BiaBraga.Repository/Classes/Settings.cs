using BiaBraga.Repository.Context;
using BiaBraga.Repository.Interfaces;
using BiaBraga.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace BiaBraga.Repository.Classes
{
    public class Settings
    {
        public static readonly string UrlAPI = "https://localhost:44346";
        public static readonly string UrlAdmin = "https://localhost:44391";
        public static string SecretKey { get; private set; }

        private static string ConnectionString;


        private void GetConfigs()
        {
            var fileConfig = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.FullName, "config.txt");
            string conn = string.Empty;
            string secretKey = string.Empty;

            if (File.Exists(fileConfig))
            {
                //using StreamReader sr = new StreamReader(fileConfig);
                //sr.ReadLineAsync
            }

            SetConnectionString(conn);
            SetSecretKey(secretKey);
        }

        private void SetConnectionString(string conn)
        {
            ConnectionString = string.IsNullOrEmpty(conn) ? "server=localhost;userid=root;password=root;database=biabraga" : conn;
        }

        private void SetSecretKey(string secretKey)
        {
            SecretKey = string.IsNullOrEmpty(secretKey) ? "4F8B2A41D93D573E81D5C1937BAAF" : secretKey;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            GetConfigs();
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
