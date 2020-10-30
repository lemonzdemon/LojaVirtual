using BiaBraga.Domain.Models.Entitys;
using Microsoft.EntityFrameworkCore;

namespace BiaBraga.Repository.Context
{
    public class BiaBragaDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories{ get; set; }

        public BiaBragaDbContext(DbContextOptions<BiaBragaDbContext> options) : base(options)
        {
        }
    }
}
