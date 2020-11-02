using BiaBraga.Domain.Models.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiaBraga.Repository.Interfaces
{
    public interface IProductRepository : IBiaBragaRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<List<Product>> GetAllProductByCategorie(int categoryId);

        Task<List<Product>> GetAllProductsAsync();

    }
}
