using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademyApp.Data.EF.Interface;
using Microsoft.EntityFrameworkCore;

namespace AcademyApp.Data.EF.Implementation
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public IQueryable<T> Table
        {
            get
            {
                return _dbSet;
            }
        }

        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(params object[] id)
        {
            var result = await _dbSet.FindAsync(id);
            if (result != null)
            {
                _dbContext.Remove(result);
            }
            await _dbContext.SaveChangesAsync();
        }
        public async Task<T> GetAsync(params object[] id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public void SetState(T entity, EntityState state)
        {
            _dbContext.Entry(entity).State = state;
        }
    }
}
