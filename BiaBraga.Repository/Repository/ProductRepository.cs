using BiaBraga.Domain.Models.Entitys;
using BiaBraga.Repository.Context;
using BiaBraga.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiaBraga.Repository.Repository
{
    public class ProductRepository : BiaBragaRepository, IProductRepository
    {
        public ProductRepository(BiaBragaDbContext context) : base(context)
        {
        }


        public async Task<List<Product>> GetAllProductByCategorie(int categoryId) => 
            await _context.Products
            .Include(x => x.Categoria)
            .Where(x => x.CategoryId == categoryId)
            .ToListAsync();

        public async Task<List<Product>> GetAllProductsAsync() =>
            await _context.Products
            .Include(x => x.Categoria)
            .OrderBy(x => x.Categoria)
            .ThenBy(x => x.Name)
            .ToListAsync();



        public async Task<Product> GetProductByIdAsync(int id) => 
            await _context.Products
            .Include(x => x.Categoria)
            .FirstOrDefaultAsync(x => x.ID == id);
    }
}
