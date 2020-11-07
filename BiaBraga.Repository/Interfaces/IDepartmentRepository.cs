using BiaBraga.Domain.Models.Entitys;
using System.Threading.Tasks;

namespace BiaBraga.Repository.Interfaces
{
    public interface IDepartmentRepository : IBiaBragaRepository
    {
        Task<bool> DepartmentExistAsync(string name, int? id);
        Task<bool> DepartmentExistInCategoryAsync(int id);
        Task<Department> GetDepartmentByIdAsync(int id);
    }
}
