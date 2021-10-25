using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyApp.Data.EF.Interface
{
    public interface IBaseRepository <T> where T:class
    {
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task<T> GetAsync(params object[] id);
        Task<List<T>> GetAllAsync();
        Task DeleteAsync(T entity);
        Task DeleteAsync(params object[] id);
        IQueryable<T> Table { get; }
        void SetState(T entity, EntityState state);
    }
}
