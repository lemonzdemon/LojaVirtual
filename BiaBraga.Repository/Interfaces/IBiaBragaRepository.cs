using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BiaBraga.Repository.Interfaces
{
    public interface IBiaBragaRepository
    {
        Task<bool> AddAsync<T>(T entity) where T : class;
        Task<bool> UpdateAsync<T>(T entity) where T : class;
        Task<bool> DeleteAsync<T>(T entity) where T : class;
        Task<List<T>> GetAllAsync<T>() where T : class;
        Task<List<T>> GetWhereAsync<T>(Expression<Func<T, bool>> where) where T : class;
    }
}
