using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AcademyApp.Data.EF.Interface;
using AcademyApp.Domain.POCO;

namespace AcademyApp.Data.EF.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly IBaseRepository<User> _repo;

        public UserRepository(IBaseRepository<User> repo)
        {
            _repo = repo;
        }
        public async Task<List<User>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }
    }
}
