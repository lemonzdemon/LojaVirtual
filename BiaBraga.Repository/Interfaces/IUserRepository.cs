using BiaBraga.Business.Dtos;
using BiaBraga.Domain.Models;
using BiaBraga.Domain.Models.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiaBraga.Repository.Interfaces
{
    public interface IUserRepository : IBiaBragaRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<List<User>> GetAllUsersAsync();
        Task<ResultDefault> VerifyLoginAsync(LoginDto login);
    }
}
