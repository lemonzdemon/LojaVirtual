using BiaBraga.Domain.Models.Entitys;
using BiaBraga.Repository.Context;
using BiaBraga.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BiaBraga.Repository.Repository
{
    public class DepartmentRepository : BiaBragaRepository, IDepartmentRepository
    {
        public DepartmentRepository(BiaBragaDbContext context) : base(context)
        {
        }

        public async Task<bool> DepartmentExistAsync(string name, int? id)
        => await _context.Departments.AnyAsync(x => x.Name.ToUpper() == name.ToUpper() && id != x.Id);

        public async Task<bool> DepartmentExistInCategoryAsync(int id)
        => await _context.Categories.AnyAsync(x => x.DepartmentId == id);

        public async Task<Department> GetDepartmentByIdAsync(int id)
            => await _context.Departments
            .Include(x => x.Categories)
            .FirstOrDefaultAsync(x => x.Id == id);
        
    }
}
