using BiaBraga.Domain.Models.Entitys;
using System.Threading.Tasks;

namespace BiaBraga.Repository.Interfaces
{
    public interface ICategoryRepository : IBiaBragaRepository
    {
        Task<bool> CategorieExistAsync(string name, int? id);
        Task<bool> CategorieExistInProductAsync(int id);
        Task<Category> GetCategoryByIdAsync(int id);
    }
}
