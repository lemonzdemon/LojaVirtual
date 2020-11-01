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

        public async Task<bool> CategorieExistAsync(string name) =>
            await _context.Categories
            .AnyAsync(x => x.Name.ToUpper() == name.ToUpper());

        public async Task<Category> GetCategoryByIdAsync(int id) =>
            await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
