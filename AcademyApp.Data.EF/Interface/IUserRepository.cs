using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AcademyApp.Domain.POCO;

namespace AcademyApp.Data.EF.Interface
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
    }
}
