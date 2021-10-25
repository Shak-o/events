using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AcademyApp.Data.EF.Interface;
using AcademyApp.Domain.POCO;
using Microsoft.EntityFrameworkCore;

namespace AcademyApp.Data.EF.Implementation
{
    public class ArchiveRepository : IArchiveRepository
    {
        private readonly IBaseRepository<Archive> _archiveRepo;
        private readonly IBaseRepository<User> _userRepo;
        public ArchiveRepository(IBaseRepository<Archive> archiveRepo, IBaseRepository<User> userRepo)
        {
            _archiveRepo = archiveRepo;
            _userRepo = userRepo;
        }
        public async Task<List<Archive>> GetAllAsync()
        {
            return await _archiveRepo.Table.Include(x => x.User).ToListAsync();
        }

        public async Task<Archive> GetAsync(int id)
        {
            return await _archiveRepo.Table.Include(x => x.User).FirstAsync(x => x.Id == id);
        }

        public async Task AddAsync(Archive archive)
        {
            _userRepo.SetState(archive.User, EntityState.Unchanged);
            await _archiveRepo.CreateAsync(archive);
        }
    }
}
