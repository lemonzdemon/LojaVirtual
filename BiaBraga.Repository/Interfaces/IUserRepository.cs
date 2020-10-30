using BiaBraga.Domain.Models;
using BiaBraga.Domain.Models.Entitys;
using System.Threading.Tasks;

namespace BiaBraga.Repository.Interfaces
{
    public interface IUserRepository : IBiaBragaRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<ResultDefault> VerifyLoginAsync(Login login);
    }
}
