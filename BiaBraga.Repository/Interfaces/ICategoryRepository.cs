using BiaBraga.Domain.Models.Entitys;
using System.Threading.Tasks;

namespace BiaBraga.Repository.Interfaces
{
    public interface ICategoryRepository : IBiaBragaRepository
    {
        Task<bool> CategorieExistAsync(string name);
        Task<Category> GetCategoryByIdAsync(int id);
    }
}
