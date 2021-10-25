using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AcademyApp.Domain.POCO;

namespace AcademyApp.Data.EF.Interface
{
    public interface IArchiveRepository
    {
        Task<List<Archive>> GetAllAsync();
        Task<Archive> GetAsync(int id);
        Task AddAsync(Archive archive);
    }
}
