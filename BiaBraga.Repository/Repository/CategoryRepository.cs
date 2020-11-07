using BiaBraga.Domain.Models.Entitys;
using BiaBraga.Repository.Context;
using BiaBraga.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BiaBraga.Repository.Repository
{
    public class CategoryRepository : BiaBragaRepository, ICategoryRepository
    {
        public CategoryRepository(BiaBragaDbContext context) : base(context)
        {
        }

        public async Task<bool> CategorieExistAsync(string name, int? id) =>
            await _context.Categories
            .AnyAsync(x => x.Name.ToUpper() == name.ToUpper() && x.Id != id);

        public async Task<Category> GetCategoryByIdAsync(int id) =>
            await _context.Categories
            .Include(x => x.Department)
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<bool> CategorieExistInProductAsync(int id) =>
            await _context.Products.AnyAsync(x => x.CategoryId == id);
    }
}
