using BiaBraga.Repository.Context;
using BiaBraga.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiaBraga.Repository.Repository
{
    public class BiaBragaRepository : IBiaBragaRepository
    {
        protected BiaBragaDbContext _context;

        public BiaBragaRepository(BiaBragaDbContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<bool> AddAsync<T>(T entity) where T : class
        {
            await _context.AddAsync<T>(entity);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync<T>(T entity) where T : class
        {
            try
            {
                _context.Remove(entity);
                return await SaveAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> UpdateAsync<T>(T entity) where T : class
        {
            if (_context.Entry<T>(entity).State == EntityState.Detached)
            {
                _context.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                _context.Update(entity);
            }

            return await SaveAsync();
        }

        public async Task<List<T>> GetAllAsync<T>() where T : class
        {
            var result = new List<T>();
            try
            {
                result = await _context.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                //gravar log
            }
            return result;
        }

        private async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() != 1;
            }
            catch (Exception ex)
            {
                //gravar log
                return false;
            }
        }
    }
}
